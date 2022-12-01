namespace BookstoreApp.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Models;

    internal class GenresSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Genres.Any())
            {
                return;
            }

            await dbContext.Genres.AddAsync(new Genre { Name = "Science Fiction" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Fantasy" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Dystopian" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Action" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Adventure" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Mystery" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Horror" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Thriller" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Suspense" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Historical" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Romance" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Contemporary" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Magical Realism" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Children’s" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Autobiography" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Biography" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Art & Photography" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Self-help" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Travel" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Humor" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Guide / How-to" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Religion & Spirituality" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Humanities & Social Sciences" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Parenting & Families" });
            await dbContext.Genres.AddAsync(new Genre { Name = "Science & Technology" });

            await dbContext.SaveChangesAsync();
        }
    }
}
