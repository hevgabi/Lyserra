using System;
using System.Threading;
using Lyserra.PlayerAndAttributes;

namespace Lyserra.Game
{
    public class MainMenu
    {
        public ConsoleHelper consoleHelper = new ConsoleHelper();
        Owner owner;
        
        public void displayMainMenu()
        {
            for (byte i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("Starting the game...".PadLeft((40 + "Starting the game...".Length) / 2));
                Thread.Sleep(500);
                Console.Clear();
                Thread.Sleep(500);
            }

            bool gameMenuActive = true;
            while (gameMenuActive)
            {
                string choice;
                char option;
                do
                {
                    Console.Clear();
                    string title = "MAIN MENU";
                    Console.Write("==============================" +
                        "\n" + title.PadLeft((30 + title.Length) / 2) +
                        "\n==============================" +
                        "\n [0] Create New Pet" +
                        "\n [1] Load Pet" +
                        "\n [2] View Pet Status" +
                        "\n [3] Campaign" +
                        "\n [4] Credits" +
                        "\n [5] Exit" +
                        "\n==============================\n");
                    choice = consoleHelper.getInput("Select Option: ");
                    option = choice[0];
                } while (choice.Equals("empty"));

                switch (option)
                {
                    case '0':
                        Console.Clear();
                        owner = new Owner(consoleHelper.getInput("Enter Owner's Name: "));
                        Console.Clear();

                        consoleHelper.showMessage(owner.returnName());
                        Console.Clear();

                        string petName = consoleHelper.getInput("Enter Pet's Name: ");
                        Console.Clear();

                        consoleHelper.showMessage("Let's create your pet!");
                        Console.Clear();



                        break;

                    case '1':
                        /*Console.Clear();
                        string line = new string('=', 40);
                        Console.WriteLine(line);
                        Console.WriteLine("PET INFO".PadLeft((40 + "PET INFO".Length) / 2));
                        Console.WriteLine(line);

                        string[] labels = { "Name", "Breed", "Hair Color", "Haircut", "Eye Type", "Eye Color", "Special Eye", "Accessory", "Personality", "Scent", "Mutation", "Health" };
                        for (int i = 0; i < petData.Count; i++)
                            Console.WriteLine($"{labels[i]}: {petData[i]}".PadLeft((40 + $"{labels[i]}: {petData[i]}".Length) / 2));

                        Console.WriteLine(line);
                        Console.ReadLine();*/
                        break;

                    case '2':
                        break;

                    case '3':
                        
                        break;

                    case '4':
                        /*Console.Clear();
                        line = new string('=', 40);
                        Console.WriteLine(line);
                        Console.WriteLine("CREDITS".PadLeft((40 + "CREDITS".Length) / 2));
                        Console.WriteLine(line);
                        Console.WriteLine("Programmers/Directors:".PadLeft((40 + 23) / 2));
                        Console.WriteLine("Fritz Gabriel M. Almene".PadLeft((40 + 24) / 2));
                        Console.WriteLine("Jhon Clifford Robion".PadLeft((40 + 21) / 2));
                        Console.WriteLine(line);
                        Console.WriteLine("Press Enter to return...".PadLeft((40 + 23) / 2));
                        Console.ReadLine();*/
                        break;

                    case '5':
                        gameMenuActive = false;
                        break;
                }
            }
        }
    }
}
