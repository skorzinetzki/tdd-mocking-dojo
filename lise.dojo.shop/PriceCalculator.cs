using System;
using lise.dojo.shop.currency;

namespace lise.dojo.shop
{
    public class PriceCalculator
    {

        private readonly Currency _currencyToConvertTo;
        private readonly double _rate;
        private readonly double _minimumFee;

        private PriceCalculator(Currency currencyToConvertTo, double rate, double minimumFee)
        {
            _currencyToConvertTo = currencyToConvertTo;
            _rate = rate;
            _minimumFee = minimumFee;
        }

        public static PriceCalculator GetPriceCalculator(Currency currencyToConvertTo)
        {
            switch(currencyToConvertTo)
            {
                case Currency.EUR:
                    return new PriceCalculator(currencyToConvertTo, 0, 0);
                case Currency.GBP:
                    return new PriceCalculator(currencyToConvertTo, 0.05, 0);
                case Currency.CHF:
                    return new PriceCalculator(currencyToConvertTo, 0.03, 0);
                case Currency.DKK:
                    return new PriceCalculator(currencyToConvertTo, 0.04, 0);
                case Currency.USD:
                    return new PriceCalculator(currencyToConvertTo, 0.06, 8);
                case Currency.CAD:
                    return new PriceCalculator(currencyToConvertTo, 0.06, 9);
                default:
                    return new PriceCalculator(currencyToConvertTo, 0.07, 10);
            }
        }

        public double CalculateFee(double originalPrice)
        {
            if (originalPrice < 0)
            {
                throw new InvalidPriceException("originalPrice", originalPrice, "Price must not be negative for calculating a fee(currency=" + _currencyToConvertTo + ").");
            }
            double fee = originalPrice * _rate;
            
            if (fee < _minimumFee)
            {
                return _minimumFee;
            }
            return fee;
        }
        
        public double GetMinimumFee()
        {
            return _minimumFee;
        }
    }
}