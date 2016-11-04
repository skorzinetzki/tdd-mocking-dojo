using FluentAssertions;
using NUnit.Framework;

namespace lise.dojo.shop.tests
{
    [TestFixture]
    public class SampleClassTest
    {
        [Test]
        public void SampleClass_IsAvailable()
        {
            var sampleClass = new SampleClass();
            sampleClass.Should().BeOfType<SampleClass>();
        }
    }
}