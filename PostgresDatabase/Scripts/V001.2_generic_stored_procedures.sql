-- Does Record Exist
CREATE PROCEDURE does_record_exist
    @table_name VARCHAR(255),
    @column_name VARCHAR(255),
    @value VARCHAR(255)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'SELECT COUNT(*) FROM ' + QUOTENAME(@table_name) + 
               N' WHERE ' + QUOTENAME(@column_name) + N' = @value';
    
    EXEC sp_executesql @sql, N'@value VARCHAR(255)', @value;
END;

-- Get Records
CREATE PROCEDURE get_records
    @table_name VARCHAR(255),
    @columns VARCHAR(MAX) = NULL,
    @page_number INT = 1,
    @page_size INT = 10
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    DECLARE @previous_page_last_record INT;

    IF @columns IS NULL
	   SET @columns = '*';

    SET @previous_page_last_record = (@page_number-1)*@page_size;
    
    SET @sql= N'SELECT TOP ('+CONVERT(VARCHAR(7),@page_size)+') '+ @columns + ' FROM ' + QUOTENAME(@table_name)+ ' WHERE PagingOrder > @previous_page_last_record AND deleted_date_time IS NULL ORDER BY PagingOrder';

    EXEC sp_executesql @sql, N'@page_number INT, @page_size INT', @page_number, @page_size;
END;

-- Get Records By Id
CREATE PROCEDURE get_records_by_id
    @table_name VARCHAR(255),
    @id uuid,
    @columns VARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    IF @columns IS NULL
        SET @columns = '*';

    SET @sql = N'SELECT ' + @columns + ' FROM ' + QUOTENAME(@table_name) + 
               N' WHERE id = @id AND deleted_date_time IS NULL';

    EXEC sp_executesql @sql, N'@id uuid', @id;
END;

-- Get Records By Column
CREATE PROCEDURE get_records_by_column
    @table_name VARCHAR(255),
    @column_name VARCHAR(255),
    @value VARCHAR(255),
    @columns VARCHAR(MAX) = NULL
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    IF @columns IS NULL
        SET @columns = '*';
    SET @sql = N'SELECT ' + @columns + ' FROM ' + QUOTENAME(@table_name) + 
               N' WHERE ' + QUOTENAME(@column_name) + N' = @value AND deleted_date_time IS NULL';
    EXEC sp_executesql @sql, N'@value VARCHAR(255)', @value;
END;

-- Get User Roles
CREATE PROCEDURE get_user_roles
    @user_id uuid
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'SELECT ur.*, r.role_name FROM users.user_roles ur ' +
               N'JOIN users.r_roles r ON ur.role_id = r.role_id ' +
               N'WHERE ur.user_id = @user_id AND ur.deleted_date_time IS NULL';
    
    EXEC sp_executesql @sql, N'@user_id uuid', @user_id;
END;

-- Get Artist User Roles
CREATE PROCEDURE get_artist_user_roles
    @user_id uuid
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'SELECT aur.*, r.role_name FROM users.artist_user_roles aur ' +
               N'JOIN users.r_roles r ON aur.role_id = r.role_id ' +
               N'JOIN music.artists a ON aur.artist_id = a.artist_id ' +
               N'WHERE aur.user_id = @user_id AND aur.deleted_date_time IS NULL AND a.deleted_date_time IS NULL';
    
    EXEC sp_executesql @sql, N'@user_id uuid', @user_id;
END;

-- Get Artist User Roles By Artist Id
CREATE PROCEDURE get_artist_user_roles_by_artist_id
    @artist_id uuid
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'SELECT aur.*, r.role_name FROM users.artist_user_roles aur ' +
               N'JOIN users.r_roles r ON aur.role_id = r.role_id ' +
               N'JOIN music.artists a ON aur.artist_id = a.artist_id ' +
               N'WHERE aur.artist_id = @artist_id AND aur.deleted_date_time IS NULL AND a.deleted_date_time IS NULL';
    
    EXEC sp_executesql @sql, N'@artist_id uuid', @artist_id;
END;

-- GET Artist User Roles By Artist Id And User Id
CREATE PROCEDURE get_artist_user_roles_by_artist_id_and_user_id
    @artist_id uuid,
    @user_id uuid
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'SELECT aur.*, r.role_name FROM users.artist_user_roles aur ' +
               N'JOIN users.r_roles r ON aur.role_id = r.role_id ' +
               N'JOIN music.artists a ON aur.artist_id = a.artist_id ' +
               N'WHERE aur.artist_id = @artist_id AND aur.user_id = @user_id ' +
               N'AND aur.deleted_date_time IS NULL AND a.deleted_date_time IS NULL';
    
    EXEC sp_executesql @sql, N'@artist_id uuid, @user_id uuid', @artist_id, @user_id;
END;

-- Insert Record
CREATE PROCEDURE insert_record
    @table_name VARCHAR(255),
    @columns VARCHAR(MAX),
    @values VARCHAR(MAX)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'INSERT INTO ' + QUOTENAME(@table_name) + 
               N' (' + @columns + ') VALUES (' + @values + ') RETURNING id';
    
    EXEC sp_executesql @sql;
END;

-- Update Record
CREATE PROCEDURE update_record
    @table_name VARCHAR(255),
    @set_clause VARCHAR(MAX),
    @where_clause VARCHAR(MAX)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'UPDATE ' + QUOTENAME(@table_name) + 
               N' SET ' + @set_clause + 
               N' WHERE ' + @where_clause;
    
    EXEC sp_executesql @sql;
END;

-- Soft Delete Record
CREATE PROCEDURE soft_delete_record
    @table_name VARCHAR(255),
    @id uuid
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'UPDATE ' + QUOTENAME(@table_name) + 
               N' SET deleted_date_time = NOW() WHERE id = @id';
    
    EXEC sp_executesql @sql, N'@id uuid', @id;
END;

-- Soft Delete Records By Column
CREATE PROCEDURE soft_delete_records_by_column
    @table_name VARCHAR(255),
    @column_name VARCHAR(255),
    @value VARCHAR(255)
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    
    SET @sql = N'UPDATE ' + QUOTENAME(@table_name) + 
               N' SET deleted_date_time = NOW() WHERE ' + QUOTENAME(@column_name) + N' = @value';
    
    EXEC sp_executesql @sql, N'@value VARCHAR(255)', @value;
END;