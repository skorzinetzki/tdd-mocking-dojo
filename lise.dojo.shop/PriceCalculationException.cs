using System;
using System.Runtime.Serialization;

namespace lise.dojo.shop
{
    public class PriceCalculationException : Exception
    {
        public PriceCalculationException()
        {
        }

        public PriceCalculationException(string message) : base(message)
        {
        }

        public PriceCalculationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PriceCalculationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}