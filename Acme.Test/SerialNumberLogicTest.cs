using Acme.Logic;
using Acme.Repository;
using NUnit.Framework;
using Moq;
using Acme.Models.BaseModels;
using Acme.Repository.Repository.Interfaces;

namespace Acme.Test
{
    public class SerialNumberLogicTest
    {
        private Mock<IRepositoryFacade> _repositoryFacade = default!;
        private Mock<ISerialNumberRepository> _serialNumberRepository = default!;


        [SetUp]
        public void TestSetup()
        {
            _repositoryFacade = new Mock<IRepositoryFacade>();
            _serialNumberRepository = new Mock<ISerialNumberRepository>();

            _repositoryFacade.Setup(x => x.SerialNumberRepository()).Returns(_serialNumberRepository.Object);
        }

        [Test]
        public void GetValidSerialNumber_Expected_2_ValidSerialNumbers()
        {
            //Setup
            _serialNumberRepository.Setup(x => x.ReadAsync()).ReturnsAsync(new List<SerialNumberModel>
            {
                new() { SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5" },
                new() { SerialNumber = "2ee43e57-f532-4b98-85a4-0c9c17f97d38" }
            });

            //Act
            var result = new SerialNumberLogic(_repositoryFacade.Object).GetValidSerialNumber();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First(), Is.EqualTo("fef5d5af-6cc4-4ccc-bc68-c97443a909d5"));
            Assert.That(result.Last(), Is.EqualTo("2ee43e57-f532-4b98-85a4-0c9c17f97d38"));
        }

        [Test]
        public void GetValidSerialNumber_Expected_EmptyList()
        {
            //Setup
            _serialNumberRepository.Setup(x => x.ReadAsync()).ReturnsAsync(new List<SerialNumberModel>());

            //Act
            var result = new SerialNumberLogic(_repositoryFacade.Object).GetValidSerialNumber();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(0));
        }
    }
}
