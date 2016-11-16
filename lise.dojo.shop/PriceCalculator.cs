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
            return originalPrice * 0.07;
        }
    }
}