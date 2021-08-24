using System;
using System.Runtime.Serialization;

namespace Timerom.Exception.ExceptionBase
{
    [Serializable]
    public class TimeromException : SystemException
    {
        protected TimeromException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TimeromException(string mensagem) : base(mensagem)
        {
        }
    }
}
