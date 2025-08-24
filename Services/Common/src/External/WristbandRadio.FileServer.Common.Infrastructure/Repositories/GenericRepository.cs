namespace WristbandRadio.FileServer.Common.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : IDbEntity
{
    protected readonly DapperDataContext _dapperDataContext;
    public GenericRepository(DapperDataContext dapperDataContext)
    {
        _dapperDataContext = dapperDataContext;
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

        using (var connection = await _dapperDataContext.GetConnection())
        {
            var returnVal = await connection.QueryAsync<T>(sql, parameters);
            
            return returnVal;
        }
    }
    public async Task<T> GetByIdAsync(Guid id, params string[] selectData)
    {
        var tableName = typeof(T).GetDbTableName();
        var columns = selectData != null && selectData.Length > 0
            ? typeof(T).GetDbTableColumnNames(selectData)
            : "*"; // Default to all columns if none specified

        var parameters = new { Id = id };
        var sql = $"SELECT {columns} FROM {tableName} WHERE id = @Id AND removed_datetime IS NULL";

        using (var connection = await _dapperDataContext.GetConnection())
        {
            var returnVal = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
            return returnVal;
        }
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

        using (var connection = await _dapperDataContext.GetConnection())
        {
            var returnVal = await connection.QueryAsync<T>(sql, paremeters);
            return returnVal;
        }
    }
    public async Task<Guid> AddAsync(T entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add(COLUMNS, typeof(T).GetDbTableColumnNames(new string[0]), DbType.String, ParameterDirection.Input);
        parameters.Add(COLUMN_VALUES, typeof(T).GetColumnValuesForInsert(entity), DbType.String, ParameterDirection.Input);
        using (var connection = await _dapperDataContext.GetConnection())
        {
            await connection.ExecuteScalarAsync<string>("insert_record", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
            return entity.Id;
        }
    }
    public async Task UpdateAsync(T entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add(COLUMNS, typeof(T).GetColumnValuesForUpdate(entity), DbType.String, ParameterDirection.Input);
        parameters.Add(ID, entity.Id, DbType.String, ParameterDirection.Input, size: 22);
        using (var connection = await _dapperDataContext.GetConnection())
        {
            await connection.ExecuteAsync("update_record", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task SoftDeleteAsync(Guid id, bool softDeleteFromRelatedChildTables = false)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add(ID, id, DbType.String, ParameterDirection.Input, size: 22);
        using (var connection = await _dapperDataContext.GetConnection())
        {
            await connection.ExecuteAsync("soft_delete_record", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);

            if (softDeleteFromRelatedChildTables)
                foreach (var associatedType in typeof(T).GetAssociatedTypes())
                {
                    parameters = new DynamicParameters();
                    parameters.Add(TABLE_NAME, associatedType.Type.GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
                    parameters.Add(COLUMN_NAME, associatedType.ForeignKeyProperty.GetDbColumnName(), DbType.String, ParameterDirection.Input, size: 50);
                    parameters.Add(VALUE, id, DbType.String, ParameterDirection.Input, size: 22);

                    await connection.ExecuteAsync("soft_delete_records_by_column", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
                }
        }
    }
    public async Task<int> GetTotalCountAsync()
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);

        using (var connection = await _dapperDataContext.GetConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<int>("get_total_records_count", parameters, commandType: CommandType.StoredProcedure);
        }
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
