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
            if(currencyConverter == null)
            {
                throw new ArgumentNullException("currencyConverter");
            }
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

        public double GetMinimumFee()
        {
            return _minimumFee;
        }

        public double CalculateFee(double originalPrice)
        {
            double feeInCurrency = CalculateFeeinEUR(originalPrice) * (double)_currencyConverter.GetCurrentConversionRate(_currencyToConvertTo);

            return feeInCurrency;
        }
        private double CalculateFeeinEUR(double originalPrice)
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

            return feeInEUR;
        }

        public double CalculateFee(int originalPrice, DateTime conversionDate)
        {
            if(conversionDate > DateTime.Now)
            {
                throw new InvalidDateException("conversionDate", conversionDate, "ConversionDate must not be in the Future");
            }
            double feeInCurrency = CalculateFeeinEUR(originalPrice) * (double)_currencyConverter.GetConversionRateByDate(_currencyToConvertTo, conversionDate);

            return feeInCurrency;
        }
    }
}