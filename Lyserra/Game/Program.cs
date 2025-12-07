using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lyserra.Game.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            Animation animation = new Animation();
            ConsoleHelper consoleHelper = new ConsoleHelper();
            animation.logo();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();

            for (byte i = 0; i < 3; i++)
            {
                Console.Clear();
                animation.startingLogo();
                Thread.Sleep(500);
                Console.Clear();
                Thread.Sleep(500);
            }

            consoleHelper.showInstructions();

            mainMenu.displayMainMenu();

        }
    }
}
