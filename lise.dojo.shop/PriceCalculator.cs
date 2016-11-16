using System;
using lise.dojo.shop.currency;

namespace lise.dojo.shop
{
    public class PriceCalculator
    {
        public double CalculateFee(double originalPrice, Currency currency)
        {

            if(currency == Currency.EUR)
            {
                return 0;
            }

            double defaultRate = 0.07;
            double minimumFee = 10;

            double fee = originalPrice * defaultRate;
            if (fee < minimumFee)
            {
                return minimumFee;
            }
            return fee;
        }
    }
}