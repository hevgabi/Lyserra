using Lyserra.PlayerAndAttributes;
using System;
using System.Threading;
using System.Xml.Linq;


namespace Lyserra.Game
{
    public class GameLoop
    {
        MainMenu mainMenu = new MainMenu();
        public Master master;
        public void startGame()
        {
            bool isGameRunning = true;
            while (isGameRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Lyserra Pet Simulator!");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        
                        mainMenu.displayMainMenu();
                        break;
                    case "2":
                        isGameRunning = false;
                        Console.WriteLine("Exiting the game. Goodbye!");
                        Thread.Sleep(2000);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Thread.Sleep(2000);
                        break;
                }
            }
}
    }
}
