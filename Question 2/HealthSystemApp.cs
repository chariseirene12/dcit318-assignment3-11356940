using System;
using System.Collections.Generic;
using System.Linq;

public class HealthSystemApp
{
    private readonly Repository<Patient> _patientRepo;
    private readonly Repository<Prescription> _prescriptionRepo;
    private Dictionary<int, List<Prescription>> _prescriptionMap;

    public HealthSystemApp()
    {
        _patientRepo = new Repository<Patient>();
        _prescriptionRepo = new Repository<Prescription>();
        _prescriptionMap = new Dictionary<int, List<Prescription>>();
    }

    /// <summary>
    /// Seeds the repositories with sample data
    /// </summary>
    public void SeedData()
    {
        // Add sample patients
        _patientRepo.Add(new Patient(1, "John Doe", 45, "Male"));
        _patientRepo.Add(new Patient(2, "Jane Smith", 32, "Female"));
        _patientRepo.Add(new Patient(3, "Robert Johnson", 58, "Male"));

        // Add sample prescriptions
        var today = DateTime.Today;
        _prescriptionRepo.Add(new Prescription(101, 1, "Lisinopril 10mg", today.AddDays(-30)));
        _prescriptionRepo.Add(new Prescription(102, 1, "Metformin 500mg", today.AddDays(-15)));
        _prescriptionRepo.Add(new Prescription(201, 2, "Ibuprofen 200mg", today.AddDays(-7)));
        _prescriptionRepo.Add(new Prescription(202, 2, "Amoxicillin 500mg", today.AddDays(-3)));
        _prescriptionRepo.Add(new Prescription(301, 3, "Atorvastatin 20mg", today.AddDays(-60)));
    }

    /// <summary>
    /// Builds a map of patient IDs to their prescriptions
    /// </summary>
    public void BuildPrescriptionMap()
    {
        _prescriptionMap = _prescriptionRepo.GetAll()
            .GroupBy(p => p.PatientId)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    /// <summary>
    /// Prints all patients in the system
    /// </summary>
    public void PrintAllPatients()
    {
        var patients = _patientRepo.GetAll();
        Console.WriteLine("\n=== PATIENTS ===");
        foreach (var patient in patients)
        {
            Console.WriteLine(patient);
        }
        Console.WriteLine("================\n");
    }

    /// <summary>
    /// Prints all prescriptions for a specific patient
    /// </summary>
    /// <param name="patientId">The ID of the patient</param>
    public void PrintPrescriptionsForPatient(int patientId)
    {
        var patient = _patientRepo.GetById(p => p.Id == patientId);
        if (patient == null)
        {
            Console.WriteLine($"Patient with ID {patientId} not found.");
            return;
        }

        Console.WriteLine($"\n=== PRESCRIPTIONS FOR {patient.Name.ToUpper()} ===");
        
        if (_prescriptionMap.TryGetValue(patientId, out var prescriptions))
        {
            foreach (var prescription in prescriptions)
            Console.WriteLine($"- {prescription}");
        }
        else
        {
            Console.WriteLine("No prescriptions found for this patient.");
        }
        
        Console.WriteLine("==================================\n");
    }
}
