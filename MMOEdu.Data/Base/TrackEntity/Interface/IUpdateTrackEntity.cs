namespace EduOMO.Data.Base;

public interface IUpdateTrackEntity
{
    public string? UpdatedBy { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
