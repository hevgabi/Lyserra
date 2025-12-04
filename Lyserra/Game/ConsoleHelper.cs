using Lyserra.PlayerAndAttributes;
using System;
using System.Threading;
using System.Text.RegularExpressions;
using SQLitePCL;

namespace Lyserra.Game
{
    // struct to hold display variables
    public struct DisplayVariables
    {
        public string Line;
        public string Title;
        public string Message;
        public int PaddingValue;

        // constructor to initialize struct
        public DisplayVariables(string line, string title, string message, int paddingValue)
        {
            Line = line;
            Title = title;
            Message = message;
            PaddingValue = paddingValue;
        }

        // center text method
        public string CenterText(string text, int width = 40)
        {
            return text.PadLeft((width + text.Length) / 2);
        }
    }

    public class ConsoleHelper : IInputValidator
    {
        // using struct for display variables
        private DisplayVariables displayVars;

        // regex pattern : letters with spaces, 2-20 chars lang to
        private readonly Regex namePattern = new Regex(@"^[a-zA-Z\s]{2,20}$");
        private bool skipStory = false;

        // master reference
        private Master master;

        // constructor to initialize display variables
        public ConsoleHelper()
        {
            displayVars = new DisplayVariables(
                new string('=', 40),
                "",
                "",
                40
            );
        }

        // for validating names using regex
        public bool ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            return namePattern.IsMatch(name.Trim());
        }

        // validate input for empty or whitespace to prevent empty entries
        public bool ValidateInput(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        // sanitize input by trimming whitespace
        public string SanitizeInput(string input)
        {
            return input?.Trim() ?? string.Empty;
        }

        public void showCampaign()
        {
            Console.Clear();
            skipStory = false; // skip flag reset

            // using struct for display variables
            displayVars.Title = "Campaign: The Rise of the Orderbreakers";

            Console.WriteLine(displayVars.Line);
            Console.WriteLine(displayVars.CenterText(displayVars.Title));
            Console.WriteLine(displayVars.Line);
            Console.WriteLine();
            Console.WriteLine(displayVars.CenterText("Press SPACE to skip the story"));
            Console.WriteLine();

            string[] story = {
                "Lyserra is a world guided by the Natural Order, a force that determines which pets are born with special eyes.",
                "Only a few are blessed, creating the classifications of Ordained, Eye-Bound, and Unaffined.",
                "The Ordained hold natural power that keeps the world balanced.",
                "The Eye-Bound awaken their abilities through a deep emotional bond with their master.",
                "The Unaffined live normal lives as loyal companions or trusted professionals.",
                "Your journey begins with your pet at the PRt Hall Center, where your true path will be revealed.",
                "",
                "The rise of the Orderbreakers disrupts the Natural Order through the creation of artificial eyes.",
                "These artificial eyes grant unstable power that corrupts pets and spreads mutation across the land.",
                "Villages fall, Ordained pets vanish, and corruption slowly reaches the Hall's borders.",
                "As a new trainee, you and your pet take on missions to investigate attacks and protect the innocent.",
                "Along the way, you face corrupted beasts and uncover the truth behind the synthetic eye cores.",
                "Every step brings you closer to discovering the destiny of your pet.",
                "",
                "The final stage of the campaign brings you face-to-face with the leader of the Orderbreakers",
                "and the creation of the Artificial King Eye.",
                "Your pet's strength becomes your greatest weapon, whether it is Ordained, Eye-Bound, or Unaffined.",
                "Unaffined pets support the battle as professionals, protecting civilians and stabilizing the field.",
                "Ordained pets fight beside you to restore the balance that was broken.",
                "Eye-Bound pets serve as the key to breaking the corruption at its core.",
                "In the end, you rise as the Guardian of the Natural Order, restoring harmony to Lyserra."
            };

            // start a thread to listen for spacebar press to skip story
            Thread skipThread = new Thread(() =>
            {
                while (!skipStory)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Spacebar)
                        {
                            skipStory = true;
                        }
                    }
                    Thread.Sleep(100);
                }
            });
            skipThread.Start();

            foreach (string lineText in story)
            {
                if (skipStory)
                {
                    // display agad to after mag space ng user
                    if (!string.IsNullOrEmpty(lineText))
                    {
                        Console.WriteLine(displayVars.CenterText(lineText));
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
                else
                {
                    slowWriteLine(lineText);
                    Thread.Sleep(350);
                }
            }

            skipThread.Join(50); // wait for skip thread to finish

            Console.WriteLine();
            Console.WriteLine(displayVars.CenterText("Press Enter to return to the Main Menu..."));
            Console.ReadLine();
        }


        // method to get general input with validation for empty entries
        public string getInput(string prompt)
        {
            string input;

            try
            {
                Console.WriteLine(displayVars.Line);
                Console.Write("=== " + prompt);

                input = Console.ReadLine();
                input = SanitizeInput(input);

                if (!ValidateInput(input))
                {
                    throw new EmptyInputException();
                }

                return input;
            }
            catch (EmptyInputException ex)
            {
                showMessage(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                showMessage($"An error occurred: {ex.Message}");
                return null;
            }
        }

        // method to get a valid name with specific validation 
        public string getName(string prompt)
        {
            string name;
            do
            {
                try
                {
                    Console.WriteLine(displayVars.Line);
                    Console.Write("=== " + prompt);

                    name = Console.ReadLine();
                    name = SanitizeInput(name);

                    if (!ValidateName(name))
                    {
                        throw new InvalidNameException();
                    }

                    return name;
                }
                catch (InvalidNameException ex)
                {
                    Console.Clear();
                    showMessage(ex.Message);
                    Thread.Sleep(1500);
                    Console.Clear();
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    showMessage($"An error occurred: {ex.Message}");
                    Thread.Sleep(1500);
                    Console.Clear();
                }
            } while (true);
        }

        // method to show a message with optional delay - using struct
        public void showMessage(string message)
        {
            displayVars.Message = message;
            Console.WriteLine(displayVars.Line);
            Console.WriteLine(displayVars.CenterText(displayVars.Message));
            Console.WriteLine(displayVars.Line);
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        // method to display a menu and get user choice - using struct
        public char getMenuChoice(string title, string[] options, short startIndex = 0)
        {
            string input;
            do
            {
                try
                {
                    Console.Clear();
                    displayVars.Title = title;

                    Console.WriteLine(displayVars.Line);
                    Console.WriteLine(displayVars.CenterText(displayVars.Title));
                    Console.WriteLine(displayVars.Line);

                    for (int i = 0; i < options.Length; i++)
                        Console.WriteLine($"[{i + startIndex}] {options[i]}");

                    Console.WriteLine(displayVars.Line);
                    Console.WriteLine(displayVars.Line);
                    Console.Write("=== " + "Select Option: ");

                    input = Console.ReadLine();
                    input = SanitizeInput(input);

                    if (!ValidateInput(input))
                    {
                        throw new EmptyInputException();
                    }

                    return input[0];
                }
                catch (EmptyInputException ex)
                {
                    Console.Clear();
                    showMessage(ex.Message);
                    Thread.Sleep(700);
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    showMessage($"An error occurred: {ex.Message}");
                    Thread.Sleep(700);
                }
            } while (true);
        }

        // method to write text slowly with optional character delay
        public void slowWriteLine(string text, int charDelayMs = 12)
        {
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine();
                return;
            }

            string padded = displayVars.CenterText(text);

            foreach (char c in padded)
            {
                if (skipStory) // check skip flag
                {
                    Console.Write(c); // write agad without delay
                }
                else
                {
                    Console.Write(c);
                    Thread.Sleep(charDelayMs);
                }
            }
            Console.WriteLine();
        }

        // method to safely pick an option from the menu
        public string safePick(string[] arr, char choiceChar)
        {
            if (arr == null || arr.Length == 0)
                return string.Empty;

            int index;
            while (true)
            {
                try
                {
                    if (Char.IsDigit(choiceChar))
                    {
                        index = choiceChar - '0';
                        if (index >= 0 && index < arr.Length)
                        {
                            return arr[index];
                        }
                    }

                    throw new InvalidMenuChoiceException($"Invalid choice. Please select a number between 0 and {arr.Length - 1}.");
                }
                catch (InvalidMenuChoiceException ex)
                {
                    showMessage(ex.Message);
                    choiceChar = getMenuChoice("Select Option", arr);
                }
                catch (Exception ex)
                {
                    showMessage($"An error occurred: {ex.Message}");
                    choiceChar = getMenuChoice("Select Option", arr);
                }
            }
        }

        // method to combine menu choice and safe pick
        public string pickType(string title, string[] option)
        {
            char choice = getMenuChoice(title, option);
            return safePick(option, choice);
        }

        public List<byte> setStat(string[] attr)
        {
            byte statValue = 100;
            List<byte> value = new List<byte>(new byte[attr.Length]);

            int index = 0;

            while (statValue > 0)
            {
                Console.Clear();
                displayVars.Title = "Stats";

                Console.WriteLine(displayVars.Line);
                Console.WriteLine(displayVars.CenterText(displayVars.Title));
                Console.WriteLine(displayVars.Line);

                // Show all current stats
                for (int i = 0; i < attr.Length; i++)
                {
                    Console.WriteLine($"[{i + 1}] {attr[i]}: {value[i]}");
                }

                Console.WriteLine(displayVars.Line);

                string attribute = attr[index];
                string raw = getInput($"{attribute} value (Remaining {statValue}): ");

                if (!byte.TryParse(raw, out byte input))
                {
                    showMessage("Invalid input. Enter a number.");
                    continue;
                }

                if (input > statValue)
                {
                    showMessage("Stat exceeds remaining points.");
                    continue;
                }

                // Add to existing value
                value[index] += input;
                statValue -= input;

                // Move to next attribute
                index = (index + 1) % attr.Length;
            }

            showMessage("All stat points allocated.");

            return value;
        }


    }
}