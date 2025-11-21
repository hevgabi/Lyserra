using System;

namespace Lyserra.PlayerAndAttributes
{
    public class Pet
    {

        private string petName;
        private short petWeight;
        private byte petAge;

        public Pet(string petName, short petWeight, byte petAge)
        {
            this.petName = petName;
            this.petWeight = petWeight;
            this.petAge = petAge;
        }

        public string PetName { get { return petName; } set { petName = value; } }
        public short PetWeight { get { return petWeight; } set { petWeight = value; } }
        public byte PetAge { get { return petAge; } set { petAge = value; } }

        


    }
}
