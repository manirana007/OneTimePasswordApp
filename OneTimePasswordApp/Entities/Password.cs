using System;

namespace OneTimePasswordApp.Entities
{
    public class Password
    {
        public string Value { get; private set; }
        public int TimeoutMilliseconds { get; private set; }
        public DateTime GenerationTime { get; private set; }

        public Password(string value, int timeoutMilliseconds)
        {
            Value = value;
            TimeoutMilliseconds = timeoutMilliseconds;
            GenerationTime = DateTime.UtcNow;
        }
    }
}