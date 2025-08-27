namespace SchoolGradingSystem
{
    /// <summary>
    /// Exception thrown when a score cannot be converted to an integer
    /// </summary>
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException() 
            : base("Invalid score format. Score must be a valid integer.")
        {
        }

        public InvalidScoreFormatException(string message) 
            : base(message)
        {
        }

        public InvalidScoreFormatException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Exception thrown when a required field is missing in the input
    /// </summary>
    public class MissingFieldException : Exception
    {
        public string FieldName { get; }
        public int LineNumber { get; }

        public MissingFieldException(string fieldName, int lineNumber)
            : base($"Missing required field '{fieldName}' on line {lineNumber}.")
        {
            FieldName = fieldName;
            LineNumber = lineNumber;
        }

        public MissingFieldException(string fieldName, int lineNumber, string message) 
            : base(message)
        {
            FieldName = fieldName;
            LineNumber = lineNumber;
        }

        public MissingFieldException(string fieldName, int lineNumber, string message, Exception innerException) 
            : base(message, innerException)
        {
            FieldName = fieldName;
            LineNumber = lineNumber;
        }
    }
}
