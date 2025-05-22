namespace EduOMO.Data.Base;

public abstract class UpdateTrackEntity : IEntity<Guid>, IUpdateTrackEntity
{
    public Guid Id { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
