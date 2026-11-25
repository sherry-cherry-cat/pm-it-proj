using Microsoft.EntityFrameworkCore;

namespace PasswordVault.Infrastructure;

internal sealed class VaultDbContext(DbContextOptions<VaultDbContext> options) : DbContext(options)
{
    public DbSet<VaultMetadata> Metadata => Set<VaultMetadata>();
    public DbSet<VaultRecord> Records => Set<VaultRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VaultMetadata>().HasKey(m => m.Id);
        modelBuilder.Entity<VaultRecord>().HasKey(r => r.Id);
    }
}

internal sealed class VaultMetadata
{
    public int Id { get; set; } = 1;
    public string HeaderJson { get; set; } = "";
}

internal sealed class VaultRecord
{
    public string Id { get; set; } = "";
    public string EncryptedJson { get; set; } = "";
}
