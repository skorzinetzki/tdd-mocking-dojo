using System;
using lise.dojo.shop.currency;

namespace lise.dojo.shop
{
    public class PriceCalculator
    {
        public double CalculateFee(double originalPrice, Currency currency)
        {
            double defaultFee = 0.07;
            if(currency == Currency.EUR)
            {
                return 0;
            }
            double fee = originalPrice * defaultFee;
            if (fee < 10)
            {
                return 10;
            }
            return fee;
        }
    }
}