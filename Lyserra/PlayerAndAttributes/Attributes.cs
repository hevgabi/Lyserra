using System;
using System.Text;

namespace Lyserra.PlayerAndAttributes
{
    public class Attributes
    {
        public List<string> ownerTypes = new List<string>()
        {
            "Tita with over decorated bags", "Rich kid", "College student na payat",
            "Gym bro", "Sigma lolo", "Delulu na couple", "Swerteng kumag"
        };

        public List<string> stats = new List<string>()
        {
            "Strength", "Mana", "Defense", "Speed"
        };


        public List<string> specialTraits = new List<string>
        {
            "Brave", "Clever", "Kind", "Mischievous", "Wise"
        };

        public List<string> GetBreed(string petType)
        {
            return petType == "Dog" ? dogBreed : catBreed;
        }

        public List<string> specialEye = new List<string>()
        {
            "Yes", "No"
        };

        public List<string> dogBreed = new List<string>()
        {
            "Tiger Commando", "Aspin", "Corgi", "Chihuahua", "Afghan Hound"
        };

        public List<string> catBreed = new List<string>()
        {
            "British Shorthair", "Puspin", "Scottish Fold", "Russian", "Maine coon"
        };

        public List<string> hairColor = new List<string>()
        {
            "Black", "White", "Brown", "Golden", "Gray", "Multi-Colored", "Calico", "Tuxedo"
        };

        public List<string> hairCut = new List<string>()
        {
            "Trim", "Summer Cut", "Poodle Cut", "Lion Cut", "Teddy Bear Cut",
            "Pixie Cut", "Bob Cut", "Panther Cut", "V Cut", "Burst Fade", "Other"
        };

        public List<string> colorEType = new List<string>()
        {
            "Pikachu", "Tiger", "Winnie the Pooh", "Panda", "Jollibee", "Doraemon"
        };

        public List<string> colorEye = new List<string>()
        {
            "Red", "Blue", "Black", "Brown", "Violet", "Yellow", "Green"
        };

        public List<string> accessory = new List<string>()
        {
            "Necklace", "Bracelet", "Shades", "Mask", "Hoodie", "Ribbon", "Tung tung tung sahur bat"
        };

        public List<string> personality = new List<string>()
        {
            "Depressed", "Joyful", "Sigma", "NPC", "Skibidi", "Delulu", "GYAAAAT", "Overthinker"
        };

        public List<string> scent = new List<string>()
        {
            "Baby Powder", "Rich Kid Perfume", "Chocolate Milk", "Mango Float",
            "Starbucks Caramel", "Sadboi Scent [maasim]"
        };

        public List<string> mutation = new List<string>()
        {
             "God [all stats glow]", "Angel [radiant aura]", "Demon [shadow aura]", "Neon [neon glow]", "Golden [golden shine]"
        };

        // weight categories
        public List<string> weightCategories = new List<string>()
        {
            "Light", "Lean", "Overweight"
        };

        // age with descriptions
        public List<string> ageCategories = new List<string>()
        {
            "Young (Below 1 year)", "Adult (1 year and above)"
        };

        // ilimints
        public List<string> elements = new List<string>()
        {
            "Fire [burning aura]",
            "Water [soothing waves]",
            "Earth [rocky shield]",
            "Air [whirling breeze]"
        };


        public List<string> healthStatusMenu = new List<string>()
        {
            "Healthy", "Sick", "Injured"
        };

        public List<string> crystal = new List<string>()
        {
            "Crystal of Invisibility [becomes unseen]",
            "Crystal of Levitation [floats above ground]",
            "Crystal of Undying [resists fatal damage]",
            "Crystal of Kage-Bunshin [creates shadow clones]"
        };

        public List<string> evolution = new List<string>()
        {
            "Mega [increases size and power]",
            "Prism [gains colorful aura]",
            "Titan [massive strength boost]",
            "Phantom [can phase through objects]",
            "Royal [enhanced leadership presence]",
            "Ultra [maximizes potential]",
            "Alpha [dominates other pets]",
            "Arcane [gains magical abilities]",
            "Turbo [extreme speed boost]",
            "Master [perfect balance of all traits]"
        };

        // helper methods to extract value without description
        public string GetAgeValue(string selectedAge)
        {
            if (selectedAge.Contains("Young"))
                return "Young";
            else if (selectedAge.Contains("Adult"))
                return "Adult";
            return selectedAge;
        }
    }
}