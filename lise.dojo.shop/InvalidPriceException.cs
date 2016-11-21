using System;

namespace lise.dojo.shop
{
    public class InvalidPriceException : ArgumentOutOfRangeException
    {
        public InvalidPriceException(string parameterName, Object price, string message) : base(parameterName, price, message)
        {
        }
    }
}