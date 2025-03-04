using Microsoft.EntityFrameworkCore;
using WristbandRadio.FileServer.Submissions.Domain.Entities;

namespace WristbandRadio.FileServer.Submissions.Infrastructure.Contexts
{
    class MusicSubmissionContext : DbContext
    {
        public DbSet<MusicSubmission> MusicSubmissions { get; set; }
    }
}
