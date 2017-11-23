using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.Service
{
    [Serializable]
    internal class PatientIncorectException : Exception
    {
        public PatientIncorectException()
        {
        }

        public PatientIncorectException(string message) : base(message)
        {
        }

        public PatientIncorectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PatientIncorectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}