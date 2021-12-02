using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Common.Exceptions
{
    public class ValidationCustomException : Exception
    {
        public ValidationCustomException()
          : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationCustomException(IEnumerable<ValidationFailure> failures)
         : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
