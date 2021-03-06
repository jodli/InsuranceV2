﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using InsuranceV2.Application.Services;
using InsuranceV2.Common.Enums;
using InsuranceV2.Common.Logging;
using InsuranceV2.Common.Models;
using InsuranceV2.Infrastructure.Database;
using InsuranceV2.Infrastructure.Repositories;
using Moq;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.AppServiceTests
{
    [TestFixture]
    public class InsureeManagementAppServiceTests : AppServiceTestBase
    {
        private const int Id = 1;
        private const int PartnerId = 2;
        private Mock<IInsureeRepository> _insureeRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactoryMock;
        private Mock<ILogger<InsureeManagementAppService>> _logger;
        private InsureeManagementAppService _insureeManagementAppService;

        [OneTimeSetUp]
        public void Init()
        {
            _insureeRepositoryMock = new Mock<IInsureeRepository>();
            _unitOfWorkFactoryMock = new Mock<IUnitOfWorkFactory>();
            _logger = new Mock<ILogger<InsureeManagementAppService>>();

            _insureeRepositoryMock.Setup(x => x.FindAll()).Returns(() =>
            {
                var allInsurees = new List<Insuree>();

                for (var i = 0; i < 15; i++)
                {
                    var insuree = new Insuree
                    {
                        Id = i + 1,
                        FirstName = i.ToString(),
                        LastName = i.ToString(),
                        DateOfBirth = DateTime.Now.AddYears(-10 - i)
                    };
                    insuree.Addresses.Add("street " + i, "123", "12345", "city", "country", ContactType.Personal);

                    allInsurees.Add(insuree);
                }

                return allInsurees.AsQueryable();
            });

            _insureeRepositoryMock.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<Expression<Func<Insuree, object>>[]>()))
                .Returns(() =>
                {
                    var insuree = new Insuree
                    {
                        Id = Id,
                        FirstName = "first",
                        LastName = "last"
                    };
                    insuree.Addresses.Add("street", "123", "12345", "city", "country", ContactType.Personal);
                    insuree.PhoneNumbers.Add("1234567890", PhoneType.Fax, ContactType.Partner);
                    insuree.EmailAddresses.Add("asdf@asdf.asdf", ContactType.Business);

                    insuree.Partner = new Insuree
                    {
                        Id = PartnerId,
                        FirstName = "partnerFirst",
                        LastName = "partnerLast"
                    };

                    return insuree;
                });

            _insureeManagementAppService = new InsureeManagementAppService(_insureeRepositoryMock.Object,
                _unitOfWorkFactoryMock.Object, _logger.Object, Mapper);
        }

        [Test]
        public void GetDetailInsureeToDisplay()
        {
            var insuree = _insureeManagementAppService.GetDetailInsuree(Id);

            insuree.Id.ShouldBeEquivalentTo(Id);
            insuree.FirstName.Should().NotBeNullOrEmpty();
            insuree.LastName.Should().NotBeNullOrEmpty();

            insuree.Addresses.Count().ShouldBeEquivalentTo(1);
            insuree.PhoneNumbers.Count().ShouldBeEquivalentTo(1);
            insuree.EmailAddresses.Count().ShouldBeEquivalentTo(1);

            insuree.Partner.Id.ShouldBeEquivalentTo(PartnerId);
            insuree.Partner.FirstName.Should().NotBeNullOrEmpty();
            insuree.Partner.LastName.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GetExistingInsureeToEdit()
        {
            var insuree = _insureeManagementAppService.GetExistingInsureeToEdit(Id);

            insuree.Id.ShouldBeEquivalentTo(Id);
            insuree.FirstName.Should().NotBeNullOrEmpty();
            insuree.LastName.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GetNewInsureeToCreate()
        {
            var insuree = _insureeManagementAppService.GetNewInsuree();

            insuree.Id.ShouldBeEquivalentTo(0);
            insuree.FirstName.Should().BeNullOrEmpty();
            insuree.LastName.Should().BeNullOrEmpty();
        }

        [Test]
        public void GetPagedInsurees()
        {
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees();

            pagedInsurees.TotalPages.ShouldBeEquivalentTo(2);
            pagedInsurees.PageNumber.ShouldBeEquivalentTo(1);
            pagedInsurees.PageSize.ShouldBeEquivalentTo(10);

            pagedInsurees.Data.Count().ShouldBeEquivalentTo(10);

            pagedInsurees.Data.First().LastName.Should().Be("0");
            pagedInsurees.Data.Last().LastName.Should().Be("9");
        }

        [Test]
        public void GetPagedInsureesSecondPageSortedDescendingById()
        {
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees(2, 10, "Id", "DESC");

            pagedInsurees.TotalPages.ShouldBeEquivalentTo(2);
            pagedInsurees.PageNumber.ShouldBeEquivalentTo(2);
            pagedInsurees.PageSize.ShouldBeEquivalentTo(10);

            pagedInsurees.Data.Count().ShouldBeEquivalentTo(5);

            pagedInsurees.Data.First().LastName.Should().Be("4");
            pagedInsurees.Data.Last().LastName.Should().Be("0");
        }

        [Test]
        public void GetPagedInsureesSortedDescendingByFullName()
        {
            var pagedInsurees = _insureeManagementAppService.GetPagedInsurees(1, 10, "FullName", "DESC");

            pagedInsurees.Data.Count().ShouldBeEquivalentTo(10);

            pagedInsurees.Data.First().FullName.ShouldBeEquivalentTo("9, 9");
        }
    }
}