namespace BookstoreApp.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using BookstoreApp.Data.Models;

    public class AuthorsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            await dbContext.Authors.AddAsync(new Author
            {
                FirstName = "Agatha",
                LastName = "Christie",
                ShortBiography = "The name \"Agatha Christie\" is nearly synonymous with upper-class British mysteries, for good reason. Christie (1890-1976) set the standard for the genre in more than 60 novels and dozens of short stories, creating two iconic detectives along the way: the fastidious Belgian Hercule Poirot, and the English spinster Jane Marple in the Miss Marple series. No one could match her knack for weaving clues into her stories. Widely considered her masterpiece, And Then There Were None has been adapted into a number of films.",
            });

            await dbContext.Authors.AddAsync(new Author
            {
                FirstName = "John",
                LastName = "Grisham",
                ShortBiography = "John Grisham is an American writer and former attorney and politician, best known for his popular legal thrillers. Grisham's first novel, A Time to Killl, was written and published in 1989 while he served in the House of Representatives in Mississippi. Many of his books have been adapted into films and television shows, including his first best seller, The Firm. In 2010, he started a book series for children, Theodore Boone, in which the main character provides legal advice to his classmates.",
            });

            await dbContext.Authors.AddAsync(new Author
            {
                FirstName = "J. K.",
                LastName = "Rowling",
                ShortBiography = "The author of the celebrated Harry Potter series, J. K. Rowling is one of the world's most successful authors. Her books have created a fantastic world — filled with wizards and muggles — that has completely revitalized a love of reading in both kids and adults. In addition to books, the Harry Potter series includes the play Harry Potter and the Cursed Child, which debuted on the London stage to a sold-out audience. Rowling has also published the novel A Casual Vacancy and several books in the Cormoran Strike series under the pen name Robert Galbraith.",
            });

            await dbContext.Authors.AddAsync(new Author
            {
                FirstName = "J. R. R.",
                LastName = "Tolkien",
                ShortBiography = "J. R. R. Tolkien (1892-1973) was an English writer, poet, philologist, and university professor who is best known as the author of the classic high-fantasy books The Hobbit and The Lord of the Rings series (The Fellowship of the Ring, The Two Towers, and The Return of the King). After his death, his son Christopher Tolkien published books based on Tolkien's notes and unpublished manuscripts, including The Silmarillion.",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
