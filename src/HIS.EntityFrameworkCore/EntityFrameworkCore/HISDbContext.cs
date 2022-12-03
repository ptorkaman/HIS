using HIS.AdministrativeGenders;
using HIS.AdministrativeSexes;
using HIS.Authors;
using HIS.ContactModes;
using HIS.Countries;
using HIS.Educations;
using HIS.Facilities;
using HIS.IdentifierTypes;
using HIS.InActiveReasons;
using HIS.Languages;
using HIS.MaritalStatuses;
using HIS.NamePrefixes;
using HIS.Nationalities;
using HIS.Races;
using HIS.Relationships;
using HIS.Religions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace HIS.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class HISDbContext :
    AbpDbContext<HISDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public DbSet<Author> Authors { get; set; }
    public DbSet<AdministrativeSex> AdministrativeSexes { get; set; }
    public DbSet<NamePrefix> NamePrefixes { get; set; }
    public DbSet<IdentifierType> IdentifierTypes { get; set; }
    public DbSet<AdministrativeGender> AdministrativeGenders { get; set; }
    public DbSet<ContactMode> ContactModes { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<MaritalStatus> MaritalStatuses { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Nationality> Nationalities { get; set; }
    public DbSet<Relationship> Relationships { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<InActiveReason> InActiveReasons { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Religion> Religions { get; set; }
    public HISDbContext(DbContextOptions<HISDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(HISConsts.DbTablePrefix + "YourEntities", HISConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<Author>(b =>
        {
            b.ToTable(HISConsts.DbTablePrefix + "Authors",
                HISConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(64);

            b.HasIndex(x => x.Name);
        });
    }
}
