﻿using EduOMO.Data;
using EduOMO.Data.Base;
using MMOEdu.Data;

namespace EduOMO.Models;

public class QuestionViewModel : CommonDto
{
    public string? Content { get; set; }
    public List<AnswerResponseDto>? Answers { get; set; }
}

public class AnswerResponseDto : CommonDto
{
    public string? UserName { get; set; }
    public string? Content { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class QuestionReferenceModel : CommonDto
{
    public string? Content { get; set; }
}

public class QuestionRequest : CommonDto
{
    public string? Content { get; set; }
    public List<string> Answers { get; set; } = [];
}