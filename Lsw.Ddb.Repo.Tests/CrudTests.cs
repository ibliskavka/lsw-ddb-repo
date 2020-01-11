using System;
using Amazon.DynamoDBv2;
using Lsw.Ddb.Repo.Tests.Model;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Xunit;

namespace Lsw.Ddb.Repo.Tests
{
    public class CrudTests
    {
        private readonly Mock<IDynamoConfig> _cfg;
        private DynamoRepository _sut;

        public CrudTests()
        {
            _cfg = new Mock<IDynamoConfig>();
            _cfg.Setup(x => x.TableName).Returns("svt-data-dev");
            _cfg.Setup(x => x.PkName).Returns("pk");
            _cfg.Setup(x => x.SkName).Returns("sk");
            _sut = new DynamoRepository(_cfg.Object, new AmazonDynamoDBClient());
        }

        [Fact]
        public async void PutAndGetJsonAreEqual()
        {
            var expected = new
            {
                pk = "test",
                sk = "test",
                Name = "Bob",
                Relationship = "Uncle",
                Address = new
                {
                    Street = "123 Main",
                    City = "Anytown"
                }
            };

            var expectedJson = JsonConvert.SerializeObject(expected);
            await _sut.PutJson(expectedJson);

            var retrievedJson = await _sut.GetJson(expected.pk, expected.sk);
            var retrieved = JsonConvert.DeserializeAnonymousType(retrievedJson, expected);

            Assert.Equal(expected.pk, retrieved.pk);
            Assert.Equal(expected.sk, retrieved.sk);
            Assert.Equal(expected.Name, retrieved.Name);
            Assert.Equal(expected.Relationship, retrieved.Relationship);
            Assert.Equal(expected.Address.Street, retrieved.Address.Street);
            Assert.Equal(expected.Address.City, retrieved.Address.City);
        }


        [Fact]
        public async void PutAndGetItemAreEqual()
        {
            var expectedRequest = new Request
            {
                Field = "test",
                camelCase = "example",
            };

            var expected = new ShopUsage("aws-dialer-dev", "test-test", "+13055102484")
            {
                Request = expectedRequest
            };

            await _sut.PutItem(expected);

            var retrieved = await _sut.GetItem<ShopUsage>(expected.Pk, expected.Sk);

            Assert.Equal(expected.Pk, retrieved.Pk);
            Assert.Equal(expected.Sk, retrieved.Sk);
            Assert.Equal(expected.ShopId, retrieved.ShopId);
            Assert.Equal(expected.OrderId, retrieved.OrderId);
            Assert.Equal(expected.PhoneNumber, retrieved.PhoneNumber);
            Assert.Equal(expected.CreatedOn, retrieved.CreatedOn);


            var retrievedRequest = (retrieved.Request as JObject).ToObject<Request>() ;
            Assert.Equal(expectedRequest.Field, retrievedRequest.Field);
            Assert.Equal(expectedRequest.camelCase, retrievedRequest.camelCase);
        }
    }
}
