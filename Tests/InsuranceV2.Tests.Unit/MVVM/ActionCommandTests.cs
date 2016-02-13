using System;
using FluentAssertions;
using InsuranceV2.Common.MVVM;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit.MVVM
{
    [TestFixture]
    public class ActionCommandTests
    {
        [Test]
        public void CanExecuteExecutesFalsePredicate()
        {
            var command = new ActionCommand(o => { }, o => (int) o == 1);
            command.CanExecute(0).Should().BeFalse();
        }

        [Test]
        public void CanExecuteExecutesTruePredicate()
        {
            var command = new ActionCommand(o => { }, o => (int) o == 1);
            command.CanExecute(1).Should().BeTrue();
        }

        [Test]
        public void CanExecuteIsTrueByDefault()
        {
            var command = new ActionCommand(o => { });
            command.CanExecute(null).Should().BeTrue();
        }

        [Test]
        public void ConstructorThrowsExceptionWhenActionIsNull()
        {
            Action act = () => { var actionCommand = new ActionCommand(null); };

            act.ShouldThrow<ArgumentNullException>().Where(x => x.Message.Contains("You must specify an Action<T>"));
        }

        [Test]
        public void ExecuteInvokesAction()
        {
            var invoked = false;

            var command = new ActionCommand(o => { invoked = true; });
            command.Execute();

            invoked.Should().BeTrue();
        }

        [Test]
        public void ExecuteInvokesActionWithParameters()
        {
            var invoked = false;

            var command = new ActionCommand(o =>
            {
                o.Should().NotBeNull();
                invoked = true;
            });
            command.Execute(new object());

            invoked.Should().BeTrue();
        }
    }
}