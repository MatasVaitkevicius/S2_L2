using System.Collections.Generic;

namespace L2_Code
{
    public class Residents
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Month { get; set; }
        public string UtilityCode { get; set; }
        public int ServiceCount { get; set; }
        public double MoneySpent { get; set; }   

        public Residents() {}

        public Residents(string surname, string name, string address, string month, string utilityCode, int serviceCount)
        {
            Surname = surname;
            Name = name;
            Address = address;
            Month = month;
            UtilityCode = utilityCode;
            ServiceCount = serviceCount;
        }

        public static bool operator >(Residents firstOne, Residents secondOne)
        {
            return firstOne.Surname.CompareTo(secondOne.Surname) > 0 || firstOne.Surname.CompareTo(secondOne.Surname) == 0 && firstOne.Name.CompareTo(secondOne.Name) > 0;
        }

        public static bool operator <(Residents firstOne, Residents secondOne)
        {
            return firstOne.Surname.CompareTo(secondOne.Surname) < 0 || firstOne.Surname.CompareTo(secondOne.Surname) == 0 && firstOne.Name.CompareTo(secondOne.Name) < 0;
        }

        public static bool operator ==(Residents firstOne, Residents secondOne)
        {
            return firstOne.Surname == secondOne.Surname && firstOne.Name == secondOne.Name;
        }

        public static bool operator !=(Residents firstOne, Residents secondOne)
        {
            return firstOne.Surname != secondOne.Surname && firstOne.Name != secondOne.Name;
        }

        public string  ResidentsPrintToTable()
        {
            return $"| {Surname,-15} | {Name,-15} | {Address,-15} | {Month,-15} | {UtilityCode,15} | {ServiceCount,25} |";
        }

        public override bool Equals(object obj)
        {
            var residents = obj as Residents;
            return residents != null &&
                   Surname == residents.Surname &&
                   Name == residents.Name &&
                   Address == residents.Address &&
                   Month == residents.Month &&
                   UtilityCode == residents.UtilityCode &&
                   ServiceCount == residents.ServiceCount &&
                   MoneySpent == residents.MoneySpent;
        }

        public override int GetHashCode()
        {
            var hashCode = 1231863198;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Month);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UtilityCode);
            hashCode = hashCode * -1521134295 + ServiceCount.GetHashCode();
            hashCode = hashCode * -1521134295 + MoneySpent.GetHashCode();
            return hashCode;
        }
    }
}
