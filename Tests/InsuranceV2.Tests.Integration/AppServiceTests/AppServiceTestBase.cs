using AutoMapper;
using InsuranceV2.Application.Models;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.AppServiceTests
{
    [TestFixture]
    public class AppServiceTestBase
    {
        public IMapper Mapper { get; private set; }

        [OneTimeSetUp]
        public void SetUp()
        {
            Mapper = AutoMapperConfig.Start();
        }
    }
}