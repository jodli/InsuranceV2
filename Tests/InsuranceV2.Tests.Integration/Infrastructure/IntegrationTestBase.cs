using System.Data.SqlClient;
using InsuranceV2.Infrastructure.Database.Context;
using NUnit.Framework;

namespace InsuranceV2.Tests.Integration.Infrastructure
{
    [TestFixture]
    public class IntegrationTestBase
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            using (var context = new InsuranceAppContext())
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }
                context.Database.Create();
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            using (var context = new InsuranceAppContext())
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }
            }
        }

        protected static bool CheckIfExists(string sql)
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
    }
}