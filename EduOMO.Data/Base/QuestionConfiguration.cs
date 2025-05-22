using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MMOEdu.Data;

namespace EduOMO.Data.Base;

public class QuestionConfiguration : IEntityTypeConfiguration<QuestionEntity>
{
    public void Configure(EntityTypeBuilder<QuestionEntity> builder)
    {
        builder.OwnsMany(a => a.Answers, answer =>
        {
            answer.ToTable("Answer");
            answer.WithOwner().HasForeignKey(nameof(AnswerEntity.QuestionId));
        });
    }
}
