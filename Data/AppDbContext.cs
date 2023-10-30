// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Document> Documents { get; set; }
public DbSet<PlosDocument> PlosDocuments { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=docs.db");
}
