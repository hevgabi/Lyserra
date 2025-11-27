using System;

namespace Lyserra.PlayerAndAttributes
{
    public class Dog : Pet
    {
        public Dog(string petName) : base(petName)
        {
            Type = "Dog";
        }
        public override string showDisplay()
        {
            string line = new string('=', 40);
            string message = "Awesome! you get a dog";
            Console.WriteLine(line);
            Console.WriteLine(message.PadLeft((40 + message.Length) / 2));
            Console.WriteLine(line);

            return message;
        }
    }
}
