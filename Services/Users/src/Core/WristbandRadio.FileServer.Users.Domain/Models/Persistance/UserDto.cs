namespace WristbandRadio.FileServer.Users.Domain.Models.Persistance;

[TableName("users.users")]
public class UserDto : IDbEntity
{
    [PrimaryKey]
    [ColumnName("id")]
    public Guid Id { get; set; }
    [ColumnName("username")]
    public string Username { get; set; }
    [ColumnName("email")]
    public string Email { get; set; }
    [ColumnName("first_name")]
    public string FirstName { get; set; }
    [ColumnName("last_name")]
    public string LastName { get; set; }
    [ColumnName("created_by")]
    public Guid CreatedBy { get; set; }
    [ColumnName("created_datetime")]
    public DateTime? CreatedDatetime { get; set; }

    public UserDto()
    {
    }
}