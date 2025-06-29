using EduOMO.Data.Base;

namespace MMOEdu.Data;
public class QuestionEntity : FullTrackEntity
{
    public string? Content { get; set; }
    public List<AnswerEntity> Answers { get; set; } = [];
}
