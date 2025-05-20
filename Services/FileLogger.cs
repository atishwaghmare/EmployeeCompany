using System.IO;

namespace EmployeeCompany.Services
{
    public class FileLogger
    {
        private readonly string logFilePath = "Logs/operations.txt";

        public FileLogger()
        {
            Directory.CreateDirectory("Logs"); // Ensure folder exists
        }

        public void Log(string message)
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logEntry);
        }
    }
}
