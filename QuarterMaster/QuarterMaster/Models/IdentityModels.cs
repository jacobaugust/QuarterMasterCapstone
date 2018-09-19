using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuarterMaster.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserRoles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();

        }
        public DbSet<JuniorUserAccount> juniorUserAccounts { get; set; }
        public DbSet<SeniorUserAccount> seniorUserAccounts { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Email> emails { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<StockBasic> stockBasics { get; set; }
        public DbSet<StockIncomeStatement> stockIncomeStatements { get; set; }
        public DbSet<StockBalanceSheet> stockBalanceSheets { get; set; }
        public DbSet<StockMetrics> stockMetrics { get; set; }
        public DbSet<WatchList> watchLists { get; set; }
        public DbSet<HomePage> homePages { get; set; }
        //public DbSet<Datapoint> datapoints { get; set; }
    }
}