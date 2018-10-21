using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public static class HumanFactory
    {
        public static IHuman CreateHuman(EHumanType humanType)
        {
            switch (humanType)
            {
                case EHumanType.Customer:
                    Customer tempCustomer = new Customer();
                    return tempCustomer.Create(RandomName(humanType));
                case EHumanType.Cleaner:
                    Cleaner tempCleaner = new Cleaner();
                    return tempCleaner.Create(RandomName(humanType));
            }
            return null;
        }

        private static string RandomName(EHumanType humanType)
        {
            string[] FNames = new string[] { "Katherina", "Lilly", "Elizabeth", "Olivia", "Crystal", "Destiny", "Becky", "Cadence", "Jade", "Heather", "Delilah" };
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
