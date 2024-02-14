using Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
    {
    }

    public DbSet<FeedbackMessage> Feedbacks { get; set; }
    public DbSet<AzureLanguageAnalyseInfo> AzureLanguageAnalyseInfo { get; set; }
    public DbSet<AzureLanguageAnalyseSubjectOpinion> AzureLanguageAnalyseSubjectOpinion { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        modelBuilder.UseIdentityAlwaysColumns();
    }
}