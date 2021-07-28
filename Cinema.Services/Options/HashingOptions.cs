namespace Cinema.Services.Options
{
    public class HashingOptions
    {
        public const string Position = "HashingOptions"; 
        
        public int IterationCount { get; set; }
        public int NumBytesRequested { get; set; }
    }
}