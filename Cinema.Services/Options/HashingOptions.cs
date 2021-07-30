namespace Cinema.Services.Options
{
    public class HashingOptions
    {
        public const string Position = "HashingOptions";
        public int IterationCount { get; set; }
        public int NumberBytesRequestedForHash { get; set; }
        public int NumberBytesOfSalt { get; set; }
    }
}