namespace EduOMO.Data.Base;

public abstract class CreateTrackEntity : IEntity<Guid>, ICreateTrackEntity
{
    public Guid Id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
