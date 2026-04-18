using Acme.Logic;
using Acme.Repository;
using NUnit.Framework;
using Moq;
using Acme.Models.BaseModels;
using Acme.Models;
using Acme.Repository.Repository.Interfaces;

namespace Acme.Test
{
    public class DrawLogicTest
    {
        private Mock<IRepositoryFacade> _repositoryFacade = default!;
        private Mock<ISerialNumberRepository> _serialNumberRepository = default!;
        private Mock<ICustomerRepository> _customerRepository = default!;

        [SetUp]
        public void TestSetup()
        {
            _repositoryFacade = new Mock<IRepositoryFacade>();
            _serialNumberRepository = new Mock<ISerialNumberRepository>();
            _customerRepository = new Mock<ICustomerRepository>();

            _repositoryFacade.Setup(x => x.SerialNumberRepository()).Returns(_serialNumberRepository.Object);
            _repositoryFacade.Setup(x => x.CustomerRepository()).Returns(_customerRepository.Object);
        }

        [Test]
        public void DrawLogic_read_Expected_1()
        {
            //Arrange
            _serialNumberRepository.Setup(x => x.GetDrawModel(1, 10)).ReturnsAsync(new Pagination<DrawModel>
            {
                Items = new List<DrawModel> {
                    new DrawModel {
                        Email = "Jon@doe.com",
                        FirstName = "jon",
                        LastName = "Doe",
                        SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5"
                    }
                }
            });

            //Act
            var result = new DrawLogic(_repositoryFacade.Object).GetAllAsync(1, 10).Result;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count(), Is.EqualTo(1));
            Assert.That(result.Items.First().Email, Is.EqualTo("Jon@doe.com"));
            Assert.That(result.Items.First().FirstName, Is.EqualTo("jon"));
            Assert.That(result.Items.First().LastName, Is.EqualTo("Doe"));
            Assert.That(result.Items.First().SerialNumber, Is.EqualTo("fef5d5af-6cc4-4ccc-bc68-c97443a909d5"));
        }

        [Test]
        public void DrawLogic_read_Expected_2()
        {
            //Arrange
            _serialNumberRepository.Setup(x => x.GetDrawModel(1, 10)).ReturnsAsync(new Pagination<DrawModel>
            {
                Items = new List<DrawModel> {
                    new() {
                        Email = "Jon@doe.com",
                        FirstName = "jon",
                        LastName = "Doe",
                        SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5"
                    },
                    new() {
                        Email = "Jon@doe.com",
                        FirstName = "jan",
                        LastName = "Doe",
                        SerialNumber =   "2ee43e57-f532-4b98-85a4-0c9c17f97d38"
                    }
                }
            });

            //Act
            var result = new DrawLogic(_repositoryFacade.Object).GetAllAsync(1, 10).Result;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count(), Is.EqualTo(2));
            Assert.That(result.Items.First().Email, Is.EqualTo("Jon@doe.com"));
            Assert.That(result.Items.Last().FirstName, Is.EqualTo("jan"));
            Assert.That(result.Items.First().LastName, Is.EqualTo("Doe"));
            Assert.That(result.Items.Last().SerialNumber, Is.EqualTo("2ee43e57-f532-4b98-85a4-0c9c17f97d38"));
        }

        [Test]
        public void DrawLogic_read_Expected_empty()
        {
            //Arrange
            _serialNumberRepository.Setup(x => x.GetDrawModel(1, 10)).ReturnsAsync(new Pagination<DrawModel> { Items = [] });

            //Act
            var result = new DrawLogic(_repositoryFacade.Object).GetAllAsync(1, 10).Result;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Items.Count(), Is.EqualTo(0));
            Assert.That(result.Items, Is.Empty);
        }

        [Test]
        public void DrawLogic_AddToSerialNumber_With_0_Customers_And_Customer_Exist()
        {
            //Arrange
            var drawModel = new DrawModel
            {
                Email = "Jon@doe.com",
                FirstName = "jon",
                LastName = "Doe",
                SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5"
            };

            var customerModel = new CustomerModel()
            {
                Id = 0,
                Email = "Jon@doe.com",
                FirstName = "jon",
                LastName = "Doe",
            };

            _serialNumberRepository.Setup(x => x.ReadAsync("fef5d5af-6cc4-4ccc-bc68-c97443a909d5")).ReturnsAsync(new List<SerialNumberModel>
            {
                new() {
                    Id = 0,
                    SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5",
                    FirstCustomer = null,
                    SecondCustomer = null
                }
            });

            _customerRepository.Setup(x => x.ReadAsync(customerModel.Email)).ReturnsAsync(new List<CustomerModel>
            {
                customerModel
            });

            _serialNumberRepository.Setup(x => x.UpdateAsync(It.IsAny<SerialNumberModel>()));

            //Act
            new DrawLogic(_repositoryFacade.Object).AddToSerialNumber(drawModel);

            //Assert
            Assert.That(_serialNumberRepository.Invocations.Count, Is.EqualTo(2));
            Assert.That(_customerRepository.Invocations.Count, Is.EqualTo(1));
        }

        [Test]
        public void DrawLogic_AddToSerialNumber_With_0_Customers_And_Customer_Dont_Exist()
        {
            //Arrange
            var drawModel = new DrawModel
            {
                Email = "Jon@doe.com",
                FirstName = "jon",
                LastName = "Doe",
                SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5"
            };

            var customerModel = new CustomerModel()
            {
                Id = 0,
                Email = "Jon@doe.com",
                FirstName = "jon",
                LastName = "Doe",
            };

            _serialNumberRepository.Setup(x => x.ReadAsync("fef5d5af-6cc4-4ccc-bc68-c97443a909d5")).ReturnsAsync(new List<SerialNumberModel>
            {
                new() {
                    Id = 0,
                    SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5",
                    FirstCustomer = null,
                    SecondCustomer = null
                }
            });

            _customerRepository.Setup(x => x.ReadAsync(customerModel.Email)).ReturnsAsync(new List<CustomerModel>());

            _serialNumberRepository.Setup(x => x.UpdateAsync(It.IsAny<SerialNumberModel>()));

            //Act
            new DrawLogic(_repositoryFacade.Object).AddToSerialNumber(drawModel);

            //Assert
            Assert.That(_serialNumberRepository.Invocations.Count, Is.EqualTo(2));
            Assert.That(_customerRepository.Invocations.Count, Is.EqualTo(2));
        }

        [Test]
        public void DrawLogic_AddToSerialNumber_With_2_Customers_And_Customer_Exist()
        {
            //Arrange
            var drawModel = new DrawModel
            {
                Email = "Jon@doe.com",
                FirstName = "jon",
                LastName = "Doe",
                SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5"
            };

            var customerModel = new CustomerModel()
            {
                Id = 0,
                Email = "Jon@doe.com",
                FirstName = "jon",
                LastName = "Doe",
            };

            _serialNumberRepository.Setup(x => x.ReadAsync("fef5d5af-6cc4-4ccc-bc68-c97443a909d5")).ReturnsAsync(new List<SerialNumberModel>
            {
                new() {
                    Id = 0,
                    SerialNumber = "fef5d5af-6cc4-4ccc-bc68-c97443a909d5",
                    FirstCustomer = new CustomerModel
                    {
                        Id = 1,
                        Email = "Bob@XL.com",
                        FirstName = "Bob",
                        LastName = "Big"
                    },
                    SecondCustomer = new CustomerModel
                    {
                        Id = 2,
                        Email = "Bog@XL.com",
                        FirstName = "Bog",
                        LastName = "Bigger"
                    }
                }
            });

            _customerRepository.Setup(x => x.ReadAsync(customerModel.Email)).ReturnsAsync(new List<CustomerModel>());

            _serialNumberRepository.Setup(x => x.UpdateAsync(It.IsAny<SerialNumberModel>()));

            //Act + Assert
            var ex = Assert.Throws<Exception>(() =>
            {
                new DrawLogic(_repositoryFacade.Object).AddToSerialNumber(drawModel);
            });

            Assert.That(ex, Is.Not.Null);
            Assert.That(ex?.Message, Is.EqualTo("Serial number fef5d5af-6cc4-4ccc-bc68-c97443a909d5 already has 2 customers."));
        }
    }
}
