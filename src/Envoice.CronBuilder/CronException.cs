using System;

namespace Envoice.CronBuilder
{
    public class CronException : Exception
    {
        public CronException()
        {
        }

        public CronException(string message) : base(message)
        {
        }

        public CronException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
