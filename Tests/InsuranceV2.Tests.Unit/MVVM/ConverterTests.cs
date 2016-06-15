using System.Globalization;
using System.Windows;
using FluentAssertions;
using InsuranceV2.Common.MVVM;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit.MVVM
{
    [TestFixture]
    public class ConverterTests : UnitTestBase
    {
        [Test]
        public void ReturnsVisibilityCollapsedWhenObjectIsNull()
        {
            var notNullToVisibilityConverter = new NotNullToVisibilityConverter();

            var visibility = notNullToVisibilityConverter.Convert(null, null, null, CultureInfo.CurrentCulture);

            visibility.ShouldBeEquivalentTo(Visibility.Collapsed);
        }

        [Test]
        public void ReturnsVisibilityVisibleWhenObjectIsNotNull()
        {
            var notNullToVisibilityConverter = new NotNullToVisibilityConverter();

            var visibility = notNullToVisibilityConverter.Convert(new object(), null, null, CultureInfo.CurrentCulture);

            visibility.ShouldBeEquivalentTo(Visibility.Visible);
        }
    }
}