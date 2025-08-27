using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolGradingSystem
{
    /// <summary>
    /// Handles reading student data from a file and writing grade reports
    /// </summary>
    public class StudentResultProcessor
    {
        /// <summary>
        /// Reads student data from a file and returns a list of Student objects
        /// </summary>
        /// <param name="inputFilePath">Path to the input file</param>
        /// <returns>List of Student objects</returns>
        /// <exception cref="FileNotFoundException">Thrown when the input file is not found</exception>
        /// <exception cref="InvalidScoreFormatException">Thrown when a score is not a valid integer</exception>
        /// <exception cref="MissingFieldException">Thrown when a required field is missing</exception>
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException($"Input file not found: {inputFilePath}");
            }

            var students = new List<Student>();
            int lineNumber = 0;

            using (var reader = new StreamReader(inputFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    
                    // Skip empty lines
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    try
                    {
                        var parts = line.Split(',');
                        
                        // Validate number of fields
                        if (parts.Length != 3)
                        {
                            throw new MissingFieldException("student record", lineNumber, 
                                $"Expected 3 fields (ID, Name, Score) on line {lineNumber}, but found {parts.Length} fields.");
                        }

                        // Parse ID
                        if (!int.TryParse(parts[0].Trim(), out int id))
                        {
                            throw new InvalidScoreFormatException($"Invalid ID format on line {lineNumber}. ID must be an integer.");
                        }

                        // Get name (trim whitespace)
                        string name = parts[1].Trim();
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            throw new MissingFieldException("name", lineNumber);
                        }

                        // Parse score
                        if (!int.TryParse(parts[2].Trim(), out int score))
                        {
                            throw new InvalidScoreFormatException($"Invalid score format on line {lineNumber}. Score must be an integer.");
                        }

                        // Create and add student
                        var student = new Student(id, name, score);
                        students.Add(student);
                    }
                    catch (Exception ex) when (ex is not (MissingFieldException or InvalidScoreFormatException))
                    {
                        // Wrap unexpected exceptions with line number information
                        throw new Exception($"Error processing line {lineNumber}: {ex.Message}", ex);
                    }
                }
            }

            return students;
        }

        /// <summary>
        /// Writes a formatted grade report to the specified output file
        /// </summary>
        /// <param name="students">List of students to include in the report</param>
        /// <param name="outputFilePath">Path to the output file</param>
        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            if (students == null)
                throw new ArgumentNullException(nameof(students));

            // Create directory if it doesn't exist
            string directory = Path.GetDirectoryName(outputFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var writer = new StreamWriter(outputFilePath))
            {
                // Write header
                writer.WriteLine("=== STUDENT GRADE REPORT ===");
                writer.WriteLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
                
                // Write each student's information
                foreach (var student in students.OrderBy(s => s.FullName))
                {
                    string line = $"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}";
                    writer.WriteLine(line);
                }
                
                // Write summary
                writer.WriteLine("\n=== SUMMARY ===");
                writer.WriteLine($"Total Students: {students.Count}");
                writer.WriteLine($"Average Score: {students.Average(s => s.Score):F2}");
                writer.WriteLine($"Highest Score: {students.Max(s => s.Score)} ({students.OrderByDescending(s => s.Score).First().FullName})");
                writer.WriteLine($"Lowest Score: {students.Min(s => s.Score)} ({students.OrderBy(s => s.Score).First().FullName})");
                
                // Grade distribution
                writer.WriteLine("\nGrade Distribution:");
                var gradeGroups = students
                    .GroupBy(s => s.GetGrade())
                    .OrderBy(g => g.Key);
                    
                foreach (var group in gradeGroups)
                {
                    writer.WriteLine($"{group.Key}: {group.Count()} students");
                }
            }
        }
    }
}
