using System;

namespace lise.dojo.shop
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException(string message) : base(message)
        {
        }
    }
}