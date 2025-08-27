namespace SchoolGradingSystem
{
    /// <summary>
    /// Represents a student with an ID, name, and score
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Score { get; set; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            
            if (score < 0 || score > 100)
                throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 0 and 100.");
                
            Score = score;
        }

        /// <summary>
        /// Determines the letter grade based on the student's score
        /// </summary>
        /// <returns>A string representing the letter grade (A, B, C, D, or F)</returns>
        public string GetGrade()
        {
            return Score switch
            {
                >= 80 and <= 100 => "A",
                >= 70 and < 80 => "B",
                >= 60 and < 70 => "C",
                >= 50 and < 60 => "D",
                _ => "F"  // Below 50
            };
        }
    }
}
