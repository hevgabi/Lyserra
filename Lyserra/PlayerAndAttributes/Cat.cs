using System;


namespace Lyserra.PlayerAndAttributes
{
    public class Cat : Pet
    {
        public Cat(string petName) : base(petName)
        {
            Type = "Cat";
        }

        public override string showDisplay()
        {
            string line = new string('=', 40);
            string message = "Awesome! you get a cat";
            Console.WriteLine(line);
            Console.WriteLine(message.PadLeft((40 + message.Length) / 2));
            Console.WriteLine(line);

            return message;
        }
    }
}
