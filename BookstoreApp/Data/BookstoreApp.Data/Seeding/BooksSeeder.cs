namespace BookstoreApp.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BookstoreApp.Data.Models;

    public class BooksSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Books.Any())
            {
                return;
            }

            await dbContext.Books.AddAsync(new Book
            {
                Title = "And Then There Were None",
                Description = "Ten people, each with something to hide and something to fear, are invited to an isolated mansion on Indian Island by a host who, surprisingly, fails to appear. On the island they are cut off from everything but each other and the inescapable shadows of their own past lives. One by one, the guests share the darkest secrets of their wicked pasts. And one by one, they die... Which among them is the killer and will any of them survive?",
                Pages = 272,
                Price = 14.99M,
                YearPublished = 1939,
                AuthorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Death on the Nile",
                Description = "The tranquility of a luxury cruise along the Nile was shattered by the discovery that Linnet Ridgeway had been shot through the head. She was young, stylish, and beautiful. A girl who had everything . . . until she lost her life.\r\n\r\nHercule Poirot recalled an earlier outburst by a fellow passenger: \"I'd like to put my dear little pistol against her head and just press the trigger.\" Yet under the searing heat of the Egyptian sun, nothing is ever quite what it seems.\r\n\r\nA sweeping mystery of love, jealousy, and betrayal, Death on the Nile is among Christie's most legendary and timeless works.",
                Pages = 288,
                Price = 15.29M,
                YearPublished = 1937,
                AuthorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Murder on the Orient Express",
                Description = "Just after midnight, the famous Orient Express is stopped in its tracks by a snowdrift. By morning, the millionaire Samuel Edward Ratchett lies dead in his compartment, stabbed a dozen times, his door locked from the inside. Without a shred of doubt, one of his fellow passengers is the murderer.\r\n\r\nIsolated by the storm, detective Hercule Poirot must find the killer among a dozen of the dead man's enemies, before the murderer decides to strike again.",
                Pages = 256,
                Price = 14.99M,
                YearPublished = 1934,
                AuthorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "The Murder of Roger Ackroyd",
                Description = "With its famously shocking ending, The Murder of Roger Ackroyd is one of Agatha Christie's greatest mysteries, and the book that changed her career.\r\n\r\nOne evening the wealthy Roger Ackroyd is discovered slumped in his armchair, a knife buried in his heart. It is the start of a murder case that spurs the inhabitants of the sleepy English village of King's Abbot to feverish speculation.\r\n\r\nThe local police are perplexed, but soon a recently retired Belgian detective, Hercule Poirot, joins the investigation. The truth he uncovers will shock even the most imaginative of the village gossips.",
                Pages = 312,
                Price = 14.99M,
                YearPublished = 1926,
                AuthorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "The Mysterious Affair at Styles",
                Description = "A refugee of the Great War, Poirot is settling in England near Styles Court, the country estate of his wealthy benefactress, the elderly Emily Inglethorp. When Emily is poisoned and the authorities are baffled, Poirot puts his prodigious sleuthing skills to work.\r\n\r\nSuspects are plentiful, including the victim's much younger husband, her resentful stepsons, her longtime hired companion, a young family friend working as a nurse, and a London specialist on poisons who just happens to be visiting the nearby village. All of them have secrets they are desperate to keep, but none can outwit Poirot as he navigates the ingenious red herrings and plot twists that earned Agatha Christie her well-deserved reputation as the queen of mystery.",
                Pages = 296,
                Price = 10.00M,
                YearPublished = 1920,
                AuthorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Murder in Mesopotamia",
                Description = "Inexplicably fearing for her life, a woman's \"nervous terrors\" prompt Hercule Poirot to investigate-only to discover that her fears aren't all in her head.",
                Pages = 288,
                Price = 15.49M,
                YearPublished = 1936,
                AuthorId = 1,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "A Time to Kill",
                Description = "In this searing courtroom drama, best-selling  author John Grisham probes the savage depths of  racial violence... as he delivers a compelling tale  of uncertain justice in a small southern  town...\r\nClanton, Mississippi. The life of a  ten-year-old girl is shattered by two drunken and  remorseless young man. The mostly white town reacts  with shock and horror at the inhuman crime. Until  her black father acquires an assault rifle -- and  takes justice into his own outraged hands.\r\n\r\nFor ten days, as burning crosses and the crack of  sniper fire spread through the streets of  Clanton, the nation sits spellbound as young defense  attorney Jake Brigance struggles to save his  client's life... and then his own...",
                Pages = 672,
                Price = 9.99M,
                YearPublished = 1989,
                AuthorId = 2,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "The Firm",
                Description = "For a young lawyer on the make, it was an offer he couldn't refuse: a position at a law firm where the bucks, billable hours, and benefits are over the top. It's a dream job for an up-and-comer--if he can overlook the uneasy feeling he gets at the office. Then an FBI investigation plunges the straight and narrow attorney into a nightmare of terror and intrigue, with no choice but to pit his wits, ethics, and legal skills against the firm's deadly secrets--if he hopes to stay alive ...\r\nSynopsis\r\nMitch McDeere made it to the top of his class at Harvard Law and had his choice of any firm in the country. But when he signed on with Bendini, Lambert & Locke of Memphis, he made a deadly mistake. Now with a new package. Reissue.",
                Pages = 544,
                Price = 9.99M,
                YearPublished = 1991,
                AuthorId = 2,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "The Pelican Brief",
                Description = "In suburban Georgetown a killer's Reeboks whisper  on the front floor of a posh home... In a seedy  D.C. porno house a patron is swiftly  garroted to death... The next day America learns  that two of its Supreme Court justices have been  assassinated. And in New Orleans, a young law  student prepares a legal brief... To Darby Shaw it was  no more than a legal shot in the dark, a brilliant  guess. To the Washington establishment it was  political dynamite. Suddenly Darby is witness to a  murder -- a murder intended for her. Going  underground, she finds there is only one person she can  trust -- an ambitious reporter after a newsbreak  hotter than Watergate -- to help her piece together the  deadly puzzle. Somewhere between the bayous of  Louisiana and the White House's inner sanctums, a  violent cover-up is being engineered. For somone has  read Darby's brief. Someone who will stop at  nothing to destroy the evidence of an unthinkable  crime.",
                Pages = 387,
                Price = 9.99M,
                YearPublished = 1993,
                AuthorId = 2,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Harry Potter and the Sorcerer’s Stone",
                Description = "Harry Potter has never been the star of a Quidditch team, scoring points while riding a broom far above the ground. He knows no spells, has never helped to hatch a dragon, and has never worn a cloak of invisibility.\r\nAll he knows is a miserable life with the Dursleys, his horrible aunt and uncle, and their abominable son, Dudley — a great big swollen spoiled bully. Harry's room is a closet at the foot of the stairs, and he hasn't had a birthday party in eleven years.\r\n\r\nBut all that is about to change when a mysterious letter arrives by owl messenger: a letter with an invitation to an incredible place that Harry — and anyone who reads about him — will find unforgettable.\r\n\r\nFor it's there that he finds not only friends, aerial sports, and magic in everything from classes to meals, but a great destiny that's been waiting for him...if Harry can survive the encounter.",
                Pages = 223,
                Price = 8.99M,
                YearPublished = 1997,
                AuthorId = 3,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Harry Potter and the Chamber of Secrets",
                Description = "The Dursleys were so mean and hideous that summer that all Harry Potter wanted was to get back to the Hogwarts School for Witchcraft and Wizardry. But just as he's packing his bags, Harry receives a warning from a strange, impish creature named Dobby who says that if Harry Potter returns to Hogwarts, disaster will strike.\r\n\r\nAnd strike it does. For in Harry's second year at Hogwarts, fresh torments and horrors arise, including an outrageously stuck-up new professor, Gilderoy Lockhart, a spirit named Moaning Myrtle who haunts the girls' bathroom, and the unwanted attentions of Ron Weasley's younger sister, Ginny.\r\n\r\nBut each of these seem minor annoyances when the real trouble begins, and someone - or something - starts turning Hogwarts students to stone. Could it be Draco Malfoy, a more poisonous rival than ever? Could it possibly be Hagrid, whose mysterious past is finally told? Or could it be the one everyone at Hogwarts most suspects... Harry Potter himself!",
                Pages = 251,
                Price = 8.99M,
                YearPublished = 1998,
                AuthorId = 3,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Harry Potter and the Prisoner of Azkaban",
                Description = "For twelve long years, the dread fortress of Azkaban held an infamous prisoner named Sirius Black. Convicted of killing thirteen people with a single curse, he was said to be the heir apparent to the Dark Lord, Voldemort. Now he has escaped, leaving only two clues as to where he might be headed: Harry Potter's defeat of You-Know-Who was Black's downfall as well. And the Azkaban guards heard Black muttering in his sleep, \"He's at Hogwarts...he's at Hogwarts.\" Harry Potter isn't safe, not even within the walls of his magical school, surrounded by his friends. Because on top of it all, there may well be a traitor in their midst.",
                Pages = 317,
                Price = 9.89M,
                YearPublished = 1999,
                AuthorId = 3,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Harry Potter and the Goblet of Fire",
                Description = "Harry Potter is midway through his training as a wizard and his coming of age. Harry wants to get away from the pernicious Dursleys and go to the International Quidditch Cup. He wants to find out about the mysterious event that's supposed to take place at Hogwarts this year, an event involving two other rival schools of magic, and a competition that hasn't happened for a hundred years. He wants to be a normal, fourteen-year-old wizard. But unfortunately for Harry Potter, he's not normal - even by wizarding standards. And in his case, different can be deadly.",
                Pages = 636,
                Price = 11.69M,
                YearPublished = 2000,
                AuthorId = 3,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Harry Potter and the Order of the Phoenix",
                Description = "In the richest installment yet of J. K. Rowling's seven-part story, Harry Potter confronts the unreliability of the very government of the magical world, and the impotence of the authorities at Hogwarts. Despite this (or perhaps because of it) Harry finds depth and strength in his friends, beyond what even he knew; boundless loyalty and unbearable sacrifice. Though thick runs the plot (as well as the spine), readers will race through these pages, and leave Hogwarts, like Harry, wishing only for the next train back.",
                Pages = 766,
                Price = 10.99M,
                YearPublished = 2003,
                AuthorId = 3,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Harry Potter and the Half-Blood Prince",
                Description = "The war against Voldemort is not going well; even Muggle governments are noticing. Ron scans the obituary pages of the Daily Prophet, looking for familiar names. Dumbledore is absent from Hogwarts for long stretches of time, and the Order of the Phoenix has already suffered losses.\r\nAnd yet...\r\n\r\nAs in all wars, life goes on. Sixth-year students learn to Apparate \u0097 and lose a few eyebrows in the process. The Weasley twins expand their business. Teenagers flirt and fight and fall in love. Classes are never straightforward, though Harry receives some extraordinary help from the mysterious Half-Blood Prince.\r\n\r\nSo it's the home front that takes center stage in the multilayered sixth installment of the story of Harry Potter. Here at Hogwarts, Harry will search for the full and complex story of the boy who became Lord Voldemort \u0097 and thereby find what may be his only vulnerability.",
                Pages = 607,
                Price = 12.99M,
                YearPublished = 2005,
                AuthorId = 3,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "Harry Potter and the Deathly Hallows",
                Description = "In Harry Potter and the Deathly Hallows, the seventh and final book in the epic tale of Harry Potter, Harry and Lord Voldemort each prepare for their ultimate encounter.\r\n\r\nVoldemort takes control of the Ministry of Magic, installs Severus Snape as headmaster at Hogwarts, and sends his Death Eaters across the country to wreak havoc and find Harry. Meanwhile, Harry, Ron, and Hermione embark on a desperate quest the length and breadth of Britain, trying to locate and destroy Voldemort’s four remaining Horcruxes, the magical objects in which he has hidden parts of his broken soul. They visit the Burrow, Grimmauld Place, the Ministry, Godric’s Hollow, Malfoy Manor, Diagon Alley…\r\n\r\nBut every time they solve one mystery, three more evolve—and not just about Voldemort, but about Dumbledore, and Harry’s own past, and three mysterious objects called the Deathly Hallows. The Hallows are literally things out of a children’s tale, which, if real, promise to make their possessor the “Master of Death;” and they ensnare Harry with their tantalizing claim of invulnerability.\r\n\r\nIt is only after a nigh-unbearable loss that he is brought back to his true purpose, and the trio returns to Hogwarts for the final breathtaking battle between the forces of good and evil. They fight the Death Eaters alongside members of the Order of the Phoenix, Dumbledore’s Army, the Weasley clan, and the full array of Hogwarts teachers and students.\r\n\r\nYet everything turns upon the moment the entire series has been building up to, the same meeting with which our story began: the moment when Harry and Voldemort face each other at last.",
                Pages = 607,
                Price = 13.99M,
                YearPublished = 2007,
                AuthorId = 3,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "The Lord of the Rings",
                Description = "One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them.\r\n\r\nIn ancient times the Rings of Power were crafted by the Elven-smiths, and Sauron, the Dark Lord, forged the One Ring, filling it with his own power so that he could rule all others. But the One Ring was taken from him, and though he sought it throughout Middle-earth, it remained lost to him. After many ages it fell by chance into the hands of the hobbit Bilbo Baggins.\r\n\r\nFrom Sauron's fastness in the Dark Tower of Mordor, his power spread far and wide. Sauron gathered all the Great Rings to him, but always he searched for the One Ring that would complete his dominion.\r\n\r\nWhen Bilbo reached his eleventy-first birthday he disappeared, bequeathing to his young cousin Frodo the Ruling Ring and a perilous quest: to journey across Middle-earth, deep into the shadow of the Dark Lord, and destroy the Ring by casting it into the Cracks of Doom.\r\n\r\nThe Lord of the Rings tells of the great quest undertaken by Frodo and the Fellowship of the Ring: Gandalf the Wizard; the hobbits Merry, Pippin, and Sam; Gimli the Dwarf; Legolas the Elf; Boromir of Gondor; and a tall, mysterious stranger called Strider.\r\n\r\nThis new edition is illustrated with J.R.R. Tolkien's own artwork, created as he wrote the original text. It will be packaged with the following features: shrink-wrapped for damage protection, a sewn hardback binding with a ribbon placemark, ink-sprayed edges displaying Tolkien's runes, two maps loosely tucked, and will be printed on FSC \"forest-friendly\" paper. The text will be printed in two colours with full-colour illustrations, and the dustjacket will be stamped in two foils with a circular die-cut. The U.S. edition published by Houghton Mifflin Harcourt will be printed alongside the editions to be sold in the United Kingdom and Australia/New Zealand by HarperCollins to ensure matching quality.",
                Pages = 1216,
                Price = 59.99M,
                YearPublished = 1954,
                AuthorId = 4,
            });

            await dbContext.Books.AddAsync(new Book
            {
                Title = "The Hobbit",
                Description = "A great modern classic and the prelude to The Lord of the Rings\r\n\r\nBilbo Baggins is a hobbit who enjoys a comfortable, unambitious life, rarely traveling any farther than his pantry or cellar. But his contentment is disturbed when the wizard Gandalf and a company of dwarves arrive on his doorstep one day to whisk him away on an adventure. They have launched a plot to raid the treasure hoard guarded by Smaug the Magnificent, a large and very dangerous dragon. Bilbo reluctantly joins their quest, unaware that on his journey to the Lonely Mountain he will encounter both a magic ring and a frightening creature known as Gollum.",
                Pages = 310,
                Price = 15.49M,
                YearPublished = 1937,
                AuthorId = 4,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
