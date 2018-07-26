using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CrimeAnalyzer
{
    public class crimeStats
    {
        public int year;
        public int pop;
        public int vc;
        public int murder;
        public int rape;
        public int robbery;
        public int assault;
        public int property;
        public int burglary;
        public int theft;
        public int vehicle;

        public crimeStats(int year, int pop, int vc, int murder, int rape, int robbery, int assault, int property, int burglary, int theft, int vehicle)
        {

            this.year = year;
            this.pop = pop;
            this.vc = vc;
            this.murder = murder;
            this.rape = rape;
            this.robbery = robbery;
            this.assault = assault;
            this.property = property;
            this.burglary = burglary;
            this.theft = theft;
            this.vehicle = vehicle;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            String file1;
            String wFile;
            List<crimeStats> list = new List<crimeStats>();
            int count = 0;

            if (args.Length != 2) //checks for number of arguments
            {
                Console.WriteLine("Incorrect arguments. Format should be \n dotnet CrimeAnalyzer.dll <csv_file_path> <report_file_path>  \n");
                Environment.Exit(-1);
            }

            file1 = args[0];

            if (File.Exists(file1) == false) // checks if CrimeData.csv file exists
            {
                Console.WriteLine("File does not exist. Exiting Program. \n");
                Environment.Exit(-1);
            }

            using (var reader = new StreamReader(file1))
            {
               //List<crimeStats> list = new List<crimeStats>();

                string header = reader.ReadLine(); 
                var hValues = header.Split(','); //stores the header value where they can't hurt anyone

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    //vars get assigned here
                    int year = Convert.ToInt32(values[0]);
                    int pop = Convert.ToInt32(values[1]);
                    int vc = Convert.ToInt32(values[2]);
                    int murder = Convert.ToInt32(values[3]);
                    int rape = Convert.ToInt32(values[4]);
                    int robbery = Convert.ToInt32(values[5]);
                    int assault = Convert.ToInt32(values[6]);
                    int property = Convert.ToInt32(values[7]);
                    int burglary = Convert.ToInt32(values[8]);
                    int theft = Convert.ToInt32(values[9]);
                    int vehicle = Convert.ToInt32(values[10]);

                    //vars added to the class list with each iteration of the while loop
                    list.Add(new crimeStats(year, pop, vc, murder, rape, robbery, assault, property, burglary, theft, vehicle));            
                }

                //list.ForEach(Console.WriteLine);

            }


            //put together the output for the report from the info added above ^

            string report = ""; //store all output into this string
           // var years = from crimeStats in list where crimeStats.year < 85000 select crimeStats.year;
            var years = from crimeStats in list select crimeStats.year;
            foreach (var x in years)
            {
                count++;
            }

            var q3Murders = from crimeStats in list where crimeStats.murder < 15000 select crimeStats.year;

            var q4Robberies = from crimeStats in list where crimeStats.robbery > 500000 select new { crimeStats.year, crimeStats.robbery };

            var q5Violence = from crimeStats in list where crimeStats.year == 2010 select crimeStats.vc;
            var q5Capita = from crimeStats in list where crimeStats.year == 2010 select crimeStats.pop;
            double v = 0;
            double c = 0;
            foreach(var x in q5Violence)
            {
                v = (double)x;
            }

            foreach (var x in q5Capita)
            {
                c = (double)x;
            }


            double q5Answer = v/c;

            var q6 = from crimeStats in list select crimeStats.murder;
            double q6Murder = 0;
            foreach (var x in q6)
            {
                q6Murder += x;
            }

            double q6Answer = q6Murder / count;

            var q7 = from crimeStats in list where crimeStats.year >= 1994 && crimeStats.year <= 1997 select crimeStats.murder;
            double q7Murder = 0;
            int q7Count = 0;
            foreach (var x in q7)
            {
                q7Murder += x;
                q7Count++;
            }

            double q7Answer = q7Murder/q7Count;

            var q8 = from crimeStats in list where crimeStats.year >= 2010 && crimeStats.year <= 2013 select crimeStats.murder;
            double q8Murder = 0;
            int q8Count = 0;
            foreach (var x in q8)
            {
                q8Murder += x;
                q8Count++;
            }

            double q8Answer = q8Murder / q8Count;


            var q9 = from crimeStats in list where crimeStats.year >= 1999 && crimeStats.year <= 2004 select crimeStats.theft;

            int q9Answer = q9.Min();

            var q10 = from crimeStats in list where crimeStats.year >= 1999 && crimeStats.year <= 2004 select crimeStats.theft;

            int q10Answer = q10.Max();

            var q11 = from crimeStats in list select new { crimeStats.year, crimeStats.vehicle };
            int q11Answer = 0;
            int temp = 0;

            foreach (var x in q11)
            {
                
                if(x.vehicle > temp )
                {
                    q11Answer = x.year;
                    temp = x.vehicle;
                }
            }



            //put answers into the report

            //question1 + 2
            report += "The range of years include " + years.Min() + " - " + years.Max() + " (" + count + " years) \n";

            //question3
            report += "Years murders per year < 15000: ";
            foreach (var x in q3Murders)
            {
                report += x + " ";
            }
            report += "\n";

            report += "Robberies per year > 50000: ";
            foreach (var x in q4Robberies)
            {
                report += string.Format("{0} = {1}, ", x.year, x.robbery);
            }

            report += "\n";

            report += "Violent crime per capita rate (2010): " + q5Answer + "\n";

            report += "Average murder per year (all years): " + q6Answer + "\n";

            report += "Average murder per year (1994-1997): " + q7Answer + "\n";

            report += "Average murder per year (1994-1997): " + q8Answer + "\n";

            report += "Minimum thefts per year (1999-2004): " + q9Answer + "\n";

            report += "Maximumthefts per year (1999-2004): " + q10Answer + "\n";

            report += "Year of highest number of motor vehicle thefts: " + q11Answer + "\n";



           // Console.WriteLine(report);


            //output list to file below

            wFile = "Output.txt";

            StreamWriter sw = new StreamWriter(wFile);

                try
                 {

                     sw.WriteLine(report);


                 }
                 catch (Exception e)
                 {
                     Console.WriteLine("Exception: " + e.Message);
                 }
                 finally
                 {
                     Console.WriteLine("Executing finally block.");

                     sw.Close();

                 }


        }
    }
}