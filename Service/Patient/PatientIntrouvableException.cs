using System;
using System.Runtime.Serialization;

namespace MigraineCSMiddleware.DAO
{
    [Serializable]
    internal class PatientIntrouvableException : Exception
    {
        public PatientIntrouvableException()
        {
        }

        public PatientIntrouvableException(string message) : base(message)
        {
        }

        public PatientIntrouvableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PatientIntrouvableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}