using System;

namespace Application.Common.Exceptions
{
    public class NotFoundUserOwnException : Exception
    {
        public NotFoundUserOwnException()
        : base()
        {
        }

        public NotFoundUserOwnException(string message)
            : base(message)
        {
        }

        public NotFoundUserOwnException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        public NotFoundUserOwnException(string name, object user)
           : base($"Entity \"{name}\" ({user}) do not own this entity.")
        {
        }
    }
}
