using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace L2
{
    public partial class Forma1 : System.Web.UI.Page
    {
    
        const string utilitiesData = "U16a.txt";
        const string residentsData = "U16b.txt";
        const string startingDataTable = "U16table.txt";
        const string resultsData = "U16results.txt";
        const string removedListData = "U16removed.txt";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var utilitiesList = ReadUtilitiesData(utilitiesData);
            var residentsList = ReadResidentsData(residentsData);

            CalculateResidentsMoneySpent(residentsList, utilitiesList);
            PrintDataTable(utilitiesList, residentsList, startingDataTable);

            var printToWeb = File.ReadAllText(Server.MapPath(startingDataTable));
            TextBox1.Text = printToWeb;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var utilitiesList = ReadUtilitiesData(utilitiesData);
            var residentsList = ReadResidentsData(residentsData);
            CalculateResidentsMoneySpent(residentsList, utilitiesList);

            var filteredResidentsList = FilterResidentsByMoneySpent(residentsList);
            filteredResidentsList.Sort();
            var cheapestMonth = CalculateCheapestMonth(residentsList, utilitiesList);
            PrintResultsTable(cheapestMonth, utilitiesList, residentsList, filteredResidentsList, resultsData);

            var printToWeb = File.ReadAllText(Server.MapPath(resultsData));
            TextBox2.Text = printToWeb;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var utilitiesList = ReadUtilitiesData(utilitiesData);
            var residentsList = ReadResidentsData(residentsData);
            string chosenMonth = TextBox3.Text;
            string chosenUtility = TextBox4.Text;
            if (chosenMonth == "" || chosenUtility == "")
            {
                TextBox5.Text = "Iveskite menesi ir komunaline paslauga!";
            }
            else
            {
                CalculateResidentsMoneySpent(residentsList, utilitiesList);
                var filteredResidentsList = FilterResidentsByMoneySpent(residentsList);
                RemoveResidents(filteredResidentsList, utilitiesList, chosenMonth, chosenUtility);
                PrinteRemovedListTable(filteredResidentsList, removedListData, chosenMonth, chosenUtility);

                var printToWeb = File.ReadAllText(Server.MapPath(removedListData));
                TextBox5.Text = printToWeb;
            }
        }

        UtilitiesList ReadUtilitiesData(string file)
        {
            var utilitiesList = new UtilitiesList();
            using (StreamReader reader = new StreamReader(Server.MapPath(file)))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var values = line.Split(' ');
                    var serviceCode = values[0];
                    var serviceName = values[1];
                    var serviceUnitPrice = double.Parse(values[2]);
                    utilitiesList.AddData(new Utilities(serviceCode, serviceName, serviceUnitPrice));
                    line = reader.ReadLine();
                }
            }

            return utilitiesList;
        }

        ResidentsList ReadResidentsData(string file)
        {
            var residentsList = new ResidentsList();

            using (StreamReader reader = new StreamReader(Server.MapPath(file)))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var values = line.Split(' ');
                    var surname = values[0];
                    var name = values[1];
                    var address = values[2];
                    var month = values[3];
                    var utilityCode = values[4];
                    var serviceCount = int.Parse(values[5]);
                    residentsList.AddData(new Residents(surname, name, address, month, utilityCode, serviceCount));
                    line = reader.ReadLine();
                }
            }

            return residentsList;
        }

        static void CalculateResidentsMoneySpent(ResidentsList residentsList, UtilitiesList utilitiesList)
        {

            for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
            {
                for (utilitiesList.StartOfList(); utilitiesList.Contains(); utilitiesList.NextListNode())
                {
                    residentsList.CalculateMoneySpent(utilitiesList.GetData());
                }
            }
        }

        static double CalculateResidentsAllMoneySpent(ResidentsList residentsList)
        {
            var sum = 0.0;

            for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
            {
                sum += residentsList.GetData().MoneySpent;
            }

            return sum;
        }

        static double CalculateResidentsAverageMoneySpent(ResidentsList residentsList)
        {
            var count = 0;

            for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
            {
                count++;
            }

            return CalculateResidentsAllMoneySpent(residentsList) / count;
        }

        static ResidentsList FilterResidentsByMoneySpent(ResidentsList residentsList)
        {
            var filteredResidents = new ResidentsList();
            var averageMoneySpent = CalculateResidentsAverageMoneySpent(residentsList);

            for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
            {
                if (averageMoneySpent > residentsList.GetData().MoneySpent)
                {
                    filteredResidents.AddData(residentsList.GetData());
                }
            }

            return filteredResidents;
        }

        static double FindUtilityPrice(UtilitiesList utilitiesList, string residentUtilityCode)
        {
            var utilityPrice = 0.0;
            for (utilitiesList.StartOfList(); utilitiesList.Contains(); utilitiesList.NextListNode())
            {

                if (residentUtilityCode == utilitiesList.GetData().ServiceCode)
                {
                    utilityPrice = utilitiesList.GetData().ServiceUnitPrice;
                    break;
                }
            }
            return utilityPrice;
        }

        static string CalculateCheapestMonth(ResidentsList residentsList, UtilitiesList utilitiesList)
        {
            var cheapestMonth = new Dictionary<string, Dictionary<string, double>>();
            var cheapestMonthList = new List<CheapestMonth>();
            var temp = residentsList;
            for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
            {
                if (cheapestMonth.ContainsKey(residentsList.GetData().Month))
                {
                    if (cheapestMonth[residentsList.GetData().Month].ContainsKey(residentsList.GetData().UtilityCode))
                    {

                        cheapestMonth[residentsList.GetData().Month][residentsList.GetData().UtilityCode] +=
                            FindUtilityPrice(utilitiesList, residentsList.GetData().UtilityCode) * residentsList.GetData().ServiceCount;
                    }
                    else
                    {
                        cheapestMonth[residentsList.GetData().Month].Add(
                       residentsList.GetData().UtilityCode,
                       FindUtilityPrice(utilitiesList, residentsList.GetData().UtilityCode) * residentsList.GetData().ServiceCount
                       );
                    }
                }
                else
                {
                    cheapestMonth.Add(residentsList.GetData().Month, new Dictionary<string, double>());
                    cheapestMonth[residentsList.GetData().Month].Add(
                        residentsList.GetData().UtilityCode,
                        FindUtilityPrice(utilitiesList, residentsList.GetData().UtilityCode) * residentsList.GetData().ServiceCount
                        );
                }
            }

            foreach (KeyValuePair<string, Dictionary<string, double>> item in cheapestMonth)
            {
                foreach (KeyValuePair<string, double> utility in cheapestMonth[item.Key])
                {
                    cheapestMonthList.Add(new CheapestMonth(item.Key, utility.Key, utility.Value));
                }
            }

            var cheapestMonthUtility = cheapestMonthList.OrderBy(x => x.Price).FirstOrDefault();

            for (utilitiesList.StartOfList(); utilitiesList.Contains(); utilitiesList.NextListNode())
            {
                if (cheapestMonthUtility.UtilityCode == utilitiesList.GetData().ServiceCode)
                {
                    cheapestMonthUtility.UtilityCode = utilitiesList.GetData().ServiceName;
                }
            }
            if (cheapestMonthUtility != null)
            {
                return $"{cheapestMonthUtility.Month} mėnesį {cheapestMonthUtility.UtilityCode} kainavo pigiausiai";
            }
            return "Nerasta";
        }

        static void RemoveResidents(ResidentsList residentsList, UtilitiesList utilitiesList, string chosenMonth, string chosenUtility)
        {
            var utilityCode = "";

            for (utilitiesList.StartOfList(); utilitiesList.Contains(); utilitiesList.NextListNode())
            {
                if (utilitiesList.GetData().ServiceName == chosenUtility)
                {
                    utilityCode = utilitiesList.GetData().ServiceCode;
                    break;
                }
            }

            for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
            {
                if (residentsList.GetData().Month != chosenMonth || residentsList.GetData().ServiceCount <= 0 || residentsList.GetData().UtilityCode != utilityCode)
                {
                    residentsList.RemoveResident(residentsList.GetNodeLocation(residentsList.GetData()));
                    residentsList.StartOfList();
                }
            }
        }

        void PrintDataTable(UtilitiesList utilitiesList, ResidentsList residentsList, string file)
        {
            using (var writer = new StreamWriter(Server.MapPath(file)))
            {
                writer.WriteLine("Pradiniai Duomenys");
                writer.WriteLine();
                writer.WriteLine("Komunalines Paslaugos");
                writer.WriteLine(new string('-', 80));
                writer.WriteLine("| {0, 15} | {1,25} | {2,30} |", "Paslaugos kodas", "Paslaugos pavadinimas", "Vieno menesio vieneto kaina");
                writer.WriteLine(new string('-', 80));
                for (utilitiesList.StartOfList(); utilitiesList.Contains(); utilitiesList.NextListNode())
                {
                    writer.WriteLine(utilitiesList.GetData().UtilitiesPrintToTable());
                }
                writer.WriteLine(new string('-', 80));
                writer.WriteLine();
                writer.WriteLine("Gyventoju duomenys");
                writer.WriteLine(new string('-', 119));
                writer.WriteLine("| {0,15} | {1,15} | {2,15} | {3,15} | {4,15} | {5,25} |", "Pavardė", "Vardas", "Adresas", "Mėnuo", "Paslaugos kodas", "Sunaudotų vienetų kiekis");
                writer.WriteLine(new string('-', 119));
                for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
                {
                    writer.WriteLine(residentsList.GetData().ResidentsPrintToTable());
                }
                writer.WriteLine(new string('-', 119));
            }
        }

        void PrintResultsTable(string cheapestMonth, UtilitiesList utilitiesList, ResidentsList residentsList, ResidentsList filteredResidentsList, string file)
        {
            using (StreamWriter writer = new StreamWriter(Server.MapPath(file)))
            {
                writer.WriteLine("Pradiniai Duomenys");
                writer.WriteLine();
                writer.WriteLine("Komunalines Paslaugos");
                writer.WriteLine(new string('-', 80));
                writer.WriteLine("| {0, 15} | {1,25} | {2,30} |", "Paslaugos kodas", "Paslaugos pavadinimas", "Vieno menesio vieneto kaina");
                writer.WriteLine(new string('-', 80));
                for (utilitiesList.StartOfList(); utilitiesList.Contains(); utilitiesList.NextListNode())
                {
                    writer.WriteLine(utilitiesList.GetData().UtilitiesPrintToTable());
                }
                writer.WriteLine(new string('-', 80));
                writer.WriteLine();
                writer.WriteLine("Gyventoju duomenys");
                writer.WriteLine(new string('-', 119));
                writer.WriteLine("| {0,15} | {1,15} | {2,15} | {3,15} | {4,15} | {5,25} |", "Pavardė", "Vardas", "Adresas", "Mėnuo", "Paslaugos kodas", "Sunaudotų vienetų kiekis");
                writer.WriteLine(new string('-', 119));
                for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
                {
                    writer.WriteLine(residentsList.GetData().ResidentsPrintToTable());
                }
                writer.WriteLine(new string('-', 119));

                writer.WriteLine("Rezultatai");
                writer.WriteLine();
                writer.WriteLine("Gyventojų sąrašas, kurie už komunalines paslaugas per metus mokėjo sumą, mažesnę už vidutinę, ir surikiuoti pagal pavardę ir vardą abėcėlės tvarka");
                writer.WriteLine(new string('-', 119));
                writer.WriteLine("| {0,15} | {1,15} | {2,15} | {3,15} | {4,15} | {5,25} |", "Pavardė", "Vardas", "Adresas", "Mėnuo", "Paslaugos kodas", "Sunaudotų vienetų kiekis");
                writer.WriteLine(new string('-', 119));
                for (filteredResidentsList.StartOfList(); filteredResidentsList.Contains(); filteredResidentsList.NextListNode())
                {
                    writer.WriteLine(filteredResidentsList.GetData().ResidentsPrintToTable());
                }
                writer.WriteLine(new string('-', 119));
                writer.WriteLine("Nustatytas menesis ir komunalines paslaugos, kainavusios pigiausiai");
                writer.WriteLine(cheapestMonth);
            }
        }

        void PrinteRemovedListTable(ResidentsList residentsList, string file, string chosenMonth, string chosenUtlity)
        {
            using (var writer = new StreamWriter(Server.MapPath(file)))
            {
                writer.WriteLine("Rezultatai");
                writer.WriteLine();
                writer.WriteLine($"Gyventojai po pašalinimo, pašalinti buvo tie, kurie nemokėjo už nurodyta mėnesį ({chosenMonth}) ir paslaugą ({chosenUtlity})");
                writer.WriteLine(new string('-', 119));
                writer.WriteLine("| {0,15} | {1,15} | {2,15} | {3,15} | {4,15} | {5,25} |", "Pavardė", "Vardas", "Adresas", "Mėnuo", "Paslaugos kodas", "Sunaudotų vienetų kiekis");
                writer.WriteLine(new string('-', 119));
                for (residentsList.StartOfList(); residentsList.Contains(); residentsList.NextListNode())
                {
                    writer.WriteLine(residentsList.GetData().ResidentsPrintToTable());
                }
                writer.WriteLine(new string('-', 119));
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }
        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }
        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}