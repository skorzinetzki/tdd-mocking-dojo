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
            if(currency == Currency.GBP)
            {
                return 0.05;
            }
            return 0.07;
        }

        public double GetMinimumFee(Currency currency)
        {
            if(currency == Currency.GBP)
            {
                return 0;
            }
            return 10;
        }
    }
}