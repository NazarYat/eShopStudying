using eShopStudying.DataAccess.Repository.IRepository;
using eShopStudying.Models;
using eShopStudying.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eShopStudying.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SQLDBContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            SQLDBContext db
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee));
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp));
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi));

                _userManager.CreateAsync(new ApplicationUser() {
                    UserName = "Admin",
                    Email = "admin@gmail.com",
                    Name = "My Name",
                    PhoneNumber = "123456789",
                    StreetAddress = "Street",
                    State = "NY",
                    PostalCode = "123456",
                    City = "New-York"
                }, "qwertyuioP1$").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}