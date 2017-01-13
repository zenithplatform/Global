using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zenith.Core.DataProviders;
using Zenith.Core.DataProviders.PostgreSQL;
using Zenith.Core.DataProviders.Base;
using Zenith.Core.DataProviders.RavenDB;
using Zenith.Core.DataProviders.SQLServer;
using Zenith.Core.DataProviders.Enums;

namespace Zenith.Global.Tests
{
    [TestClass]
    public class DataProviders
    {
        [TestMethod]
        public void PostgreSQLProviderConnection()
        {
            DataProviderBase provider = new PostgreSQLDataProvider(new ConnectionParams()
            {
                Database = "Zenith",
                Password = "disaster",
                Port = 5432,
                Server = "localhost",
                Username = "postgres",
                OpenImmidiately = false
            });

            provider.Open();
            Assert.IsTrue(provider.IsOpen);
        }

        [TestMethod]
        public void PostgreSQLProviderInsert()
        {
            DataProviderBase provider = new PostgreSQLDataProvider(new ConnectionParams()
            {
                Database = "Zenith",
                Password = "disaster",
                Port = 5432,
                Server = "localhost",
                Username = "postgres",
                OpenImmidiately = false
            });

            provider.Open();

            User user = new User()
            {
                Username = "providertest",
                AccessToken = "",
                EmailAddress = "testmail@test.com",
                Id = 23,
                IsValid = true
            };

            DataPayload<User> payload = new DataPayload<User>()
            {
                Entity = user,
                Identifier = null
            };

            DataResult result = provider.Execute(Operation.Insert, payload);
            Assert.IsNotNull(result);

            provider.Close();
            Assert.IsFalse(provider.IsOpen);
        }

        [TestMethod]
        public void RavenDBProviderConnection()
        {
            DataProviderBase provider = new RavenDBDataProvider(new ConnectionParams()
            {
                Database = "Zenith",
                Port = 8080,
                Url = "http://localhost",
                OpenImmidiately = false
            });

            provider.Open();

            Assert.IsTrue(provider.IsOpen);
        }

        [TestMethod]
        public void RavenDBProviderInsert()
        {
            DataProviderBase provider = new RavenDBDataProvider(new ConnectionParams()
            {
                Database = "Zenith",
                Port = 8080,
                Url = "http://localhost",
                OpenImmidiately = false
            });

            provider.Open();

            User user = new User()
            {
                Username = "providertest",
                AccessToken = "",
                EmailAddress = "testmail@test.com",
                Id = 23,
                IsValid = true
            };

            DataPayload<User> payload = new DataPayload<User>()
            {
                Entity = user,
                Identifier = null
            };

            DataResult result = provider.Execute(Operation.Insert, payload);

            Assert.IsNotNull(result);

            provider.Close();

            Assert.IsFalse(provider.IsOpen);
        }

        [TestMethod]
        public void SQLServerProviderConnection()
        {
            DataProviderBase provider = new SQLServerDataProvider(new ConnectionParams()
            {
                ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=SSPI;AttachDBFilename=D:\Programming\DB\Test.mdf"
            });

            provider.Open();
            Assert.IsTrue(provider.IsOpen);
            provider.Close();
        }

        [TestMethod]
        [DeploymentItem("EntityFramework.SqlServer.dll")]
        public void SQLServerInsert()
        {
            DataProviderBase provider = new SQLServerDataProvider(new ConnectionParams()
            {
                ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=SSPI;AttachDBFilename=D:\Programming\DB\Test.mdf"
            });

            provider.Open();

            User user = new User()
            {
                Username = "providertest",
                AccessToken = "",
                EmailAddress = "testmail@test.com",
                Id = 23,
                IsValid = true
            };

            DataPayload<User> payload = new DataPayload<User>()
            {
                Entity = user,
                Identifier = null
            };

            DataResult result = provider.Execute(Operation.Insert, payload);
        }
    }
}
