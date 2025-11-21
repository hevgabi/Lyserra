using System;
using System.Text;


namespace Lyserra.PlayerAndAttributes
{
    public class Attributes
    {
        public List<string> GetBreed(string petType)
        {
            return petType == "Dog" ? dogBreed : catBreed;
        }

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
            "Trim", "Summer Cut", "Poodle Cut", "Lion Cut", "Teddy Bear Cut", "Pixie Cut", "Bob Cut",
             "Panther Cut", "V Cut", "Burst Fade", "Other"
        };

        public List<string> colorEType = new List<string>()
        {
            "Pikachu", "Tiger", "Winnie the Pooh", "Panda", "Jollibee", "Doraemon"
        };

        public List<string> colorEye = new List<string>()
        {
            "Red", "Blue", "Black", "Brown", "Violet", "Yellow", "Green"
        };

        public List<string> specialEye = new List<string>()
        {
            "Rinnegan", "Sharingan", "Byakugan", "Six Eye [Gojo]", "Shinigami"
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
            "Baby Powder", "Rich Kid Perfume", "Chocolate Milk", "Mango Float", "Starbucks Caramel", "Sadboi Scent [maasim]"
        };

        public List<string> mutation = new List<string>()
        {
            "God", "Angel", "Demon", "Neon", "Golden"
        };

        public List<string> healthStatusMainMenu = new List<string>()
        {
            "Health Status Overview", "Vaccination Reviews"
        };

        public List<string> healthStatusMenu = new List<string>()
        {
            "Upper Body", "Middle Body", "Lower Body"
        };  
    }
}

