using FluentAssertions;
using InsuranceV2.Common.MVVM;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit.MVVM
{
    [TestFixture]
    public class ObservableObjectTests
    {
        private class StubObservableObject : ObservableObject
        {
            private string _changedProperty;

            public string ChangedProperty
            {
                get { return _changedProperty; }
                set
                {
                    _changedProperty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [Test]
        public void PropertyChangedEventHandlerIsRaised()
        {
            var observableObject = new StubObservableObject();
            var raised = false;

            observableObject.PropertyChanged += (sender, args) =>
            {
                args.PropertyName.ShouldBeEquivalentTo("ChangedProperty");
                raised = true;
            };

            observableObject.ChangedProperty = "Changed";
            raised.ShouldBeEquivalentTo(true);
        }
    }
}