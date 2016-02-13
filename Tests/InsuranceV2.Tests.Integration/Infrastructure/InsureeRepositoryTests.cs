using System;
using System.Data.SqlClient;
using System.Linq;
using FluentAssertions;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database;
using InsuranceV2.Infrastructure.Database.Context;
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
                LastName = "TestLastName",
                HomeAddress = AddressTests.CreateAddress(ContactType.Personal),
                WorkAddress = AddressTests.CreateAddress(ContactType.Business)
            };
        }

        private bool CheckIfExists(string sql)
        {
            var connection = new SqlConnection(new InsuranceAppContext().Database.Connection.ConnectionString);
            var command = new SqlCommand(sql, connection);

            connection.Open();

            var reader = command.ExecuteReader();
            var result = reader.Read();
            reader.Close();

            connection.Close();

            return result;
        }

        [Test]
        public void ClearingPhoneNumbersCollectionDeletesPhoneNumbers()
        {
            var number1 = Guid.NewGuid().ToString().Substring(0, 25);
            var number2 = Guid.NewGuid().ToString().Substring(0, 25);
            string sql = $"SELECT * FROM PhoneNumbers WHERE Number = '{number1}'";

            var insuree = CreateInsuree();
            insuree.PhoneNumbers.Add(number1, ContactType.Personal);
            insuree.PhoneNumbers.Add(number2, ContactType.Personal);

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
        public void DeletingPersonDeletesPhoneNumbers()
        {
            var number1 = Guid.NewGuid().ToString().Substring(0, 25);
            var number2 = Guid.NewGuid().ToString().Substring(0, 25);
            string sql = $"SELECT * FROM PhoneNumbers WHERE Number = '{number1}'";

            var insuree = CreateInsuree();
            insuree.PhoneNumbers.Add(number1, ContactType.Personal);
            insuree.PhoneNumbers.Add(number2, ContactType.Personal);

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
            insuree1.EmailAddresses.Add("1@example.com", ContactType.Personal);
            insuree1.EmailAddresses.Add("2@example.com", ContactType.Business);
            insuree1.PhoneNumbers.Add("555-123", ContactType.Personal);
            insuree1.PhoneNumbers.Add("555-456", ContactType.Business);
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
            var repository = new InsureeRepository();

            using (new UnitOfWorkFactory().Create())
            {
                repository.Add(insuree);
            }
            insuree.Id.Should().BePositive();

            var insureeCheck = repository.FindById(insuree.Id);
            insureeCheck.Id.Should().Be(insuree.Id);
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

            var repository = new InsureeRepository();

            using (new UnitOfWorkFactory().Create())
            {
                repository.Add(insuree);
            }
            insuree.Id.Should().BePositive();

            var insureeCheck = repository.FindByLastName(lastName);
            insureeCheck.Count(x => x.Id == insuree.Id).Should().BePositive();
        }

        [Test]
        public void FindInsureeByUnknownIdReturnsNull()
        {
            var repository = new InsureeRepository();
            repository.FindById(-1).Should().BeNull();
        }
    }
}