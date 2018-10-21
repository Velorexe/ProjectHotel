using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    static class HumanFactory
    {
        /// <summary>
        /// Creates a IHuman with the given EHumanType and returns it
        /// </summary>
        /// <param name="humanType">The type of IHuman that needs to be created (Cleaner or Customer)</param>
        /// <returns>IHuman (Cleaner or Customer)</returns>
        public static IHuman CreateHuman(EHumanType humanType)
        {
            switch (humanType)
            {
                //If the given EHumanType is a Customer we will create a new Customer and assign a random name to them
                case EHumanType.Customer:
                    Customer tempCustomer = new Customer();
                    return tempCustomer.Create(RandomName(humanType));
                //If the given EHumanType is a Cleaner we will create a new Cleaner and assign a random name to them
                case EHumanType.Cleaner:
                    Cleaner tempCleaner = new Cleaner();
                    return tempCleaner.Create(RandomName(humanType));
            }
            //If the EHumanType can't be found, we will return null
            return null;
        }

        /// <summary>
        /// Returns a random name (Male or Female)
        /// </summary>
        /// <param name="humanType">The type of IHuman that needs a name (Cleaner will get a Female name, Customer gets a Male or Female name.</param>
        /// <returns>A random name (string)</returns>
        private static string RandomName(EHumanType humanType)
        {
            //A collection of Female names
            string[] FNames = new string[] { "Katherina", "Lilly", "Elizabeth", "Olivia", "Crystal", "Destiny", "Becky", "Cadence", "Jade", "Heather", "Delilah" };
            //A collection of Male names
            string[] MNames = new string[] { "Tim", "Chad", "Bob", "Vincent", "Ahmad", "Mathijs", "Bas", "Hani", "Luuk", "Sam", "David", "Martin" };
            Random r = new Random();

            if(humanType == EHumanType.Cleaner)
            {
                return FNames[r.Next(0, FNames.Length - 1)];
            }
            else if (humanType == EHumanType.Customer)
            {
                if(r.Next(0,2) == 0)
                {
                    return MNames[r.Next(0, MNames.Length - 1)];
                }
                else
                {
                    return FNames[r.Next(0, FNames.Length - 1)];
                }
            }
            else
            {
                return "Sam";
            }
        }
    }
}
