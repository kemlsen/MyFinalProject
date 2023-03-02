namespace Business.CCS
{
    public class FileLogger : ILogger
    {
        public void Log()
        {
            System.Console.WriteLine("Dosyaya loglandı");
        }
    }
}
