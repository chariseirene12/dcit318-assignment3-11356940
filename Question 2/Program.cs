using System;

class Program
{
    static void Main()
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Healthcare Management System";
            Console.Clear();
            
            Console.WriteLine("=== Healthcare Management System ===\n");
            
            // Initialize the application
            var healthSystem = new HealthSystemApp();
            
            // Seed with sample data
            Console.WriteLine("Loading sample data...");
            healthSystem.SeedData();
            
            // Build the prescription map
            Console.WriteLine("Building prescription database...\n");
            healthSystem.BuildPrescriptionMap();
            
            // Display all patients
            healthSystem.PrintAllPatients();
            
            // Display prescriptions for each patient
            Console.WriteLine("Displaying prescriptions for each patient...\n");
            
            // Get all patient IDs and show their prescriptions
            var patientIds = new List<int> { 1, 2, 3 };
            foreach (var id in patientIds)
            {
                healthSystem.PrintPrescriptionsForPatient(id);
            }
            
            Console.WriteLine("\n=== End of Report ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
