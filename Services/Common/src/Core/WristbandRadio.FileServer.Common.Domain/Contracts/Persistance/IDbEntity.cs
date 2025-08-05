namespace WristbandRadio.FileServer.Common.Domain.Contracts.Persistance;

public interface IDbEntity
{
    public string Id { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public DateTime? DeletedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string? DeletedBy { get; set; }
}
