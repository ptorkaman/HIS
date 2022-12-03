using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HIS.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class HISDbContextFactory : IDesignTimeDbContextFactory<HISDbContext>
{
    public HISDbContext CreateDbContext(string[] args)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        HISEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<HISDbContext>()
            .UseNpgsql(configuration.GetConnectionString("Default"));

        return new HISDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../HIS.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
