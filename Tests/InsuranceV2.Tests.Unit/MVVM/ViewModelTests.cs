using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using InsuranceV2.Common.MVVM;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit.MVVM
{
    [TestFixture]
    public class ViewModelTests
    {
        private class StubViewModel : ViewModel
        {
            [Required]
            public string RequiredProperty { get; set; }

            [Required]
            public string AnotherProperty { get; set; }
        }

        [Test]
        public void IndexerValidatesAndReturnsPropertyName()
        {
            var viewModel = new StubViewModel
            {
                RequiredProperty = null,
                AnotherProperty = null
            };
            viewModel["AnotherProperty"].Should().NotBeNull().And.Contain("AnotherProperty");
        }

        [Test]
        public void IndexerValidatesPropertyName()
        {
            var viewModel = new StubViewModel();
            viewModel["RequiredProperty"].Should().NotBeNull().And.Contain("RequiredProperty");
        }

        [Test]
        public void IndexerValidatesPropertyNameWithValidValue()
        {
            var viewModel = new StubViewModel
            {
                RequiredProperty = "Some Value"
            };
            viewModel["RequiredProperty"].Should().BeNull();
        }

        [Test]
        public void IsAbstractBaseClass()
        {
            var t = typeof (ViewModel);
            t.IsAbstract.Should().BeTrue();
        }

        [Test]
        public void IsIDataErrorInfo()
        {
            typeof (IDataErrorInfo).IsAssignableFrom(typeof (ViewModel)).Should().BeTrue();
        }

        [Test]
        public void IsIObservableObject()
        {
            typeof (ObservableObject).Should().Be(typeof (ViewModel).BaseType);
        }
    }
}