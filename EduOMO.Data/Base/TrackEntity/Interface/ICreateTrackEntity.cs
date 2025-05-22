namespace EduOMO.Data.Base;

public interface ICreateTrackEntity
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
