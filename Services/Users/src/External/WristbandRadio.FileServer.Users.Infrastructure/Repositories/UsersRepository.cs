namespace WristbandRadio.FileServer.Users.Infrastructure.Repositories;

public class UsersRepository : GenericRepository<UserDto>, IUsersRepository
{
    public UsersRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }
}
