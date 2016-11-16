using FluentAssertions;
using lise.dojo.shop.currency;
using NUnit.Framework;

namespace lise.dojo.shop.tests
{
    [TestFixture]
    public class PriceCalculatorTest
    {
        [Test]
        public void PriceCalculator_CalculateFee_NoFeeForEuro()
        {
            var priceCalculator = new PriceCalculator();
            double originalPrice = 100;

            double expectedFee = 0;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice, Currency.EUR);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
    }
}