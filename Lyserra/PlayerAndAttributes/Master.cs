using System;


namespace Lyserra.PlayerAndAttributes
{
    public class Master
    {
        private string masterName;
        private int masterID;
        private string specialTrait;
        private string masterType;
        

        public Master(string masterName)
        {
            this.masterName = masterName;
        }

        public string MasterName { get { return masterName; } set { masterName = value; } }
        public int MasterID { get { return masterID; } set { masterID = value; } } 
        public string SpecialTrait { get { return specialTrait; } set { specialTrait = value; } }
        public string MasterType { get { return masterType: } set { masterType = value; } }

        public string returnName()
        {
            string message = "HI " + masterName + "! Welcome to Lyserra.";
            return message;
        }
    }
}
