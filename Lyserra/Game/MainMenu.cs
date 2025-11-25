using System;
using System.Threading;
using Lyserra.PlayerAndAttributes;
using System.Collections.Generic;

namespace Lyserra.Game
{
    public class MainMenu
    {
        public ConsoleHelper consoleHelper = new ConsoleHelper();
        private Attributes attributes = new Attributes();
        Master master;
        private Dog dog;
        private Cat cat;

        public void displayMainMenu()
        {
            string[] mainMenuOptions = { "Create New Pet", "Load Pet", "View Pet Status", "Campaign", "Credits", "Exit" };

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
                char option = consoleHelper.getMenuChoice("MAIN MENU", mainMenuOptions);
                switch (option)
                {
                    case '0':
                        StartPetCustomizationFlow();
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
                        consoleHelper.showCampaign();
                        Console.Clear();
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
                        Console.Clear();
                        string exitLine = new string('=', 40);
                        Console.WriteLine(exitLine);
                        Console.WriteLine("Thank you for playing Lyserra!".PadLeft((40 + 30) / 2));
                        Console.WriteLine("Goodbye!".PadLeft((40 + 8) / 2));
                        Console.WriteLine(exitLine);
                        Thread.Sleep(2000);
                        break;

                    default:
                        Console.Clear();
                        consoleHelper.showMessage("Invalid option. Please try again.", 1500);
                        break;
                }
            }
        }

        private void StartPetCustomizationFlow()
        {
            try
            {
                Console.Clear();
                LyserraDB.initialize();
                master = new Master(consoleHelper.getName("Enter Master's Name: "));
                Console.Clear();

                master.MasterType = consoleHelper.pickType("Select Master Type", attributes.ownerTypes.ToArray());

                master.SpecialTrait = consoleHelper.pickType("Select Special Trait", attributes.specialTraits.ToArray());
                

                string[] petTypes = { "Dog", "Cat" };
                string petType = consoleHelper.pickType("Select Pet Type", petTypes);

                // Create pet object based on selected type
                if (petType.Equals("Dog"))
                {
                    dog = new Dog(consoleHelper.getName("Enter Pet's Name: "));
                    consoleHelper.showMessage($"Let's create your pet '{dog.Name}'!");
                    Console.Clear();
                }
                else
                {
                    cat = new Cat(consoleHelper.getName("Enter Pet's Name: "));
                    consoleHelper.showMessage($"Let's create your pet '{cat.Name}'!");
                    Console.Clear();
                }

                // Gather pet attributes
                List<string> breedList = attributes.GetBreed(petType);
                string breed = consoleHelper.pickType("Select Pet Breed", breedList.ToArray());

                // new: Weight selection
                string weight = consoleHelper.pickType("Select Pet Weight", attributes.weightCategories.ToArray());

                // new: Age selection
                string ageWithDesc = consoleHelper.pickType("Select Pet Age", attributes.ageCategories.ToArray());
                string age = attributes.GetAgeValue(ageWithDesc); //get actual age value

                string hairColor = consoleHelper.pickType("Select Hair Color", attributes.hairColor.ToArray());
                string hairCut = consoleHelper.pickType("Select Pet Cut", attributes.hairCut.ToArray());
                string colorType = consoleHelper.pickType("Select Pet Color Type", attributes.colorEType.ToArray());
                string eyeColor = consoleHelper.pickType("Select Eye Color", attributes.colorEye.ToArray());
                string accessory = consoleHelper.pickType("Select Accessory", attributes.accessory.ToArray());
                string personality = consoleHelper.pickType("Select Personality", attributes.personality.ToArray());
                string scent = consoleHelper.pickType("Select Scent", attributes.scent.ToArray());
                string mutation = consoleHelper.pickType("Select Mutation", attributes.mutation.ToArray());

                // new: Element selection
                string element = consoleHelper.pickType("Select Element", attributes.elements.ToArray());

                string healthMain = consoleHelper.pickType("Select Health Status", attributes.healthStatusMainMenu.ToArray());
                string healthPart = consoleHelper.pickType("Select Specific Health Issue", attributes.healthStatusMainMenu.ToArray());

                string petName = petType == "Dog" ? dog.Name : cat.Name;


                // assign attributes to pet object
                if (petType == "Dog")
                {
                    dog.Type = "Dog";
                    dog.MasterID = master.MasterID;
                    dog.Weight = weight;
                    dog.Age = age;
                    dog.Breed = breed;
                    dog.HairColor = hairColor;
                    dog.HairCut = hairCut;
                    dog.ColorDesign = colorType;
                    dog.EyeColor = eyeColor;
                    dog.Accessory = accessory;
                    dog.Personality = personality;
                    dog.Scent = scent;
                    dog.Mutation = mutation;
                    dog.Element = element;
                }
                else
                {
                    cat.Type = "Cat";
                    cat.MasterID = master.MasterID;
                    cat.Weight = weight;
                    cat.Age = age;
                    cat.Breed = breed;
                    cat.HairColor = hairColor;
                    cat.HairCut = hairCut;
                    cat.ColorDesign = colorType;
                    cat.EyeColor = eyeColor;
                    cat.Accessory = accessory;
                    cat.Personality = personality;
                    cat.Scent = scent;
                    cat.Mutation = mutation;
                    cat.Element = element;
                }

                // save to db
                LyserraDB.insertMaster(master);

                if (petType == "Dog")
                    LyserraDB.insertPet(dog);
                else
                    LyserraDB.insertPet(cat);

                consoleHelper.showMessage($"{petName} has been created successfully!", 2000);
            }
            catch (Exception ex)
            {
                Console.Clear();
                consoleHelper.showMessage($"Error during pet creation: {ex.Message}", 3000);
            }
        }
    }
}