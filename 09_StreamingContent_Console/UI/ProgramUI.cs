using _07_RepositoryPattern_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_StreamingContent_Console.ProgramUI
{
    class ProgramInt
    {
        private readonly StreamingContent_Repo _repo = new StreamingContent_Repo();
        public void Run()
        {
            RunMenu();
        }
        private void RunMenu()
        {
            bool continueToRun = true;
            while(continueToRun)
            {

                Console.Clear();
            
                Console.WriteLine("Enter the number of the option you'd like to select:\n" +
                "1. Show all streaming content\n" +
                "2. Find all streaming content by title\n" +
                "3. Add new streaming content\n" +
                "4. Remove streaming content\n" +
                "5. Exit");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        /*Console.WriteLine("Star wars");
                        Console.ReadKey();*/
                        ShowAllContent();
                        break;
                    case "2":
                        ShowAllContentByTitle();
                        break;
                    case "3":
                        CreateNewContent();
                        break;
                    case "4":
                        RemoveContentFromList();
                        break;
                    case "5":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number between 1 and 5.\n");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CreateNewContent()
        {
            Console.Clear();

            StreamingContent content = new StreamingContent();

            Console.WriteLine("PLease enter a title: ");
            content.Title = Console.ReadLine();

            Console.WriteLine("PLease enter a description: ");
            content.Description = Console.ReadLine();

            Console.WriteLine("PLease enter the star rating (1-5): ");
            content.StarRating = double.Parse(Console.ReadLine());

            Console.WriteLine("Select a maturity rating: \n" +
               "1. G\n" +
               "2. PG\n" +
               "3. R\n" +
               "4. PG13\n" +
               "5. NC17\n" +
               "6. TVY\n" +
               "7. TVY7\n" +
               "8. TVMA\n" +
               "9. TVPG");

            string maturityString = Console.ReadLine();

            int maturityID = int.Parse(maturityString);

            content.MaturityRating = (Maturity)maturityID;

            Console.WriteLine("Select a genre:\n" +
                "1. Horror\n" +
                "2. Comedy\n" +
                "3. SciFi\n" +
                "4. Drama\n" +
                "5. Romance\n" +
                "6. Romans\n" +
                "7. Action\n" +
                "8. International");

            string genreInput = Console.ReadLine();

            int genreID = int.Parse(genreInput);

            content.GenreType = (GenreType)genreID;

            _repo.AddContentToDirectory(content);
        }

        private void ShowAllContent()
        {
            Console.Clear();

            List<StreamingContent> listOfContent = _repo.GetContents();

            foreach (StreamingContent content in listOfContent)
            {
                DisplayContent(content);
            }
            Console.ReadKey();
        }

        private void DisplayContent(StreamingContent content)
        {
            Console.WriteLine($"Title: {content.Title}\n" + 
                $"Description: {content.Description}\n" +
                $"Genre: {content.GenreType}\n" +
                $"Stars: {content.StarRating}\n" +
                $"Family Friendly: {content.IsFamilyFriendly}\n" +
                $"Rating: {content.MaturityRating}\n");
        }

        private void ShowAllContentByTitle()
        {
            Console.Clear();

            Console.WriteLine("Enter a Title: ");

            string title = Console.ReadLine();

            StreamingContent content = _repo.GetContentByTitle(title);

            if(content != null)
            {
                DisplayContent(content);
            }
            else
            {
                Console.WriteLine("Invalid title. Could not find any results.");
            }

            Console.ReadKey();
        }
        private void RemoveContentFromList()
        {
            Console.Clear();

            Console.WriteLine("Which item would you like to remove?");

            List<StreamingContent> contentList = _repo.GetContents();

            int count = 0;

            foreach (StreamingContent content in contentList)
            {
                count++;
                Console.WriteLine($"{count}. {content.Title}");
            }

            int targetContentId = int.Parse(Console.ReadLine());
            int targetIndex = targetContentId - 1;

            if (targetIndex >= 0 && targetIndex < contentList.Count)
            {
                StreamingContent desiredContent = contentList[targetIndex];

                if (_repo.DeleteContent(desiredContent.Title))
                {
                    Console.WriteLine($"{desiredContent.Title} successfully removed.");
                }
                else
                {
                    Console.WriteLine("I'm sorry, I can't do that.");
                }
            }
            else
            {
                Console.WriteLine("No content has that ID.");
            }
            Console.ReadKey();
            
        }
        private void SeedContentList()
        {
            StreamingContent starwar = new StreamingContent("Star Wars", "space", Maturity.R, 5.0, GenreType.Drama);
            StreamingContent startrek = new StreamingContent("Star Trek", "Also space", Maturity.R, 5.0, GenreType.Drama);

            _repo.AddContentToDirectory(starwar);
            _repo.AddContentToDirectory(startrek);
        }
    }
}
                
