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

        [Obsolete("Use the factory-method instead")]
        public PriceCalculator()
        {
        }

        [Obsolete("Use the constructor with the factory-method GetPriceCalculator() instead, then call this function without a currency")]
        public double CalculateFee(double originalPrice, Currency currency)
        {
            if (originalPrice < 0)
            {
                throw new InvalidPriceException("originalPrice", originalPrice, "Price must not be negative for calculating a fee(currency=" + currency + ").");
            }
            double fee = originalPrice * GetRate(currency);

            double minimumFee = GetMinimumFee(currency);
            if (fee < minimumFee)
            {
                return minimumFee;
            }
            return fee;
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

        private double GetRate(Currency currency)
        {
            switch (currency)
            {
                case Currency.EUR:
                    return 0;
                case Currency.GBP:
                    return 0.05;
                case Currency.CHF:
                    return 0.03;
                case Currency.DKK:
                    return 0.04;
                case Currency.USD:
                case Currency.CAD:
                    return 0.06;
                default:
                    return 0.07;
            }
        }

        [Obsolete("Use the constructor with the factory-method GetPriceCalculator() instead, then call this function without a currency")]
        public double GetMinimumFee(Currency currency)
        {
            switch (currency)
            {
                case Currency.EUR:
                case Currency.GBP:
                case Currency.CHF:
                case Currency.DKK:
                    return 0;
                case Currency.USD:
                    return 8;
                case Currency.CAD:
                    return 9;
                default:
                    return 10;
            }
        }
        public double GetMinimumFee()
        {
            return _minimumFee;
        }
    }
}