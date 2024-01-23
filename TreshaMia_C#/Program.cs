using System;
using System.IO;
using System.Collections;
namespace TreshaMia_C_
{
    internal class Program
    {
        static List<double> numList = new List<double>(); // Declare numList globally
        static void Main(string[] args)
        {
            // Clear the contents of the file
            try
            {
                File.WriteAllText("number.txt", string.Empty);
                Console.WriteLine("File data successfully cleared.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing file contents: {ex.Message}");
            }

            int[] lowHighArray = new int[40];
       

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Get high or low");
                Console.WriteLine("2. Save in File");
                Console.WriteLine("3. Read Numbers");
                Console.WriteLine("4 .Exit");
                string choice = Console.ReadLine();

                // Get user choice
                switch (choice)
                {
                    case "1":
                        // Get low and high numbers and calculate the difference
                        GetLowHighNumbers(lowHighArray);                          
                        break;

                    case "2":
                        //save to file
                        WriteToFile(lowHighArray, "number.txt");

                        //set array value to 0
                        for (int i = 0; i < lowHighArray.Length; i++)
                        {
                            lowHighArray[i] = 0;  // Set to the default value (0 for integers)
                        }

                        break;
                    case "3":
                        // read number
                        ReadFile("number.txt");
                        Console.WriteLine("Numbers are:");
                        foreach (double num in numList)
                        {
                            Console.WriteLine(num);
                        }

                        double sum = numList.Sum(); // 
                        Console.WriteLine($"Sum of the numbers: {sum}");

                        //return prime numbers
                        Console.WriteLine("Prime numbers in the list:");
                        foreach (double num in numList)
                        {
                            if (IsPrime(num))
                            {
                                Console.WriteLine(num);
                            }
                        }
                        static bool IsPrime(double number)
                        {
                            if (number <= 1)
                                return false;
                            for (int i = 2; i <= Math.Sqrt(number); i++)
                            {
                                if (number % i == 0)
                                    return false;
                            }
                            return true;
                        }
                        break;
                    case "4":
                        Environment.Exit(0); 
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void GetLowHighNumbers(int[] lowHighArray)
        {
            int lowNumber;
            int highNumber;

            // Loop until a low number greater than 0 is entered
            do
            {
                Console.Write("Enter a low number (positive): ");
            } while (!int.TryParse(Console.ReadLine(), out lowNumber) || lowNumber <= 0);

            // Loop until a high number greater than the low number is entered
            do
            {
                Console.Write("Enter a high number (greater than the low number): ");
            } while (!int.TryParse(Console.ReadLine(), out highNumber) || highNumber <= lowNumber);

            // Calculate the difference         
            int difference = highNumber - lowNumber;
            // Print the difference to the console          
            Console.WriteLine($"The difference between {highNumber} and {lowNumber} is: {difference}");

            for (int i = 0; i < lowHighArray.Length; i++)
            {
                if (lowHighArray[i] <= 0 )
                {                 
                    lowHighArray[i] = lowNumber;
                    lowHighArray[i + 1] = highNumber;
                    break;
                }            
            }
        }

        static void WriteToFile(int[] data, string filePath)
        {
            try
            {
                // Filter out values less than or equal to 0
                var positiveValues = data.Where(item => item > 0).ToArray();

                // Bubble sort positive values in descending order
                for (int i = 0; i < positiveValues.Length - 1; i++)
                {
                    for (int j = 0; j < positiveValues.Length - i - 1; j++)
                    {
                        if (positiveValues[j] < positiveValues[j + 1])
                        {
                            // Swap values
                            int temp = positiveValues[j];
                            positiveValues[j] = positiveValues[j + 1];
                            positiveValues[j + 1] = temp;
                        }
                    }
                }

                // Check if there are any positive values to write
                if (positiveValues.Length > 0)
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        foreach (var item in positiveValues)
                        {
                            sw.WriteLine(item);
                        }
                    }
                    Console.WriteLine($"Data successfully written to {filePath}");
                }
                else
                {
                    Console.WriteLine("No low or high numbers to write.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        static List<double> ReadFile(string filePath)
        {          
            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);
                numList.Clear();
                // Parse each line and add the numbers to the list
                foreach (string line in lines)
                {
                    if (double.TryParse(line, out double number))
                    {
                        numList.Add(number);
                    }              
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }
            return numList;
        }
    }
}
