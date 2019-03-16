using System;
using System.Runtime.Serialization;

namespace ShoppingApp.Service
{
    [Serializable]
    internal class NameDuplicatedException : Exception
    {
        public NameDuplicatedException()
        {
        }

        public NameDuplicatedException(string message) : base(message)
        {
        }

        public NameDuplicatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NameDuplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}