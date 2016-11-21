using System;
using lise.dojo.shop.currency;

namespace lise.dojo.shop
{
    public class PriceCalculator
    {
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
    }
}