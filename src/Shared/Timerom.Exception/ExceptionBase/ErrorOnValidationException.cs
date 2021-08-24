using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Timerom.Exception.ExceptionBase
{
    [Serializable]
    public class ErrorOnValidationException : TimeromException
    {
        public List<string> ErrorMensages { get; set; }
        public ErrorOnValidationException(List<string> listErrors) : base("")
        {
            ErrorMensages = listErrors;
        }

        protected ErrorOnValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
