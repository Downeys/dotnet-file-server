namespace WristbandRadio.FileServer.Submissions.Infrastructure.Repositories;

public class ImageLinkRepository : GenericRepository<ImageLinkDto>, IImageLinkRepository
{
    public ImageLinkRepository(DapperDataContext dapperDataContext) : base(dapperDataContext)
    {
    }
}
