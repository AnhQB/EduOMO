using Microsoft.EntityFrameworkCore;
using MMOEdu.Data;

namespace EduOMO.Data.Base;

public class EduOMOContext : DbContext
{
    public EduOMOContext(DbContextOptions<EduOMOContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
    }

    public DbSet<PostEntity> Post {  get; set; }
    public DbSet<QuestionEntity> Question {  get; set; }
}
