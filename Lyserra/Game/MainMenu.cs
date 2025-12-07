using Lyserra.PlayerAndAttributes;
using System;
using System.Collections.Concurrent;
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
        bool mainMenuActive = true;
        private void assignPetToGlobal(Pet pet)
        {
            if (pet.Type == "Dog")
                dog = (Dog)pet;
            else if (pet.Type == "Cat")
                cat = (Cat)pet;
        }

        public void displayMainMenu()
        {
            

            //bool mainMenuActive = true;
            while (mainMenuActive)
            {
                string[] options = { "Create Master", "Choose Master", "Delete Master", "Campaign", "Credits" };
                string choice = consoleHelper.pickTypeForMenu("Main Menu", options, addExit: true);

                switch (choice)
                {
                    case "Create Master":
                        createMasterFlow();
                        if (master != null) petMenu();
                        break;
                    case "Choose Master":
                        chooseMasterFlow();
                        if (master != null) petMenu();
                        break;
                    case "Delete Master":
                        deleteMasterFlow();
                        break;
                    case "Campaign":
                        consoleHelper.showCampaign();
                        break;
                    case "Credits":
                        showCredits();
                        break;
                    case null:
                        mainMenuActive = false;
                        animation.exitingLogo();
                        break;

                }
            }


        }

        private void petMenu()
        {

            string[] mainMenuOptions = { "Create New Pet", "Delete Pet", "Load Pet", "Change Master", "Exit"};

            while (gameMenuActive)
            {
                Console.Clear();
                Master m = database.getMasterById(master.MasterID);


                string space = "         ";
                string choice = consoleHelper.pickType($"{space}Pet Menu\nMaster: {m.MasterName}", mainMenuOptions);

                switch (choice)
                {
                    case "Create New Pet":
                        animation.loadLogo();
                        StartPetCustomizationFlow(master);
                        animation.loadLogo();

                        break;
                    case "Delete Pet":
                        deletePetFlow();
                        break;
                    case "Load Pet":
                        animation.loadLogo();
                        choosePetFlow(master.MasterID);

                        break;
                    case "Change Master":
                        bool changeMasterIsActive = true;
                        while (changeMasterIsActive)
                        {
                            string[] changeMasterOption= { "Create Master", "Choose Master", "Delete Master" };
                            string changeMasterChoice = consoleHelper.pickTypeForMenu("Master Menu", changeMasterOption, addExit: false);


                            switch (changeMasterChoice)
                            {
                                case "Create Master":
                                    createMasterFlow();
                                    if (master != null) petMenu();
                                    break;
                                case "Choose Master":
                                    chooseMasterFlow();
                                    if (master != null) petMenu();
                                    break;
                                case "Delete Master":
                                    deleteMasterFlow();
                                    break;
                                case null:
                                    changeMasterIsActive = false;
                                    animation.exitingLogo();
                                    break;

                            }
                        }
                        break;
                    case "Exit":
                        gameMenuActive = false;
                        animation.exitingLogo();
                        break;
                }
            }
        }

        private void createMasterFlow()
        {
            Console.Clear();

            // get master's name, exit if null
            string masterName = consoleHelper.getName("Enter (0) to exit.\n=== Enter Master's Name: ");
            if (masterName == null) return; // user pressed 0 or ESC

            // select master type, exit if null
            string type = consoleHelper.pickType("Select Master Type", attributes.ownerTypes.ToArray());
            if (type == null) return; // user pressed 0 or ESC

            // select special trait, exit if null
            string trait = consoleHelper.pickType("Select Special Trait", attributes.specialTraits.ToArray());
            if (trait == null) return; // user pressed 0 or ESC

            // create new master
            Master newMaster = new Master(masterName)
            {
                MasterType = type,
                SpecialTrait = trait
            };

            // insert into database
            long masterId = database.insertMaster(newMaster);
            newMaster.MasterID = masterId;
            master = newMaster;

            // confirmation message
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

            string picked = consoleHelper.pickTypeForMenu("Choose Master", names);
            if (picked == null) return; 
            long masterID = long.Parse(picked.Split(':')[0]);

            Master chosenMaster = database.getMasterById(masterID);

            if (consoleHelper.confirmation($"Are you sure to choose {chosenMaster.MasterName}?"))
            {
                
                this.master = chosenMaster;
                consoleHelper.showMessage($"Master {chosenMaster.MasterName} has entered the game!\nGet ready for adventure!");

            }
            else
            {
                consoleHelper.showMessage("Master selection cancelled. No worries, take your time!");
            }

        }

        private void deleteMasterFlow()
        {
            Console.Clear();

            // Kunin lahat ng master
            List<Master> list = database.getAllMasters();

            if (list.Count == 0)
            {
                consoleHelper.showMessage("No master found.");
                return;
            }

            // Convert to string choices
            string[] names = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
                names[i] = $"{list[i].MasterID}: {list[i].MasterName}";

            // Papa-pick mo like Choose Master
            string picked = consoleHelper.pickType("Delete Master", names);

            long masterId = long.Parse(picked.Split(':')[0]);

            Master masterToDelete = list.First(m => m.MasterID == masterId);

            // Delete pets under that master
           if(consoleHelper.confirmation($"Are you sure to delete {masterToDelete.MasterName}"))
            {
                database.deletePetsByMasterId(masterId);
                database.deleteMaster(masterId);
                consoleHelper.showMessage("Master and all their pets have been deleted.");      
            }
           else
            {
                consoleHelper.showMessage("Master deletion abort.");
            }

                

            // Kung naka-select yung current master, i-clear mo rin
            if (master != null && master.MasterID == masterId )
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

            string SEAns = consoleHelper.pickType("Does your pet have Speciel Eye?", attributes.specialEye.ToArray());
            bool specialEye = SEAns == "yes";
            if (specialEye = true)
            {
                pet.SpecialEye = "Yes";
            }
            else
            {
                pet.SpecialEye = "No";
            }



            pet.Accessory = consoleHelper.pickType("Select Accessory", attributes.accessory.ToArray());
            pet.Personality = consoleHelper.pickType("Select Personality", attributes.personality.ToArray());
            pet.Scent = consoleHelper.pickType("Select Scent", attributes.scent.ToArray());
            pet.Mutation = consoleHelper.pickType("Mutation gives your pet a boost\nSelect Mutation", attributes.mutation.ToArray());
            pet.Element = consoleHelper.pickType("Select Element", attributes.elements.ToArray());
            pet.Crystal = consoleHelper.pickType("Select Crystal", attributes.crystal.ToArray());
            pet.Evolution = consoleHelper.pickType("Select Evolution", attributes.evolution.ToArray());

            string[] statsNames = attributes.stats.ToArray();
            List<byte> stats = consoleHelper.setStat(statsNames);
            bool confirmStats = false;

            Console.Clear();
            Console.WriteLine("=== Review Your Pet's Stats ===");
            for (int i = 0; i < statsNames.Length; i++)
            {
                Console.WriteLine($"{statsNames[i]}: {stats[i]}");
            }

            if (!consoleHelper.confirmation("Are you satisfied with these stats?"))
            {
                stats = consoleHelper.setStat(statsNames); // redo allocation if not satisfied
            }

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

            string picked = consoleHelper.pickType($"Delete Pet", names);
            long petId = long.Parse(picked.Split(':')[0]);

            Pet petToDelete = pets.First(p => p.PetID == petId);

            if (consoleHelper.confirmation($"Are you sure you want to delete {petToDelete.Name}?"))
            {
                database.deletePet(petId);
                consoleHelper.showMessage($"{petToDelete.Name} has been deleted.");
            }
            else
            {
                consoleHelper.showMessage("Pet deletion abort.");
            }   
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

            if(consoleHelper.confirmation($"Are you sure to choose {chosenPet.Name}?"))
            {
                assignPetToGlobal(chosenPet);
                consoleHelper.showMessage($"{chosenPet.Name}!!!, I choose you.");
                
            } else
            {
                consoleHelper.showMessage("Choosing pet cancelled");
            }

                string info = $"{chosenPet.Name} ({chosenPet.Type})\n" +
                              $"Breed: {chosenPet.Breed}\n" +
                              $"Age: {chosenPet.Age}\n" +
                              $"Weight: {chosenPet.Weight}\n" +
                              $"Hair: {chosenPet.HairColor} {chosenPet.HairCut}\n" +
                              $"Eye Color: {chosenPet.EyeColor}\n" +
                              $"Special Eye: {chosenPet.SpecialEye}\n" +
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
            Console.WriteLine("Programmers/Documentators:".PadLeft((40 + 23) / 2));
            Console.WriteLine("Fritz Gabriel M. Almene".PadLeft((40 + 24) / 2));
            Console.WriteLine("Jhon Clifford Robion".PadLeft((40 + 21) / 2));
            Console.WriteLine(line);
            Console.WriteLine("Presented to: Lorenz Christopher Afan".PadLeft((40 + 21) / 2));
            Console.WriteLine(line);

            Console.WriteLine("Press Enter to return...".PadLeft((40 + 23) / 2));
            Console.ReadLine();
        }
    }
}
