namespace GameApp.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CacheAttribute : Attribute
    {
        public string Key { get; set; }
        public int DurationSeconds { get; set; }

        public CacheAttribute(string key, int durationSeconds)
        {
            Key = key;
            DurationSeconds = durationSeconds;
        }
    }
}
