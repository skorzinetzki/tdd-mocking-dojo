using System;
using lise.dojo.shop.currency;

namespace lise.dojo.shop
{
    public class PriceCalculator
    {
        public double CalculateFee(double originalPrice, Currency currency)
        {
            if (currency == Currency.EUR)
            {
                return 0;
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
                case Currency.GBP:
                    return 0.05;
                case Currency.CHF:
                    return 0.03;
                default:
                    return 0.07;
            }
        }

        public double GetMinimumFee(Currency currency)
        {
            switch (currency)
            {
                case Currency.GBP:
                case Currency.CHF:
                    return 0;
                default:
                    return 10;
            }
        }
    }
}