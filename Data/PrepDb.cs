using RideShare.Models;

namespace RideShare.Data
{
    public static class PrepDb
    {
        public static void PrepTravel(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                 SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Travels.Any())
            {
                Console.WriteLine("--> Seeding Travel Data...");
                context.Travels.AddRange(
                    new Travel() {To="NY", From="TR", Date=DateTime.Now, Active=false, SeatNumber=10, Description="Seeded Trip 1.."},
                    new Travel() {To="FR", From="TR", Date=DateTime.Now, Active=false, SeatNumber=20, Description="Seeded Trip 2.."},
                    new Travel() {To="GR", From="TR", Date=DateTime.Now, Active=false, SeatNumber=30, Description="Seeded Trip 3.."}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Travel Data Already Exists.");
            }

            if (!context.Users.Any())
            {
                Console.WriteLine("--> Seeding User Data...");
                context.Users.AddRange(
                    new User() {Name = "Ferruh", Surname="TR"},
                    new User() {Name = "Ferhat", Surname="TR"},
                    new User() {Name = "Ferman", Surname="TR"}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> User Data Already Exists.");
            }
        }
    }
}