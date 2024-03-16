using Identity.Models;

namespace Identity.Seed;
public static class DefaultUser
{
    public static List<ApplicationUser> IdentityBasicUserList()
    {
        return new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    Id = Constants.SuperAdminUser,
                    UserName = "superadmin",
                    Email = "superadmin@gmail.com",
                    FirstName = "Aneesha",
                    LastName = "MP",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    // Password@123
                    PasswordHash = "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==",
                    NormalizedEmail= "SUPERADMIN@GMAIL.COM",
                    NormalizedUserName="SUPERADMIN"
                },
                new ApplicationUser
                {
                    Id = Constants.BasicUser,
                    UserName = "basicuser",
                    Email = "basicuser@gmail.com",
                    FirstName = "Basic",
                    LastName = "User",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    // Password@123
                    PasswordHash = "AQAAAAEAACcQAAAAEBLjouNqaeiVWbN0TbXUS3+ChW3d7aQIk/BQEkWBxlrdRRngp14b0BIH0Rp65qD6mA==",
                    NormalizedEmail= "BASICUSER@GMAIL.COM",
                    NormalizedUserName="BASICUSER"
                },
            };
    }
}
