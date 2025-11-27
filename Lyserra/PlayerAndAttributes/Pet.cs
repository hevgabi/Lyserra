using System;

namespace Lyserra.PlayerAndAttributes
{
    public abstract class Pet
    {
        private long masterID;
        private long petID;
        private string type;
        private string name;
        private string weight;
        private string age;
        private string breed;
        private string hairColor;
        private string colorDesign;
        private string hairCut;
        private string eyeColor;
        private string accessory;
        private string personality;
        private string scent;
        private string mutation;
        // special traits here
        private string element;
        private string crystal;
        private string evolution;
        //pet stats
        private byte strength;
        private byte mana;
        private byte defense;
        private byte speed;

        public Pet(string petName)
        {
            this.name = petName;
            
        }


        public long MasterID { get { return masterID; } set { masterID = value; } }
        public long PetID { get { return petID; } set { petID = value; } }
        public string Type { get { return type; } set { type = value;  } }
        public string Name { get { return name; } set { name = value; } }
        public string Weight { get { return weight; } set { weight = value; } }
        public string Age { get { return age; } set { age = value; } }
        public string Breed { get { return breed; } set { breed = value; } }
        public string HairColor { get { return hairColor; } set { hairColor = value; } }
        public string ColorDesign { get { return colorDesign; } set { colorDesign = value; } }  
        public string HairCut { get { return hairCut; } set { hairCut = value; } }
        public string EyeColor { get { return eyeColor; } set { eyeColor = value; } }
        public string Accessory { get { return accessory; } set { accessory = value; } }
        public string Personality { get { return personality; } set { personality = value; } }
        public string Scent { get { return scent; } set { scent = value; } }
        public string Mutation { get { return mutation; } set { mutation = value; } }
        public string Element { get { return element; } set { element = value; } }
        public string Crystal { get { return crystal; } set { crystal = value; } }
        public string Evolution { get { return evolution; } set { evolution = value; } }
        public byte Strength { get { return strength; } set { strength = value; } }
        public byte Mana { get { return mana; } set { mana = value; } }
        public byte Defense { get { return defense; } set { defense = value; } }
        public byte Speed { get { return speed; } set { speed = value; } }




        public abstract string showDisplay();


    }
}
