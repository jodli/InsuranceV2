using System;
using System.Linq;
using FluentAssertions;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database;
using InsuranceV2.Infrastructure.Repositories;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.Infrastructure
{
    [TestFixture]
    public class InsureeRepositoryTests : IntegrationTestBase
    {
        private static Insuree CreateInsuree()
        {
            return new Insuree
            {
                DateOfBirth = DateTime.Now,
                FirstName = "TestFirstName",
                LastName = "TestLastName"
            };
        }

        private static Address CreateAddress(ContactType contactType)
        {
            return new Address
            {
                Street = "street",
                StreetNumber = "123",
                ZipCode = "12345",
                City = "city",
                Country = "country",
                ContactType = contactType
            };
        }

        private static Insurance CreateInsurance()
        {
            return new Insurance
            {
                InsuranceNumber = "ABC 12345",
                StartDate = DateTime.Now
            };
        }

        [Test]
        public void AddressTypeRoundtripsToDatabase()
        {
            var newInsureeId = 0;
            var insuree = SimpleInsureeTests.CreateInsuree();

            var address = CreateAddress(ContactType.Personal);
            insuree.Addresses.Add(address);

            address = CreateAddress(ContactType.Business);
            insuree.Addresses.Add(address);

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }
            insuree.Id.Should().BePositive();
            newInsureeId = insuree.Id;

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                var check = repository.FindById(newInsureeId);
                check.Id.Should().Be(newInsureeId);

                check.Addresses.Count().Should().Be(2);
                check.Addresses[0].ContactType.Should().Be(ContactType.Personal);
                check.Addresses[1].ContactType.Should().Be(ContactType.Business);
            }
        }

        [Test]
        public void InsuranceRoundTripsToDatabase()
        {
            var newInsureeId = 0;
            var insuree = SimpleInsureeTests.CreateInsuree();

            var insurance = CreateInsurance();
            insuree.Insurances.Add(insurance);

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }
            insuree.Id.Should().BePositive();
            newInsureeId = insuree.Id;

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                var check = repository.FindById(newInsureeId);
                check.Id.Should().Be(newInsureeId);

                check.Insurances.Count.Should().Be(1);
                check.Insurances[0].Id.Should().Be(insurance.Id);
                check.Insurances[0].InsuranceNumber.ShouldBeEquivalentTo(insurance.InsuranceNumber);
                check.Insurances[0].StartDate.ShouldBeEquivalentTo(insurance.StartDate);
            }
        }

        [Test]
        public void ClearingAddressCollectionDeletesAddresses()
        {
            var street1 = Guid.NewGuid().ToString().Substring(0, 25);
            var street2 = Guid.NewGuid().ToString().Substring(0, 25);
            string sql = $"SELECT * FROM Addresses WHERE Street = '{street1}'";

            var insuree = CreateInsuree();
            insuree.Addresses.Add(new Address
            {
                Street = street1,
                StreetNumber = "123",
                ZipCode = "12345",
                City = "city",
                Country = "country",
                ContactType = ContactType.Personal
            });
            insuree.Addresses.Add(new Address
            {
                Street = street2,
                StreetNumber = "123",
                ZipCode = "12345",
                City = "city",
                Country = "country",
                ContactType = ContactType.Personal
            });

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }

            CheckIfExists(sql).Should().BeTrue();

            var insureeId = insuree.Id;
            insureeId.Should().BeGreaterThan(0);

            using (new UnitOfWorkFactory().Create(true))
            {
                var checkRepository = new InsureeRepository();
                var checkInsuree = checkRepository.FindById(insureeId, x => x.Addresses);
                checkInsuree.Addresses.Clear();
            }
            CheckIfExists(sql).Should().BeFalse();
        }

        [Test]
        public void ClearingEmailAddressCollectionDeletesEmailAddresses()
        {
            var emailAddress1 = Guid.NewGuid().ToString().Substring(0, 25) + "@example.com";
            var emailAddress2 = Guid.NewGuid().ToString().Substring(0, 25) + "@example.com";
            string sql = $"SELECT * FROM EmailAddresses WHERE EmailAddressText = '{emailAddress1}'";

            var insuree = CreateInsuree();
            insuree.EmailAddresses.Add(emailAddress1, ContactType.Personal);
            insuree.EmailAddresses.Add(emailAddress2, ContactType.Business);

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }

            CheckIfExists(sql).Should().BeTrue();

            var insureeId = insuree.Id;
            insureeId.Should().BeGreaterThan(0);

            using (new UnitOfWorkFactory().Create(true))
            {
                var checkRepository = new InsureeRepository();
                var checkInsuree = checkRepository.FindById(insureeId, x => x.EmailAddresses);
                checkInsuree.EmailAddresses.Clear();
            }
            CheckIfExists(sql).Should().BeFalse();
        }

        [Test]
        public void ClearingPhoneNumbersCollectionDeletesPhoneNumbers()
        {
            var number1 = Guid.NewGuid().ToString().Substring(0, 25);
            var number2 = Guid.NewGuid().ToString().Substring(0, 25);
            string sql = $"SELECT * FROM PhoneNumbers WHERE Number = '{number1}'";

            var insuree = CreateInsuree();
            insuree.PhoneNumbers.Add(number1, PhoneType.Phone, ContactType.Personal);
            insuree.PhoneNumbers.Add(number2, PhoneType.Mobile, ContactType.Personal);

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }

            CheckIfExists(sql).Should().BeTrue();

            var insureeId = insuree.Id;
            insureeId.Should().BeGreaterThan(0);

            using (new UnitOfWorkFactory().Create(true))
            {
                var checkRepository = new InsureeRepository();
                var checkInsuree = checkRepository.FindById(insureeId, x => x.PhoneNumbers);
                checkInsuree.PhoneNumbers.Clear();
            }
            CheckIfExists(sql).Should().BeFalse();
        }

        [Test]
        public void DeletingInsureeDeletesAddresses()
        {
            var street1 = Guid.NewGuid().ToString().Substring(0, 25);
            var street2 = Guid.NewGuid().ToString().Substring(0, 25);
            string sql = $"SELECT * FROM Addresses WHERE Street = '{street1}'";

            var insuree = CreateInsuree();
            insuree.Addresses.Add(new Address
            {
                Street = street1,
                StreetNumber = "123",
                ZipCode = "12345",
                City = "city",
                Country = "country",
                ContactType = ContactType.Personal
            });
            insuree.Addresses.Add(new Address
            {
                Street = street2,
                StreetNumber = "123",
                ZipCode = "12345",
                City = "city",
                Country = "country",
                ContactType = ContactType.Personal
            });

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }

            CheckIfExists(sql).Should().BeTrue();

            var insureeId = insuree.Id;
            insureeId.Should().BeGreaterThan(0);

            using (new UnitOfWorkFactory().Create(true))
            {
                var checkRepository = new InsureeRepository();
                checkRepository.Remove(insureeId);
            }
            CheckIfExists(sql).Should().BeFalse();
        }

        [Test]
        public void DeletingInsureeDeletesEmailAddresses()
        {
            var emailAddress1 = Guid.NewGuid().ToString().Substring(0, 25) + "@example.com";
            var emailAddress2 = Guid.NewGuid().ToString().Substring(0, 25) + "@example.com";
            string sql = $"SELECT * FROM EmailAddresses WHERE EmailAddressText = '{emailAddress1}'";

            var insuree = CreateInsuree();
            insuree.EmailAddresses.Add(emailAddress1, ContactType.Personal);
            insuree.EmailAddresses.Add(emailAddress2, ContactType.Business);

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }

            CheckIfExists(sql).Should().BeTrue();

            var insureeId = insuree.Id;
            insureeId.Should().BeGreaterThan(0);

            using (new UnitOfWorkFactory().Create(true))
            {
                var checkRepository = new InsureeRepository();
                checkRepository.Remove(insureeId);
            }
            CheckIfExists(sql).Should().BeFalse();
        }

        [Test]
        public void DeletingInsureeDeletesPhoneNumbers()
        {
            var number1 = Guid.NewGuid().ToString().Substring(0, 25);
            var number2 = Guid.NewGuid().ToString().Substring(0, 25);
            string sql = $"SELECT * FROM PhoneNumbers WHERE Number = '{number1}'";

            var insuree = CreateInsuree();
            insuree.PhoneNumbers.Add(number1, PhoneType.Mobile, ContactType.Personal);
            insuree.PhoneNumbers.Add(number2, PhoneType.Fax, ContactType.Personal);

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }

            CheckIfExists(sql).Should().BeTrue();

            var insureeId = insuree.Id;
            insureeId.Should().BeGreaterThan(0);

            using (new UnitOfWorkFactory().Create(true))
            {
                var checkRepository = new InsureeRepository();
                checkRepository.Remove(insureeId);
            }
            CheckIfExists(sql).Should().BeFalse();
        }

        [Test]
        public void FindAllInsureesWithPredicate()
        {
            var insuree1 = CreateInsuree();
            var insuree2 = CreateInsuree();
            var insuree3 = CreateInsuree();

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree1);
                repository.Add(insuree2);
                repository.Add(insuree3);
            }

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                var oneAndTwo = repository.FindAll(x => x.Id == insuree1.Id || x.Id == insuree2.Id).ToList();
                oneAndTwo.Count().Should().Be(2);
                oneAndTwo.First(x => x.Id == insuree1.Id).Should().NotBeNull();
                oneAndTwo.First(x => x.Id == insuree2.Id).Should().NotBeNull();
                oneAndTwo.FirstOrDefault(x => x.Id == insuree3.Id).Should().BeNull();
            }
        }

        [Test]
        public void FindAllInsureesWithPredicateEager()
        {
            var insuree1 = CreateInsuree();
            insuree1.Addresses.Add("street1", "1", "12345", "city1", "country1", ContactType.Personal);
            insuree1.Addresses.Add("street2", "2", "12345", "city2", "country2", ContactType.Business);
            insuree1.EmailAddresses.Add("1@example.com", ContactType.Personal);
            insuree1.EmailAddresses.Add("2@example.com", ContactType.Business);
            insuree1.PhoneNumbers.Add("555-123", PhoneType.Fax, ContactType.Personal);
            insuree1.PhoneNumbers.Add("555-456", PhoneType.Phone, ContactType.Business);
            var insuree2 = CreateInsuree();
            var insuree3 = CreateInsuree();

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree1);
                repository.Add(insuree2);
                repository.Add(insuree3);
            }

            using (new UnitOfWorkFactory().Create(true))
            {
                var repository = new InsureeRepository();
                var oneAndTwo =
                    repository.FindAll(x => x.Id == insuree1.Id || x.Id == insuree2.Id, x => x.EmailAddresses).ToList();
                oneAndTwo.Count().Should().Be(2);
                oneAndTwo.First(x => x.Id == insuree1.Id).Should().NotBeNull();
                oneAndTwo.First(x => x.Id == insuree2.Id).Should().NotBeNull();
                oneAndTwo.FirstOrDefault(x => x.Id == insuree3.Id).Should().BeNull();

                oneAndTwo.First(x => x.Id == insuree1.Id).EmailAddresses.Count.Should().Be(2);
                oneAndTwo.First(x => x.Id == insuree2.Id).PhoneNumbers.Count.Should().Be(0);
            }
        }

        [Test]
        public void FindByLastNameReturnsCorrectInsuree()
        {
            var lastName = Guid.NewGuid().ToString();
            var insuree1 = CreateInsuree();
            var insuree2 = CreateInsuree();

            insuree1.LastName = lastName;
            insuree2.LastName = lastName;

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree1);
                repository.Add(insuree2);
            }

            using (new UnitOfWorkFactory().Create(true))
            {
                var repository = new InsureeRepository();
                var insureeWithLastNames = repository.FindByLastName(lastName);
                insureeWithLastNames.Count(x => x.LastName == lastName).Should().Be(2);
            }
        }

        [Test]
        public void FindInsureeById()
        {
            var insuree = CreateInsuree();

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }
            insuree.Id.Should().BePositive();

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                var insureeCheck = repository.FindById(insuree.Id);
                insureeCheck.Id.Should().Be(insuree.Id);
            }
        }

        [Test]
        public void FindInsureeByLastName()
        {
            var lastName = Guid.NewGuid().ToString();
            var insuree = new Insuree
            {
                FirstName = "FirstName",
                LastName = lastName,
                DateOfBirth = DateTime.Now
            };

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.Add(insuree);
            }
            insuree.Id.Should().BePositive();

            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                var insureeCheck = repository.FindByLastName(lastName);
                insureeCheck.Count(x => x.Id == insuree.Id).Should().BePositive();
            }
        }

        [Test]
        public void FindInsureeByUnknownIdReturnsNull()
        {
            using (new UnitOfWorkFactory().Create())
            {
                var repository = new InsureeRepository();
                repository.FindById(-1).Should().BeNull();
            }
        }
    }
}