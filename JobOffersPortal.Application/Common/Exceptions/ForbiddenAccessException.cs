using System;

namespace JobOffersPortal.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException()
        : base()
        {
        }

        public ForbiddenAccessException(string message)
            : base(message)
        {
        }

        public ForbiddenAccessException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        public ForbiddenAccessException(string name, object user)
           : base($"Entity \"{name}\" ({user}) do not own this entity.")
        {
        }
    }
}
