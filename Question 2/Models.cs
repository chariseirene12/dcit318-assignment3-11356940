public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }

    public Patient(int id, string name, int age, string gender)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Age = age;
        Gender = gender ?? throw new ArgumentNullException(nameof(gender));
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Age: {Age}, Gender: {Gender}";
    }
}

public class Prescription
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string MedicationName { get; set; }
    public DateTime DateIssued { get; set; }

    public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
    {
        Id = id;
        PatientId = patientId;
        MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
        DateIssued = dateIssued;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Medication: {MedicationName}, Issued: {DateIssued:yyyy-MM-dd}";
    }
}
