namespace EduOMO.Data.Base;

public abstract class FullTrackEntity : IEntity<Guid>, IDeleteTrackEntity, ICreateTrackEntity, IUpdateTrackEntity
{
    public Guid Id { get; set; }
    public string? Slug { get; set; }
    public string? DescriptionOG { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
