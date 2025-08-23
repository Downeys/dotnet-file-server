-- Does Record Exist
CREATE OR REPLACE PROCEDURE does_record_exist(
    table_name VARCHAR(255),
    column_name VARCHAR(255),
    value VARCHAR(255))
LANGUAGE plpgsql 
AS $$
BEGIN  
    EXECUTE format('SELECT COUNT(*) FROM %I WHERE %I = %L', table_name, column_name, value);
END; $$;

-- Get Records
CREATE OR REPLACE PROCEDURE get_records(
    table_name VARCHAR(255),
    columns TEXT = NULL,
    page_number INT = 1,
    page_size INT = 10)
LANGUAGE plpgsql 
AS $$
DECLARE
    sql_format TEXT;
    column_names TEXT;
    previous_page_last_record INT;
BEGIN
    IF columns IS NULL THEN
        columns := '*';
    END IF;

    previous_page_last_record := (page_number-1)*page_size;
    
    sql_format := 'SELECT TOP (%L) %I FROM %I WHERE paging_order > %L AND deleted_date_time IS NULL ORDER BY paging_order';

    EXECUTE format(sql_format, page_size, columns, previous_page_last_record);
END; $$;

-- Get Records By Id
CREATE OR REPLACE PROCEDURE get_records_by_id(
    table_name VARCHAR(255),
    id uuid,
    columns TEXT = NULL)
LANGUAGE plpgsql 
AS $$
DECLARE
    sql_format TEXT;
BEGIN

    IF columns IS NULL THEN
        columns := '*';
    END IF;

    sql_format := 'SELECT %I FROM %I WHERE id = %L AND deleted_date_time IS NULL';

    EXECUTE format (sql_format, columns, table_name, id);
END; $$;

-- Get Records By Column
CREATE OR REPLACE PROCEDURE get_records_by_column(
    table_name VARCHAR(255),
    column_name VARCHAR(255),
    value VARCHAR(255),
    columns TEXT = NULL)
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN
    IF columns IS NULL THEN
        SET columns = '*';
    END IF;
    sql_format := 'SELECT %I FROM %I WHERE %I = %L AND deleted_date_time IS NULL';
    EXECUTE format(sql_format, columns, table_name, column_name, value);
END; $$;

-- Get User Roles
CREATE OR REPLACE PROCEDURE get_user_roles(user_id uuid)
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN
    sql_format := 'SELECT ur.*, r.role_name FROM users.user_roles ur ' +
               N'JOIN users.r_roles r ON ur.role_id = r.role_id ' +
               N'WHERE ur.user_id = %L AND ur.deleted_date_time IS NULL';
    
    EXECUTE format(sql_format, user_id);
END; $$;

-- Get Artist User Roles
CREATE OR REPLACE PROCEDURE get_artist_user_roles(user_id uuid)
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN    
    sql_format := N'SELECT aur.*, r.role_name FROM users.artist_user_roles aur ' +
               N'JOIN users.r_roles r ON aur.role_id = r.role_id ' +
               N'JOIN music.artists a ON aur.artist_id = a.artist_id ' +
               N'WHERE aur.user_id = %L AND aur.deleted_date_time IS NULL AND a.deleted_date_time IS NULL';
    
    EXECUTE format(sql_format, user_id);
END; $$;

-- Get Artist User Roles By Artist Id
CREATE OR REPLACE PROCEDURE get_artist_user_roles_by_artist_id(artist_id uuid)
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN    
    sql_format := N'SELECT aur.*, r.role_name FROM users.artist_user_roles aur ' +
               N'JOIN users.r_roles r ON aur.role_id = r.role_id ' +
               N'JOIN music.artists a ON aur.artist_id = a.artist_id ' +
               N'WHERE aur.artist_id = %L AND aur.deleted_date_time IS NULL AND a.deleted_date_time IS NULL';
    
    EXECUTE format(sql_format, artist_id);
END; $$;

-- GET Artist User Roles By Artist Id And User Id
CREATE OR REPLACE PROCEDURE get_artist_user_roles_by_artist_id_and_user_id(
    artist_id uuid,
    user_id uuid)
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN   
    sql_format := N'SELECT aur.*, r.role_name FROM users.artist_user_roles aur ' +
               N'JOIN users.r_roles r ON aur.role_id = r.role_id ' +
               N'JOIN music.artists a ON aur.artist_id = a.artist_id ' +
               N'WHERE aur.artist_id = %L AND aur.user_id = %L ' +
               N'AND aur.deleted_date_time IS NULL AND a.deleted_date_time IS NULL';
    
    EXECUTE format(sql_format, artist_id, user_id);
END; $$;

-- Insert Record
CREATE OR REPLACE PROCEDURE insert_record(
    table_name VARCHAR(255),
    columns TEXT,
    column_values TEXT)
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN
    sql_format := 'INSERT INTO %s (%s) VALUES (%s) RETURNING id';
    
    EXECUTE format(sql_format, table_name, columns, column_values);
END; $$;

-- Update Record
CREATE OR REPLACE PROCEDURE update_record(
    table_name VARCHAR(255),
    columns TEXT,
    id VARCHAR(22))
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN
    sql_format := 'UPDATE %I SET %I WHERE id = %L';

    EXECUTE format(sql_format, table_name, columns, id);
END; $$;

-- Soft Delete Record
CREATE OR REPLACE PROCEDURE soft_delete_record(
    table_name VARCHAR(255),
    id uuid)
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN
    sql_format := 'UPDATE %I SET deleted_date_time = NOW() WHERE id = %L';
    
    EXECUTE format(sql_format, table_name, id);
END; $$;

-- Soft Delete Records By Column
CREATE OR REPLACE PROCEDURE soft_delete_records_by_column(
    table_name VARCHAR(255),
    column_name VARCHAR(255),
    value VARCHAR(255))
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN
    sql_format := 'UPDATE %I SET deleted_date_time = NOW() WHERE %I = %L';
    
    EXECUTE format(sql_format, table_name, column_name, value);
END; $$;

-- Get Total Records Count
CREATE OR REPLACE PROCEDURE get_total_records_count(table_name VARCHAR(50))
LANGUAGE plpgsql
AS $$
DECLARE
    sql_format TEXT;
BEGIN
	sql_format := 'SELECT COUNT(Id) FROM %I WHERE deleted_date_time IS NULL';

	EXECUTE format(sql_format, table_name);
END; $$;