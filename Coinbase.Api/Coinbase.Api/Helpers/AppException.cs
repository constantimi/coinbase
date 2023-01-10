using System.Globalization;
using System.Runtime.Serialization;

namespace Coinbase.Api.Helpers
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }

        protected AppException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext) { }
    }
}
