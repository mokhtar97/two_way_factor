using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; }
        public List<string> FailuresMessages { get; private set; }
        public int Code { get; }

        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
            FailuresMessages = new List<string>();
        }

        public ValidationException(string[] errors, int code = (int)HttpStatusCode.BadRequest)
        {
            foreach (var item in errors)
            {
                this.FailuresMessages.Add(item);
            }
        }

        public ValidationException(List<ValidationFailure> failures, int code = (int)HttpStatusCode.BadRequest)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);

                foreach (var item in propertyFailures)
                {
                    FailuresMessages.Add(item);
                }
            }
            Code = code;
        }

    }
}
