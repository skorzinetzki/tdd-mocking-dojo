using System;
using lise.dojo.shop.currency;

namespace lise.dojo.shop
{
    public class PriceCalculator
    {

        private readonly Currency _currencyToConvertTo;
        private readonly double _rate;
        private readonly double _minimumFee;
        private readonly ICurrencyConverter _currencyConverter;

        private PriceCalculator(Currency currencyToConvertTo, double rate, double minimumFee, ICurrencyConverter currencyConverter)
        {
            _currencyToConvertTo = currencyToConvertTo;
            _rate = rate;
            _minimumFee = minimumFee;
            _currencyConverter = currencyConverter;
        }


        public static PriceCalculator GetPriceCalculator(Currency currencyToConvertTo)
        {
            return GetPriceCalculator(currencyToConvertTo, new NoCurrencyConversion());
        }

        public static PriceCalculator GetPriceCalculator(Currency currencyToConvertTo, ICurrencyConverter currencyConverter)
        {
            switch (currencyToConvertTo)
            {
                case Currency.EUR:
                    return new PriceCalculator(currencyToConvertTo, 0, 0, currencyConverter);
                case Currency.GBP:
                    return new PriceCalculator(currencyToConvertTo, 0.05, 0, currencyConverter);
                case Currency.CHF:
                    return new PriceCalculator(currencyToConvertTo, 0.03, 0, currencyConverter);
                case Currency.DKK:
                    return new PriceCalculator(currencyToConvertTo, 0.04, 0, currencyConverter);
                case Currency.USD:
                    return new PriceCalculator(currencyToConvertTo, 0.06, 8, currencyConverter);
                case Currency.CAD:
                    return new PriceCalculator(currencyToConvertTo, 0.06, 9, currencyConverter);
                default:
                    return new PriceCalculator(currencyToConvertTo, 0.07, 10, currencyConverter);
            }
        }

        private class NoCurrencyConversion : ICurrencyConverter
        {
            decimal ICurrencyConverter.GetConversionRateByDate(Currency toCurrency, DateTime dateTime)
            {
                return 1;
            }

            decimal ICurrencyConverter.GetCurrentConversionRate(Currency toCurrency)
            {
                return 1;
            }
        };

        public double CalculateFee(double originalPrice)
        {
            if (originalPrice < 0)
            {
                throw new InvalidPriceException("originalPrice", originalPrice, "Price must not be negative for calculating a fee(currency=" + _currencyToConvertTo + ").");
            }
            double feeInEUR = originalPrice * _rate;

            if (feeInEUR < _minimumFee)
            {
                feeInEUR = _minimumFee;
            }

            double feeInCurrency = feeInEUR * (double)_currencyConverter.GetCurrentConversionRate(_currencyToConvertTo);

            return feeInCurrency;
        }

        public double GetMinimumFee()
        {
            return _minimumFee;
        }
    }
}