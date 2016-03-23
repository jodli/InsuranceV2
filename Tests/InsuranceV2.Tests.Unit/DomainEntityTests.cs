using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using InsuranceV2.Common;
using NUnit.Framework;

namespace InsuranceV2.Tests.Unit
{
    [TestFixture]
    public class DomainEntityTests : UnitTestBase
    {
        internal class InsureeWithIntAsId : DomainEntity<int>
        {
            public override
                IEnumerable<ValidationResult> Validate(
                ValidationContext validationContext)
            {
                throw new NotImplementedException();
            }
        }

        internal class InsureeWithGuidAsId : DomainEntity<Guid>
        {
            public override
                IEnumerable<ValidationResult> Validate(
                ValidationContext validationContext)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void CompareTheSameReferenceReturnTrueTest()
        {
            // arrange
            var entityLeft = new InsureeWithIntAsId();
            var entityRight = entityLeft;

            // act
            if (!entityLeft.Equals(entityRight))
            {
                Assert.Fail();
            }

            if (!(entityLeft == entityRight))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void CompareUsingEqualsOperatorsAndNullOperandsTest()
        {
            // arrange
            InsureeWithIntAsId entityLeft = null;
            var entityRight = new InsureeWithIntAsId {Id = 2};

            // act
            if (!(entityLeft == null))
            {
                Assert.Fail();
            }

            if (!(entityRight != null))
            {
                Assert.Fail();
            }

            entityRight = null;

            // act
            if (!(entityLeft == entityRight))
            {
                Assert.Fail();
            }

            if (entityLeft != entityRight)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void CompareWhenLeftIsNullAndRightIsNullReturnFalseTest()
        {
            // arrange
            InsureeWithIntAsId entityLeft = null;
            var entityRight = new InsureeWithIntAsId {Id = 1};

            // act
            if (!(entityLeft == null))
            {
                Assert.Fail();
            }

            if (!(entityRight != null))
            {
                Assert.Fail();
            }
        }

        [Test]
        public void DifferentIdProduceEqualsFalseTest()
        {
            // arrange
            var entityLeft = new InsureeWithIntAsId {Id = 1};
            var entityRight = new InsureeWithIntAsId {Id = 2};

            // act
            var resultOnEquals = entityLeft.Equals(entityRight);
            var resultOnOperator = entityLeft == entityRight;

            // assert
            resultOnEquals.Should().BeFalse();
            resultOnOperator.Should().BeFalse();
        }

        [Test]
        public void NewInsureeWithGuidAsIdIsTransient()
        {
            var insuree = new InsureeWithGuidAsId();
            insuree.Id.Should().Be(Guid.Empty);
            insuree.IsTransient().Should().BeTrue();
        }

        [Test]
        public void NewInsureeWithIntAsIdIsTransient()
        {
            var insuree = new InsureeWithIntAsId();
            insuree.IsTransient().Should().BeTrue();
        }

        [Test]
        public void InsureeWithGuidAsIdWithValueIsNotTransient()
        {
            var insuree = new InsureeWithGuidAsId {Id = Guid.NewGuid()};
            insuree.IsTransient().Should().BeFalse();
        }

        [Test]
        public void InsureeWithIntAsIdWithValueIsNotTransient()
        {
            var insuree = new InsureeWithIntAsId {Id = 4};
            insuree.IsTransient().Should().BeFalse();
        }

        [Test]
        public void SameIdentityProduceEqualsTrueTest()
        {
            // arrange
            var entityLeft = new InsureeWithIntAsId {Id = 1};
            var entityRight = new InsureeWithIntAsId {Id = 1};

            // act
            var resultOnEquals = entityLeft.Equals(entityRight);
            var resultOnOperator = entityLeft == entityRight;

            // assert
            resultOnEquals.Should().BeTrue();
            resultOnOperator.Should().BeTrue();
        }

        [Test]
        public void SetIdentitySetANonTransientEntity()
        {
            // arrange
            var entity = new InsureeWithIntAsId {Id = 1};

            // assert
            entity.IsTransient().Should().BeFalse();
        }
    }
}