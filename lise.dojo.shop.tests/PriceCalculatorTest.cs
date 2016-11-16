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
        [Test]
        public void PriceCalculator_CalculateFee_Default_FeeAbove10Euro()
        {
            var priceCalculator = new PriceCalculator();
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.07;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice, Currency.CNY);

            Assert.AreEqual(expectedFee, calculatedFee);
        }

        [Test]
        public void PriceCalculator_CalculateFee_Default_FeeBelow10Euro()
        {
            var priceCalculator = new PriceCalculator();
            double originalPrice = 100;

            double expectedFee = 10;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice, Currency.CNY);

            Assert.AreEqual(expectedFee, calculatedFee);
        }

        [Test]
        public void PriceCalculator_CalculateFee_GBP()
        {
            var priceCalculator = new PriceCalculator();
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.05;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice, Currency.GBP);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_GetMinimumFee_GBP_HasNoMinimumFee()
        {
            var priceCalculator = new PriceCalculator();

            double noFee = 0;
            double minimumFee = priceCalculator.GetMinimumFee(Currency.GBP);

            Assert.AreEqual(noFee, minimumFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_CHF()
        {
            var priceCalculator = new PriceCalculator();
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.03;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice, Currency.CHF);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_GetMinimumFee_CHF_HasNoMinimumFee()
        {
            var priceCalculator = new PriceCalculator();

            double noFee = 0;
            double minimumFee = priceCalculator.GetMinimumFee(Currency.CHF);

            Assert.AreEqual(noFee, minimumFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_DKK()
        {
            var priceCalculator = new PriceCalculator();
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.04;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice, Currency.DKK);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
    }
}