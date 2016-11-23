using System;

namespace lise.dojo.shop
{
    public class InvalidDateException : ArgumentOutOfRangeException
    {
        public InvalidDateException(string paramName, object actualValue, string message) : base(paramName, actualValue, message)
        {
        }
    }
}