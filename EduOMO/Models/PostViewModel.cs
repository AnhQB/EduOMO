using EduOMO.Data;

namespace EduOMO.Models;

public class PostViewModel : CommonDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Keyword { get; set; }
    public string? Document { get; set; }
}
