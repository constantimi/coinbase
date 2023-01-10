using System.Globalization;
using System.Runtime.Serialization;

namespace Coinbase.Api.Helpers
{
    public class AppException : Exception, ISerializable
    {
        public AppException() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
