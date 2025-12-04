using Lyserra.PlayerAndAttributes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Lyserra.Game
{
    public class MainMenu
    {
        public ConsoleHelper consoleHelper = new ConsoleHelper();
        private Attributes attributes = new Attributes();
        LyserraDB database = new LyserraDB("LyserraDatabase.db");
        Animation animation = new Animation();
        Master master;
        Dog dog;
        Cat cat;
        bool gameMenuActive = true;

        private void assignPetToGlobal(Pet pet)
        {
            if (pet.Type == "Dog")
                dog = (Dog)pet;
            else if (pet.Type == "Cat")
                cat = (Cat)pet;
        }

        public void displayMainMenu()
        {
            animation.logo();
            Console.ReadLine();
            gameMenuActive = true;

            for (byte i = 0; i < 3; i++)
            {
                Console.Clear();
                animation.startingLogo();
                Thread.Sleep(500);
                Console.Clear();
                Thread.Sleep(500);
            }

            masterMenu();

            string[] mainMenuOptions = { "Create New Pet", "Delete Pet", "Load Pet", "Change Master", "Campaign", "Credits", "Exit" };

            while (gameMenuActive)
            {
                Console.Clear();
                Master m = database.getMasterById(master.MasterID);
                string space = "         ";

                string choice = consoleHelper.pickType($"{space}MAIN MENU\nMaster: {m.MasterName}", mainMenuOptions);

                switch (choice)
                {
                    case "Create New Pet":
                        StartPetCustomizationFlow(master);
                        break;
                    case "Delete Pet":
                        deletePetFlow();
                        break;
                    case "Load Pet":
                        choosePetFlow(master.MasterID);
                        break;
                    case "Change Master":
                        masterMenu();
                        break;
                    case "Campaign":
                        consoleHelper.showCampaign();
                        break;
                    case "Credits":
                        showCredits();
                        break;
                    case "Exit":
                        gameMenuActive = false;
                        break;
                }
            }
        }

        private void masterMenu()
        {
            bool masterMenuActive = true;
            while (masterMenuActive)
            {
                string[] options = { "Create Master", "Choose Master", "Delete Master", "Exit" };
                string choice = consoleHelper.pickType("Master Menu", options);

                switch (choice)
                {
                    case "Create Master":
                        createMasterFlow();
                        masterMenuActive = false;
                        break;
                    case "Choose Master":
                        chooseMasterFlow();
                        masterMenuActive = false;
                        break;
                    case "Delete Master":
                        deleteMasterFlow();
                        masterMenuActive = false;
                        break;
                    case "Exit":
                        masterMenuActive = false;
                        break;
                }
            }
        }

        private void createMasterFlow()
        {
            Console.Clear();
            string masterName = consoleHelper.getName("Enter Master's Name: ");
            string type = consoleHelper.pickType("Select Master Type", attributes.ownerTypes.ToArray());
            string trait = consoleHelper.pickType("Select Special Trait", attributes.specialTraits.ToArray());

            Master newMaster = new Master(masterName)
            {
                MasterType = type,
                SpecialTrait = trait
            };

            long masterId = database.insertMaster(newMaster);
            newMaster.MasterID = masterId;
            master = newMaster;

            consoleHelper.showMessage($"Master {masterName} created!");
        }

        private void chooseMasterFlow()
        {
            Console.Clear();
            List<Master> list = database.getAllMasters();

            if (list.Count == 0)
            {
                consoleHelper.showMessage("No master found.");
                return;
            }

            string[] names = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                names[i] = $"{list[i].MasterID}: {list[i].MasterName}";

            string picked = consoleHelper.pickType("Choose Master", names);
            long masterID = long.Parse(picked.Split(':')[0]);

            Master chosenMaster = database.getMasterById(masterID);
            this.master = chosenMaster;
        }

        private void deleteMasterFlow()
        {
            Console.Clear();
            string search = consoleHelper.getName("Search master to delete: ");
            List<Master> list = database.searchMasters(search);

            if (list.Count == 0)
            {
                consoleHelper.showMessage("No master found.");
                return;
            }

            string[] names = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                names[i] = $"{list[i].MasterID}: {list[i].MasterName}";

            string picked = consoleHelper.pickType("Delete Master", names);
            long masterId = long.Parse(picked.Split(':')[0]);

            database.deletePetsByMasterId(masterId);
            database.deleteMaster(masterId);

            consoleHelper.showMessage("Master and all their pets have been deleted.");

            if (master != null && master.MasterID == masterId)
                master = null;
        }

        private void StartPetCustomizationFlow(Master master)
        {
            try
            {
                Console.Clear();
                string[] petTypes = { "Dog", "Cat" };
                string petType = consoleHelper.pickType("Select Pet Type", petTypes);
                string petName = consoleHelper.getName("Enter Pet's Name: ");
                consoleHelper.showMessage($"Let's create your pet '{petName}'!");
                Console.Clear();

                Pet pet = petType == "Dog" ? new Dog(petName) : new Cat(petName);
                pet.MasterID = master.MasterID;

                AssignPetAttributes(pet, petType);

                long petId = database.insertPet(pet);
                pet.PetID = petId;

                consoleHelper.showMessage($"{pet.Name} has been created successfully!");
            }
            catch (Exception ex)
            {
                consoleHelper.showMessage($"Error during pet creation: {ex.Message}");
            }
        }

        private void AssignPetAttributes(Pet pet, string petType)
        {
            pet.Type = petType;
            pet.Weight = consoleHelper.pickType("Select Pet Weight", attributes.weightCategories.ToArray());
            string ageWithDesc = consoleHelper.pickType("Select Pet Age", attributes.ageCategories.ToArray());
            pet.Age = attributes.GetAgeValue(ageWithDesc);
            pet.Breed = consoleHelper.pickType("Select Pet Breed", attributes.GetBreed(petType).ToArray());
            pet.HairColor = consoleHelper.pickType("Select Hair Color", attributes.hairColor.ToArray());
            pet.HairCut = consoleHelper.pickType("Select Pet Cut", attributes.hairCut.ToArray());
            pet.ColorDesign = consoleHelper.pickType("Select Pet Color Type", attributes.colorEType.ToArray());
            pet.EyeColor = consoleHelper.pickType("Select Eye Color", attributes.colorEye.ToArray());
            pet.Accessory = consoleHelper.pickType("Select Accessory", attributes.accessory.ToArray());
            pet.Personality = consoleHelper.pickType("Select Personality", attributes.personality.ToArray());
            pet.Scent = consoleHelper.pickType("Select Scent", attributes.scent.ToArray());
            pet.Mutation = consoleHelper.pickType("Select Mutation", attributes.mutation.ToArray());
            pet.Element = consoleHelper.pickType("Select Element", attributes.elements.ToArray());
            pet.Crystal = consoleHelper.pickType("Select Crystal", attributes.crystal.ToArray());
            pet.Evolution = consoleHelper.pickType("Select Evolution", attributes.evolution.ToArray());

            string[] statsNames = attributes.stats.ToArray();
            List<byte> stats = consoleHelper.setStat(statsNames);
            pet.Strength = stats[0];
            pet.Mana = stats[1];
            pet.Defense = stats[2];
            pet.Speed = stats[3];
        }

        private void deletePetFlow()
        {
            Console.Clear();
            List<Pet> pets = database.getPetsByMasterId(master.MasterID);

            if (pets.Count == 0)
            {
                consoleHelper.showMessage("No pets to delete.");
                return;
            }

            string[] names = new string[pets.Count];
            for (int i = 0; i < pets.Count; i++)
                names[i] = $"{pets[i].PetID}: {pets[i].Name}";

            string picked = consoleHelper.pickType("Delete Pet", names);
            long petId = long.Parse(picked.Split(':')[0]);

            database.deletePet(petId);
            consoleHelper.showMessage("Pet has been deleted.");
        }

        private void choosePetFlow(long masterId)
        {
            Console.Clear();
            List<Pet> pets = database.getPetsByMasterId(masterId);

            if (pets.Count == 0)
            {
                consoleHelper.showMessage("This master has no pets yet.");
                return;
            }

            Pet chosenPet;

            if (pets.Count == 1)
                chosenPet = pets[0];
            else
            {
                string[] names = new string[pets.Count];
                for (int i = 0; i < pets.Count; i++)
                    names[i] = $"{pets[i].PetID}: {pets[i].Name} ({pets[i].Type})";

                string picked = consoleHelper.pickType("Choose Pet", names);
                long petId = long.Parse(picked.Split(':')[0]);
                chosenPet = database.getPetById(petId);
            }

            assignPetToGlobal(chosenPet);

            string info = $"{chosenPet.Name} ({chosenPet.Type})\n" +
                          $"Breed: {chosenPet.Breed}\n" +
                          $"Age: {chosenPet.Age}\n" +
                          $"Weight: {chosenPet.Weight}\n" +
                          $"Hair: {chosenPet.HairColor} {chosenPet.HairCut}\n" +
                          $"Eye Color: {chosenPet.EyeColor}\n" +
                          $"Accessory: {chosenPet.Accessory}\n" +
                          $"Personality: {chosenPet.Personality}\n" +
                          $"Scent: {chosenPet.Scent}\n" +
                          $"Mutation: {chosenPet.Mutation}\n" +
                          $"Element: {chosenPet.Element}\n" +
                          $"Crystal: {chosenPet.Crystal}\n" +
                          $"Evolution: {chosenPet.Evolution}\n" +
                          $"================= Stats  ===============\n" +
                          $"STR: {chosenPet.Strength}\n" +
                          $"MANA: {chosenPet.Mana}\n" +
                          $"DEF: {chosenPet.Defense}\n" +
                          $"SPD: {chosenPet.Speed}";

            consoleHelper.showMessage(info);
        }

        private void showCredits()
        {
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
        }
    }
}
