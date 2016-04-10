using System;

namespace OneTimePasswordApp.Entities
{
    public class Password
    {
        public string Value { get; private set; }
        public int Timeout { get; private set; }
        public DateTime GenerationTime { get; private set; }

        public Password(string value, int timeout)
        {
            Value = value;
            Timeout = timeout;
            GenerationTime = DateTime.UtcNow;
        }
    }
}