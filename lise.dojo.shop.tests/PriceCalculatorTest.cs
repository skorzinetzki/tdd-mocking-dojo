using FluentAssertions;
using NUnit.Framework;

namespace lise.dojo.shop.tests
{
    [TestFixture]
    public class PriceCalculatorTest
    {
        [Test]
        public void PriceCalculatorIsAvailable()
        {
            var priceCalculator = new PriceCalculator();
            priceCalculator.Should().BeOfType<PriceCalculator>();
        }
    }
}