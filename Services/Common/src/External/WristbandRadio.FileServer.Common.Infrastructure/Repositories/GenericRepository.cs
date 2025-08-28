namespace WristbandRadio.FileServer.Common.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : IDbEntity
{
    protected readonly DapperDataContext _dapperDataContext;
    public GenericRepository(DapperDataContext dapperDataContext)
    {
        _dapperDataContext = Guard.Against.Null(dapperDataContext);
    }
    public async Task<IEnumerable<T>> GetAsync(QueryParameters queryParameters, params string[] selectData)
    {
        var tableName = typeof(T).GetDbTableName();
        var pageNumber = queryParameters.PageNo;
        var pageSize = queryParameters.PageSize;
        var columns = selectData != null && selectData.Length > 0
            ? typeof(T).GetDbTableColumnNames(selectData)
            : "*"; // Default to all columns if none specified
        var previousPageLastRecord = (pageNumber - 1) * pageSize;

        var parameters = new { PreviousPageLastRecord = previousPageLastRecord, PageSize = pageSize };
        var sql = $"SELECT {columns} FROM {tableName} WHERE paging_order > @PreviousPageLastRecord AND removed_datetime IS NULL ORDER BY paging_order LIMIT @PageSize";

        var connection = await _dapperDataContext?.GetConnection();
        var returnVal = await connection.QueryAsync<T>(sql, parameters);
        return returnVal;
    }
    public async Task<T> GetByIdAsync(Guid id, params string[] selectData)
    {
        var tableName = typeof(T).GetDbTableName();
        var columns = selectData != null && selectData.Length > 0
            ? typeof(T).GetDbTableColumnNames(selectData)
            : "*"; // Default to all columns if none specified

        var parameters = new { Id = id };
        var sql = $"SELECT {columns} FROM {tableName} WHERE id = @Id AND removed_datetime IS NULL";

        var connection = await _dapperDataContext?.GetConnection();
        var returnVal = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        return returnVal;
    }

    public async Task<IEnumerable<T>> GetBySpecificColumnAsync(QueryParameters queryParameters, string columnName, string columnValue, params string[] selectData)
    {
        var pageNumber = queryParameters.PageNo;
        var pageSize = queryParameters.PageSize;
        var previousPageLastRecord = (pageNumber - 1) * pageSize;
        var tableName = typeof(T).GetDbTableName();
        var columns = selectData != null && selectData.Length > 0
            ? typeof(T).GetDbTableColumnNames(selectData)
            : "*"; // Default to all columns if none specified
        var filterColumn = typeof(T).GetDbTableColumnNames([columnName]);

        var paremeters = new { PreviousPageLastRecord = previousPageLastRecord, PageSize = pageSize, ColumnValue = columnValue };
        var sql = $"SELECT {columns} FROM {tableName} WHERE paging_order > @PreviousPageLastRecord AND {filterColumn} = @ColumnValue AND removed_datetime IS NULL ORDER BY paging_order LIMIT @PageSize";

        var connection = await _dapperDataContext?.GetConnection();
        var returnVal = await connection.QueryAsync<T>(sql, paremeters);
        return returnVal;
    }   
    public async Task<Guid> AddAsync(T entity)
    {
        var tableName = typeof(T).GetDbTableName();
        var columns = typeof(T).GetDbTableColumnNames(new string[0]);
        var parameterNames = typeof(T).GetParameterNames(new string[0]);
        var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({parameterNames}) RETURNING id";
        var connection = await _dapperDataContext?.GetConnection();
        var result = await connection.ExecuteScalarAsync<Guid>(sql, entity);
        return result;
    }
    public async Task UpdateAsync(T entity)
    {
        var tableName = typeof(T).GetDbTableName();
        var columns = typeof(T).GetColumnValuesForUpdate(entity);
        var sql = $"UPDATE {tableName} SET {columns} WHERE id = @Id";
        var connection = await _dapperDataContext?.GetConnection();
        await connection.ExecuteAsync(sql, entity);
    }
    public async Task SoftDeleteAsync(Guid id, bool softDeleteFromRelatedChildTables = false)
    {
        var tableName = typeof(T).GetDbTableName();
        var removedBy = Guid.NewGuid(); // This should be the id of the user calling the api
        var parameters = new { Id = id, RemovedBy = removedBy };
        var sql = $"UPDATE {tableName} SET removed_datetime = NOW(), removed_by = @RemovedBy WHERE id = @Id";
        var connection = await _dapperDataContext.GetConnection();
        await connection.ExecuteAsync(sql, parameters);

        if (softDeleteFromRelatedChildTables)
        {
            foreach (var associatedType in typeof(T).GetAssociatedTypes())
            {
                var childTableName = associatedType.Type.GetDbTableName();
                var childForeignKeyColumn = associatedType.ForeignKeyProperty.GetDbColumnName();
                var sql2 = $"UPDATE {childTableName} SET removed_datetime = NOW(), removed_by = @RemovedBy WHERE {childForeignKeyColumn} = @Id";

                await connection.ExecuteAsync("soft_delete_records_by_column", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
            }
        }
    }
    public async Task<int> GetTotalCountAsync()
    {
        var tableName = typeof(T).GetDbTableName();
        var sql = $"SELECT COUNT(Id) FROM {tableName} WHERE removed_datetime IS NULL";
        var connection = await _dapperDataContext.GetConnection();
        return await connection.QuerySingleOrDefaultAsync<int>(sql);
    }
    public async Task<bool> IsExistingAsync(string distinguishingUniqueKeyValue)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add(COLUMN_NAME, typeof(T).GetDistinguishingUniqueKeyName(), DbType.String, ParameterDirection.Input, size: 100);
        parameters.Add(VALUE, distinguishingUniqueKeyValue, DbType.String, ParameterDirection.Input, size: 100);

        using (var connection = await _dapperDataContext.GetConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<bool>("does_record_exist", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
