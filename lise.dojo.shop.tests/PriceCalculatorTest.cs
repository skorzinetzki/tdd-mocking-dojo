using FluentAssertions;
using lise.dojo.shop.currency;
using Moq;
using NUnit.Framework;

namespace lise.dojo.shop.tests
{
    [TestFixture]
    public class PriceCalculatorTest
    {
        [Test]
        public void PriceCalculator_CalculateFee_NoFeeForEuro()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.EUR);
            double originalPrice = 100;

            double expectedFee = 0;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_Default_FeeAbove10Euro()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CNY);
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.07;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }

        [Test]
        public void PriceCalculator_CalculateFee_Default_FeeBelow10Euro()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CNY);
            double originalPrice = 100;

            double expectedFee = 10;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }

        [Test]
        public void PriceCalculator_CalculateFee_GBP()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.GBP); 
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.05;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_GetMinimumFee_GBP_HasNoMinimumFee()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.GBP);

            double noFee = 0;
            double minimumFee = priceCalculator.GetMinimumFee();

            Assert.AreEqual(noFee, minimumFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_CHF()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CHF);
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.03;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_GetMinimumFee_CHF_HasNoMinimumFee()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CHF);

            double noFee = 0;
            double minimumFee = priceCalculator.GetMinimumFee();

            Assert.AreEqual(noFee, minimumFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_DKK()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.DKK);
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.04;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_GetMinimumFee_DKK_HasNoMinimumFee()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.DKK);

            double noFee = 0;
            double minimumFee = priceCalculator.GetMinimumFee();

            Assert.AreEqual(noFee, minimumFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_USD_FeeAbove8Euro()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.USD);
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.06;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_USD_FeeBelow8Euro()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.USD);
            double originalPrice = 100;

            double expectedFee = 8;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_CAD_FeeAbove9Euro()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CAD);
            double originalPrice = 1000;

            double expectedFee = originalPrice * 0.06;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }
        [Test]
        public void PriceCalculator_CalculateFee_CAD_FeeBelow9Euro()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CAD);
            double originalPrice = 100;

            double expectedFee = 9;
            double calculatedFee = priceCalculator.CalculateFee(originalPrice);

            Assert.AreEqual(expectedFee, calculatedFee);
        }

        [Test]
        public void PriceCalculator_CalculateFee_ThrowsExceptionOnNegativePrices()
        {
            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CAD);
            double negativePrice = -100;
            Assert.Throws<InvalidPriceException>(() => priceCalculator.CalculateFee(negativePrice));
        }

        [Test]
        public void PriceCalculator_CalculateFee_UsesCurrentConversionRate()
        {
            decimal conversionRateEURtoCNY = 1.25m;
            var currencyConverter = new Mock<ICurrencyConverter>();
            currencyConverter.Setup(converter => converter.GetCurrentConversionRate(Currency.CNY)).Returns(conversionRateEURtoCNY);

            var priceCalculator = PriceCalculator.GetPriceCalculator(Currency.CNY, currencyConverter.Object);
            var priceCalculatorWithoutConversion = PriceCalculator.GetPriceCalculator(Currency.CNY);

            var price = 20;
            var feeInCNY = priceCalculator.CalculateFee(price);
            var feeInEUR = priceCalculatorWithoutConversion.CalculateFee(price);

            var feeInEURConvertedToCNY = feeInEUR * (double)conversionRateEURtoCNY;
            Assert.AreEqual(feeInEURConvertedToCNY, feeInCNY, 0.005);

        }
    }
}