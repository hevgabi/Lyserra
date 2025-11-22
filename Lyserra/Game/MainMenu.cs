using System;
using System.Threading;
using Lyserra.PlayerAndAttributes;
using System.Collections.Generic;

namespace Lyserra.Game
{
    public class MainMenu
    {
        //nag commit na ako pre
        public ConsoleHelper consoleHelper = new ConsoleHelper();
        Owner owner;
        private Attributes attributes = new Attributes();

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

                        consoleHelper.showMessage($"Welcome, {owner.returnName()}!");
                        Console.Clear();

                        string petName = consoleHelper.getInput("Enter Pet's Name: ");
                        Console.Clear();

                        consoleHelper.showMessage($"Let's create your pet '{petName}'!");
                        Console.Clear();

                        StartPetCustomizationFlow(owner, petName);
                        break;

                    case '1':
                        Console.Clear();
                        consoleHelper.showMessage("Load Pet feature is not yet implemented.", 2000);
                        Console.Clear();
                        break;

                    case '2':
                        Console.Clear();
                        consoleHelper.showMessage("View Pet Status feature is not yet implemented.", 2000);
                        Console.Clear();
                        break;

                    case '3':

                        break;

                    case '4':
                        Console.Clear();
                        string line = new string('=', 40);
                        Console.WriteLine(line);
                        Console.WriteLine("CREDITS".PadLeft((40 + "CREDITS".Length) / 2));
                        Console.WriteLine(line);
                        Console.WriteLine("Programmers/Directors:".PadLeft((40 + 23) / 2));
                        Console.WriteLine("Fritz Gabriel M. Almene".PadLeft((40 + 24) / 2));
                        Console.WriteLine("Jhon Clifford Robion".PadLeft((40 + 21) / 2));
                        Console.WriteLine(line);
                        Console.WriteLine("Press Enter to return...".PadLeft((40 + 23) / 2));
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case '5':
                        gameMenuActive = false;
                        break;
                }
            }
        }

        private void StartPetCustomizationFlow(Owner owner, string petName)
        {
            string[] ownerTypes = new string[]
            {
                "Tita with over decorated bags",
                "Rich kid",
                "College student na payat",
                "Gym bro",
                "Sigma lolo",
                "Delulu na couple",
                "Swerteng kumag"
            };
            char ownerTypeChoice = consoleHelper.getMenuChoice("Select Owner Type", ownerTypes);
            int ownerTypeIndex = Char.IsDigit(ownerTypeChoice) ? ownerTypeChoice - '0' : 0;
            string ownerType = SafePick(ownerTypes, ownerTypeIndex);

            string[] petTypes = new string[] { "Dog", "Cat" };
            char petTypeChoice = consoleHelper.getMenuChoice("Select Pet Type", petTypes);
            int petTypeIndex = Char.IsDigit(petTypeChoice) ? petTypeChoice - '0' : 0;
            string petType = SafePick(petTypes, petTypeIndex);

            List<string> breedList = attributes.GetBreed(petType);
            char breedChoice = consoleHelper.getMenuChoice("Select Pet Breed", breedList.ToArray());
            int breedIndex = Char.IsDigit(breedChoice) ? breedChoice - '0' : 0;
            string breed = SafePick(breedList.ToArray(), breedIndex);

            char hairColorChoice = consoleHelper.getMenuChoice("Select Hair Color", attributes.hairColor.ToArray());
            int hairColorIndex = Char.IsDigit(hairColorChoice) ? hairColorChoice - '0' : 0;
            string hairColor = SafePick(attributes.hairColor.ToArray(), hairColorIndex);

            short weight = PromptForShort("Enter Pet Weight (kg): ");
            byte age = PromptForByte("Enter Pet Age (years): ");

            char hairCutChoice = consoleHelper.getMenuChoice("Select Pet Cut", attributes.hairCut.ToArray());
            int hairCutIndex = Char.IsDigit(hairCutChoice) ? hairCutChoice - '0' : 0;
            string hairCut = SafePick(attributes.hairCut.ToArray(), hairCutIndex);

            char colorTypeChoice = consoleHelper.getMenuChoice("Select Pet Color Type", attributes.colorEType.ToArray());
            int colorTypeIndex = Char.IsDigit(colorTypeChoice) ? colorTypeChoice - '0' : 0;
            string colorType = SafePick(attributes.colorEType.ToArray(), colorTypeIndex);

            char eyeColorChoice = consoleHelper.getMenuChoice("Select Eye Color", attributes.colorEye.ToArray());
            int eyeColorIndex = Char.IsDigit(eyeColorChoice) ? eyeColorChoice - '0' : 0;
            string eyeColor = SafePick(attributes.colorEye.ToArray(), eyeColorIndex);

            string[] yesNo = new string[] { "No", "Yes" };
            char specialAsk = consoleHelper.getMenuChoice("Add Special Eye? ", yesNo);
            int specialAskIndex = Char.IsDigit(specialAsk) ? specialAsk - '0' : 0;
            string specialEye = "None";
            if (SafePick(yesNo, specialAskIndex) == "Yes")
            {
                char specialChoice = consoleHelper.getMenuChoice("Select Special Eye", attributes.specialEye.ToArray());
                int specialIndex = Char.IsDigit(specialChoice) ? specialChoice - '0' : 0;
                specialEye = SafePick(attributes.specialEye.ToArray(), specialIndex);
            }

            char accessoryChoice = consoleHelper.getMenuChoice("Select Accessory", attributes.accessory.ToArray());
            int accessoryIndex = Char.IsDigit(accessoryChoice) ? accessoryChoice - '0' : 0;
            string accessory = SafePick(attributes.accessory.ToArray(), accessoryIndex);

            char personalityChoice = consoleHelper.getMenuChoice("Select Personality", attributes.personality.ToArray());
            int personalityIndex = Char.IsDigit(personalityChoice) ? personalityChoice - '0' : 0;
            string personality = SafePick(attributes.personality.ToArray(), personalityIndex);

            char scentChoice = consoleHelper.getMenuChoice("Select Scent / Shampoo", attributes.scent.ToArray());
            int scentIndex = Char.IsDigit(scentChoice) ? scentChoice - '0' : 0;
            string scent = SafePick(attributes.scent.ToArray(), scentIndex);

            char mutationChoice = consoleHelper.getMenuChoice("Select Mutation", attributes.mutation.ToArray());
            int mutationIndex = Char.IsDigit(mutationChoice) ? mutationChoice - '0' : 0;
            string mutation = SafePick(attributes.mutation.ToArray(), mutationIndex);

            char healthMainChoice = consoleHelper.getMenuChoice("Health Menu", attributes.healthStatusMainMenu.ToArray());
            int healthMainIndex = Char.IsDigit(healthMainChoice) ? healthMainChoice - '0' : 0;
            string healthMain = SafePick(attributes.healthStatusMainMenu.ToArray(), healthMainIndex);

            char healthPartChoice = consoleHelper.getMenuChoice("Select Body Section", attributes.healthStatusMenu.ToArray());
            int healthPartIndex = Char.IsDigit(healthPartChoice) ? healthPartChoice - '0' : 0;
            string healthPart = SafePick(attributes.healthStatusMenu.ToArray(), healthPartIndex);

            string summary = BuildSummary(owner.returnName(), ownerType, petName, petType, breed, hairColor, weight, age,
                hairCut, colorType, eyeColor, specialEye, accessory, personality, scent, mutation, healthMain, healthPart);

            if (petType == "Dog")
            {
                Dog dog = new Dog(petName);
                dog.showDisplay();
            }
            else
            {
                Cat cat = new Cat(petName, weight, age);
                cat.showDisplay();
            }

            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("PET SUMMARY".PadLeft((40 + "PET SUMMARY".Length) / 2));
            Console.WriteLine(new string('=', 40));
            Console.WriteLine(summary);
            Console.WriteLine("\nPress Enter to return to main menu...");
            Console.ReadLine();
            Console.Clear();
        }

        private static string SafePick(string[] arr, int idx)
        {
            if (arr == null || arr.Length == 0) return string.Empty;
            if (idx < 0) idx = 0;
            if (idx >= arr.Length) idx = arr.Length - 1;
            return arr[idx];
        }

        private static short PromptForShort(string prompt)
        {
            short value;
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (!short.TryParse(input, out value) || value < 0);
            return value;
        }

        private static byte PromptForByte(string prompt)
        {
            byte value;
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (!byte.TryParse(input, out value));
            return value;
        }

        private static string BuildSummary(string ownerName, string ownerType, string petName, string petType, string breed,
            string hairColor, short weight, byte age, string hairCut, string colorType, string eyeColor, string specialEye,
            string accessory, string personality, string scent, string mutation, string healthMain, string healthPart)
        {
            return
                $"Owner: {ownerName} ({ownerType})\n" +
                $"Pet Name: {petName}\n" +
                $"Type: {petType}\n" +
                $"Breed: {breed}\n" +
                $"Hair Color: {hairColor}\n" +
                $"Weight: {weight} kg, Age: {age} yrs\n" +
                $"Cut: {hairCut}\n" +
                $"Color Type: {colorType}\n" +
                $"Eye Color: {eyeColor} (Special: {specialEye})\n" +
                $"Accessory: {accessory}\n" +
                $"Personality: {personality}\n" +
                $"Scent: {scent}\n" +
                $"Mutation: {mutation}\n" +
                $"Health Menu: {healthMain} -> {healthPart}";
        }
    }
}
