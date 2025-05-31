using EduOMO.Data;
using MMOEdu.Data;

namespace EduOMO.Models;

public class QuestionViewModel : CommonDto
{
    public string? Content { get; set; }
    public List<AnswerResponseDto>? Answers { get; set; }
}

public class AnswerResponseDto
{
    public string? UserName { get; set; }
    public string? Content { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
