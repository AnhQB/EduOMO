using EduOMO.Data.Base;
using System.ComponentModel;

namespace MMOEdu.Data;
public class AnswerEntity : CreateTrackEntity
{
    public string? UserName { get; set; }
    public string? Content { get; set; }
    public Guid QuestionId { get; set; }
}
