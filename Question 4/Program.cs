using System;
using System.IO;

namespace SchoolGradingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Set up file paths
                string inputFile = "students.txt";
                string outputFile = "grade_report.txt";
                string currentDir = Directory.GetCurrentDirectory();
                string inputPath = Path.Combine(currentDir, inputFile);
                string outputPath = Path.Combine(currentDir, outputFile);

                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.Title = "School Grading System";
                Console.Clear();

                Console.WriteLine("=== School Grading System ===\n");
                Console.WriteLine($"Input file: {inputPath}");
                Console.WriteLine($"Output file: {outputPath}\n");

                // Process the student data
                var processor = new StudentResultProcessor();
                
                Console.WriteLine("Reading student data...");
                var students = processor.ReadStudentsFromFile(inputPath);
                
                Console.WriteLine($"Processed {students.Count} student records.");
                
                Console.WriteLine("\nGenerating grade report...");
                processor.WriteReportToFile(students, outputPath);
                
                Console.WriteLine($"\n✅ Grade report has been successfully generated at:\n{outputPath}");
                
                // Display a preview of the report
                Console.WriteLine("\n=== REPORT PREVIEW ===");
                string[] reportLines = File.ReadAllLines(outputPath);
                foreach (string line in reportLines.Take(5))
                {
                    Console.WriteLine(line);
                }
                if (reportLines.Length > 5)
                {
                    Console.WriteLine($"... and {reportLines.Length - 5} more lines");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine("Please make sure the input file exists and try again.");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine("Please check the score format in the input file.");
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine("Please ensure all required fields are present in the input file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ An unexpected error occurred: {ex.Message}");
                Console.WriteLine("\nStack Trace:");
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
