namespace WristbandRadio.FileServer.Common.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : IDbEntity
{
    protected readonly DapperDataContext _dapperDataContext;
    public GenericRepository(DapperDataContext dapperDataContext)
    {
        _dapperDataContext = dapperDataContext;
    }
    public async Task<IEnumerable<T>> GetAsync(params string[] selectData)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        //parameters.Add("pageNumber", queryParameters.PageNo, DbType.Int32, ParameterDirection.Input);
        //parameters.Add("pageSize", queryParameters.PageSize, DbType.Int32, ParameterDirection.Input);

        if (selectData != null && selectData.Length > 0)
            parameters.Add(COLUMNS, typeof(T).GetDbTableColumnNames(selectData), DbType.String, ParameterDirection.Input);

        using (var connection = await _dapperDataContext.GetConnection())
        {
            return await connection.QueryAsync<T>("get_records", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<T> GetByIdAsync(Guid id, params string[] selectData)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add(ID, id, DbType.String, ParameterDirection.Input, size: 22);

        if (selectData != null && selectData.Length > 0)
            parameters.Add(COLUMNS, typeof(T).GetDbTableColumnNames(selectData), DbType.String, ParameterDirection.Input);

        using (var connection = await _dapperDataContext.GetConnection())
        {
            return await connection.QuerySingleOrDefaultAsync<T>("get_records_by_id", parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public async Task<IEnumerable<T>> GetBySpecificColumnAsync(string columnName, string columnValue, params string[] selectData)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add(COLUMN_NAME, columnName, DbType.String, ParameterDirection.Input, size: 60);
        parameters.Add(VALUE, columnValue, DbType.String, ParameterDirection.Input, size: 100);

        if (selectData != null && selectData.Length > 0)
            parameters.Add(COLUMNS, typeof(T).GetDbTableColumnNames(selectData), DbType.String, ParameterDirection.Input);

        using (var connection = await _dapperDataContext.GetConnection())
        {
            return await connection.QueryAsync<T>("get_records_by_column", parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<Guid> AddAsync(T entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add(TABLE_NAME, typeof(T).GetDbTableName(), DbType.String, ParameterDirection.Input, size: 50);
        parameters.Add(COLUMNS, typeof(T).GetDbTableColumnNames(new string[0]), DbType.String, ParameterDirection.Input);
        parameters.Add(VALUES, typeof(T).GetColumnValuesForInsert(entity), DbType.String, ParameterDirection.Input);
        using (var connection = await _dapperDataContext.GetConnection())
        {
            var id =  await connection.ExecuteScalarAsync<string>("insert_record", parameters, _dapperDataContext.Transaction, commandType: CommandType.StoredProcedure);
            return new Guid(id);
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
