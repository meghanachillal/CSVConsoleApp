//  File ContactGenerator
//  Sample code was taken from:
//  http://www.csharpprogramming.tips/2013/06/RandomDoxGenerator.html

//  Other useful methods are there.
//
// Requirements:
// Exapand on the below example to create a CSV file (https://en.wikipedia.org/wiki/Comma-separated_values)
// For contacts with the fol\lowing data
// First Name
// Last Name
// Street Number
// City
// Province
// Country  == Canada ( Simply insert "canada")
// Postal Code  ( they can be read form a file for this example if you choose, or generate if you wish)
// Phone Number ( they can be read form a file for this example if you choose, or generate if you wish)
// email Address ( Append firstname.lastname against a series for domain names read for a file
//
// Please always try to write clean and readable code
// Here is a good reference doc http://ricardogeek.com/docs/clean_code.html  
// Submit to Bitbucket under Assignment1

// 

using System;
using System.IO;
using System.Linq;

// Describes what is a namespace 
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/namespaces/

namespace SimpleConsoleApp
{
    class ContactGenerator
    {
        // instance of random number generator
        Random rand = new Random();

        static void Main(string[] args)
        {
            // instance of ContactGenerator
            ContactGenerator dg = new ContactGenerator();
        }

        public ContactGenerator()
        {
            String COMMA = ",";
            String outputFileName = @".\customers.csv";

            if (File.Exists(outputFileName))
            {
                Console.Write(" File " + outputFileName + " exists, appending");
            }
            StreamWriter fileStream = new StreamWriter(outputFileName, true);

            // Write Header
            fileStream.Write("First Name");
            fileStream.Write(COMMA);
            fileStream.Write("Last Name");
            fileStream.Write(COMMA);
            fileStream.Write("Street Number");
            fileStream.Write(COMMA);
            fileStream.Write("City");
            fileStream.Write(COMMA);
            fileStream.Write("Province");
            fileStream.Write(COMMA);
            fileStream.Write("Country");
            fileStream.Write(COMMA);
            fileStream.Write("Postal Code");
            fileStream.Write(COMMA);
            fileStream.Write("Phone Number");
            fileStream.Write(COMMA);
            fileStream.Write("Email Address");
            fileStream.WriteLine();

            for (int i = 0; i < 20; i++)
            {
                string firstname = GenerateFirstName();
                fileStream.Write(firstname);
                fileStream.Write(COMMA);
                string lastname = GenerateLastName();
                fileStream.Write(lastname);
                fileStream.Write(COMMA);
                fileStream.Write(GenerateStreetNumber());
                fileStream.Write(COMMA);
                fileStream.Write(GenerateCity());
                fileStream.Write(COMMA);
                fileStream.Write(GenerateProvince());
                fileStream.Write(COMMA);
                fileStream.Write("Canada");
                fileStream.Write(COMMA);
                fileStream.Write(GeneratePostalCode());
                fileStream.Write(COMMA);
                fileStream.Write(GeneratePhoneNumber());
                fileStream.Write(COMMA);
                fileStream.Write(GenerateEmailAddress(firstname , lastname));
                fileStream.WriteLine();
            }
            fileStream.Close();
        }

        public string GenerateFirstName()
        {
            String firstNames = @".\firstNames.txt";
            return ReturnRandomLine(firstNames);
        }

        public string GenerateLastName()
        {
            String lastNames = @".\lastNames.txt";
            return ReturnRandomLine(lastNames);
        }

        public string GenerateCity()
        {
            String cities = @".\city.txt";
            return ReturnRandomLine(cities);
        }

        public string GenerateProvince()
        {
            String provinces = @".\province.txt";
            return ReturnRandomLine(provinces);
        }

        public string GeneratePostalCode()
        {
            String postalCodes =  new string(Enumerable.Repeat("B3L4P9", 6).Select(s => s[rand.Next(s.Length)]).ToArray());
            return postalCodes;
        }

        public string GenerateStreetNumber()
        {
            String streetNumbers = new string(Enumerable.Repeat("12", 2).Select(s => s[rand.Next(s.Length)]).ToArray());
            return streetNumbers;
        }

        public string GeneratePhoneNumber()
        {
            int phoneNumbers = rand.Next(2019999908, 2019999928);
            return  phoneNumbers.ToString();
        }

        public string GenerateEmailAddress( string firstname , string lastname)
        {
            string domainNames = @".\domainNames.txt";
            string domainName =  ReturnRandomLine(domainNames);
            string emailAddress = firstname + "." + lastname + "@" + domainName + ".com";
            return emailAddress;

        }

        // Gets a line from a file
        public string ReturnRandomLine(string FileName)
        {
            string sReturn = string.Empty;

            using (FileStream myFile = new FileStream(FileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader myStream = new StreamReader(myFile))
                {

                    // just cast it to int because we know it will be less than 
                    int fileLength = (int)myFile.Length;

                    // Seek file stream pointer to a rand position...
                    myStream.BaseStream.Seek(rand.Next(1, fileLength), SeekOrigin.Begin);

                    // Read the rest of that line.
                    myStream.ReadLine();

                    // Return the next, full line...
                    sReturn = myStream.ReadLine();
                }
            }

            // If our random file position was too close to the end of the file, it will return an empty string
            // I avoided a while loop in the case that the file is empty or contains only one line
            if (System.String.IsNullOrWhiteSpace(sReturn))
            {
                sReturn = ReturnRandomLine(FileName);
            }

            return sReturn;
        }
    }
}