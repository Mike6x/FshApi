using Finbuckle.MultiTenant;
using FSH.WebApi.Application.Common.Events;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Domain.Communication;
using FSH.WebApi.Domain.Elearning;
using FSH.WebApi.Domain.Geo;
using FSH.WebApi.Domain.Organization;
using FSH.WebApi.Domain.People;
using FSH.WebApi.Domain.Place;
using FSH.WebApi.Domain.Price;
using FSH.WebApi.Domain.Production;
using FSH.WebApi.Domain.Property;
using FSH.WebApi.Domain.Purchase;
using FSH.WebApi.Domain.Settings;
using FSH.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FSH.WebApi.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    #region Settings DbContext
    public DbSet<Menu> Menus => Set<Menu>();

    #endregion

    #region Geo DbContext
    public DbSet<GeoAdminUnit> GeoAdminUnits => Set<GeoAdminUnit>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<State> States => Set<State>();
    public DbSet<Province> Provinces => Set<Province>();
    public DbSet<District> Districts => Set<District>();
    public DbSet<Ward> Wards => Set<Ward>();

    #endregion

    #region Catalog DbContext
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<BusinessLine> BusinessLines => Set<BusinessLine>();
    public DbSet<GroupCategorie> GroupCategories => Set<GroupCategorie>();
    public DbSet<Categorie> Categories => Set<Categorie>();
    public DbSet<SubCategorie> SubCategories => Set<SubCategorie>();

    #endregion

    #region Property DbContext
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<AssetStatus> AssetStatuses => Set<AssetStatus>();
    public DbSet<AssetHistory> AssetHistorys => Set<AssetHistory>();
    #endregion

    #region Production DbContext
    public DbSet<Product> Products => Set<Product>();

    #endregion

    #region Price DbContext
    public DbSet<PriceGroup> PriceGroups => Set<PriceGroup>();
    public DbSet<PricePlan> PricePlans => Set<PricePlan>();

    #endregion

    #region Place DbContext
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<Retailer> Retailers => Set<Retailer>();
    public DbSet<Store> Stores => Set<Store>();

    #endregion

    #region Elearning DbContext
    public DbSet<Quiz> Quizs => Set<Quiz>();
    public DbSet<QuizResult> QuizResults => Set<QuizResult>();

    #endregion

    #region Ogarnization Chart DbContext
    public DbSet<BusinessUnit> BusinessUnits => Set<BusinessUnit>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<SubDepartment> SubDepartments => Set<SubDepartment>();
    public DbSet<Team> Teams => Set<Team>();

    #endregion

    #region People DbContext
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Title> Titles => Set<Title>();

    #endregion

    #region Purchase DbContext
    public DbSet<Vendor> Vendors => Set<Vendor>();

    #endregion

    #region Chat DbContext
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();

    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}