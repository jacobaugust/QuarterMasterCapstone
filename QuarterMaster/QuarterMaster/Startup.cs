using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

using QuarterMaster.Models;


[assembly: OwinStartupAttribute(typeof(QuarterMaster.Startup))]
namespace QuarterMaster
{
    public partial class Startup
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
            //SeedStocks();
        }
        private void SeedStocks()
        {
            //for (int i = 1; i < 517; i++)
            //{
            //    Stock seed = new Stock();
            //    seed.Id = i;
            //    seed.StockBasicId = i;
            //    seed.StockIncomeStatementId = i;
            //    seed.StockBalanceSheetId = i;
            //    seed.StockMetricsId = i;
            //    seed.EventId = i;
            //    context.stocks.Add(seed);
            //    context.SaveChanges();

            //}

        }
        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                        
                var user = new ApplicationUser();
                user.UserName = "jjahneke";
                user.Email = "jacobjahneke@gmail.com";

                string userPWD = "august";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role
            if (!roleManager.RoleExists("JuniorUserAccount"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "JuniorUser";
                roleManager.Create(role);
            }

            // creating Creating Employee role
            if (!roleManager.RoleExists("SeniorUserAccount"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SeniorUser";
                roleManager.Create(role);
            }

        }
        
    }
}
