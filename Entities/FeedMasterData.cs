using Microsoft.EntityFrameworkCore;

namespace StansAssociates_Backend.Entities
{
    public static class FeedMasterData
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            SeedRoleMasterData(modelBuilder);
            AddSuperAdminUser(modelBuilder);
        }


        public static void SeedRoleMasterData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "Staff"
                },
                new Role
                {
                    Id = 3,
                    Name = "Teacher"
                }
                );
        }


        public static void AddSuperAdminUser(ModelBuilder modelBuilder)
        {
            var date = new DateTime(2025, 07, 19);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    EmailId = "Admin@Admin.com",
                    PhoneNumber = "9999999999",
                    Name = "Admin",
                    Password = "p7D/ukHhRwG3KDJKcbfMlJqrZRNEeyxW1wKAFWbTHbI=",
                    Id = 1,
                    IsActive = true,
                    DOB = date,
                    Gender = "",
                    Street = "",
                    City = "",
                    State = "",
                    Country = "",
                    Pincode = "",
                    ProfilePicture = "",
                    CreatedDate = date,
                    UpdatedDate = date
                }
                );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    Id = 1,
                    RoleId = 1,
                    UserId = 1,
                }
                );
        }
    }
}
