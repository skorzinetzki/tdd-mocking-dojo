using System;
using FluentAssertions;
using lise.dojo.shop.currency;
using Moq;
using NUnit.Framework;

namespace lise.dojo.shop.tests
{
    [TestFixture]
    public class PriceCalculatorTest
    {
        private PriceCalculator _priceCalculator;
        private Mock<ICurrencyConverter> _currencyConverterMock;

        [SetUp]
        public void SetUp()
        {
            _currencyConverterMock = new Mock<ICurrencyConverter>();
            _priceCalculator = new PriceCalculator(_currencyConverterMock.Object);
        }

        [Test]
        public void PriceCalculatorIsAvailable()
        {
            _priceCalculator.Should().BeOfType<PriceCalculator>();
        }

        [Test]
        public void CalculateExtraFee_Eur_ShouldBe0Percent()
        {
            var price = 1m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.EUR);

            actual.Should().Be(0m);
        }

        [Test]
        public void CalculateExtraFee_NotEur_ShouldBeAtLeast10Eur()
        {
            var price = 1m;
            var notEur = Currency.CZK;

            decimal actual = _priceCalculator.CalculateExtraFee(price, notEur);

            actual.Should().Be(10m);
        }

        [Test]
        public void CalculateExtraFee_NotEur_ShouldBe7Percent()
        {
            var price = 1000m;
            var notEur = Currency.CZK;

            var actual = _priceCalculator.CalculateExtraFee(price, notEur);

            actual.Should().Be(70m);
        }

        [Test]
        public void CalculateExtraFee_Gbp_ShouldBe5Percent()
        {
            var price = 1m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.GBP);

            actual.Should().Be(0.05m);
        }

        [Test]
        public void CalculateExtraFee_Chf_ShouldBe3Percent()
        {
            var price = 1m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.CHF);

            actual.Should().Be(0.03m);
        }

        [Test]
        public void CalculateExtraFee_Dkk_ShouldBe4Percent()
        {
            var price = 1m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.DKK);

            actual.Should().Be(0.04m);
        }

        [Test]
        public void CalculateExtraFee_Usd_ShouldBeAtLeast8Eur()
        {
            var price = 1m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.USD);

            actual.Should().Be(8m);
        }

        [Test]
        public void CalculateExtraFee_Usd_ShouldBe6Percent()
        {
            var price = 1000m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.USD);

            actual.Should().Be(60m);
        }

        [Test]
        public void CalculateExtraFee_Cad_ShouldBeAtLeast9Eur()
        {
            var price = 1m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.CAD);

            actual.Should().Be(9m);
        }

        [Test]
        public void CalculateExtraFee_Cad_ShouldBe6Percent()
        {
            var price = 1000m;

            var actual = _priceCalculator.CalculateExtraFee(price, Currency.CAD);

            actual.Should().Be(60m);
        }

        [Test]
        public void CalculateExtraFee_NegativePrice_ShouldThrowException()
        {
            var price = -0.01m;

            Action action = () => _priceCalculator.CalculateExtraFee(price, Currency.CHF);

            action.ShouldThrow<PriceCalculationException>().WithMessage("The price must not be negative!");
        }

        [Test]
        public void GetConversionRate_Chf_ShouldReturnExpectedRate()
        {
            // Arrange
            var toCurrency = Currency.CHF;
            var expectedRate = 1.05m;
            _currencyConverterMock.Setup(x => x.GetCurrentConversionRate(toCurrency)).Returns(expectedRate);

            // Act
            decimal rate = _priceCalculator.RetrieveConversionRate(toCurrency);

            // Assert
            rate.Should().Be(expectedRate);
            _currencyConverterMock.Verify(x => x.GetCurrentConversionRate(toCurrency), Times.Once());
        }

        [Test]
        public void CalculateFinalPrice_Eur_ShouldStayTheSame()
        {
            // Arrange
            var toCurrency = Currency.EUR;
            var price = 100m;

            // Act
            decimal finalPrice = _priceCalculator.CalculateFinalPrice(price, toCurrency);

            // Assert
            finalPrice.Should().Be(price);
            _currencyConverterMock.Verify(x => x.GetCurrentConversionRate(toCurrency), Times.Never());
        }
    }
}