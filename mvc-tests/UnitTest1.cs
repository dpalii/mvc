using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace YourNamespace
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void UpdateView_DisplayUserDetails()
        {
            var consoleMock = new Mock<IConsole>();

            // Arrange
            var model = new User("John Doe", 30);
            var view = new UserView(consoleMock.Object);
            var controller = new UserController(model, view);

            // Act
            controller.UpdateView();

            // Assert
            consoleMock.Verify(v => v.WriteLine($"Name: {model.Name}"), Times.Once());
            consoleMock.Verify(v => v.WriteLine($"Age: {model.Age}"), Times.Once());
        }

        [TestMethod]
        public void UpdateModel_UpdateUserModel()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();

            var model = new User("John Doe", 30);
            var view = new UserView(consoleMock.Object);
            var controller = new UserController(model, view);
            var newUser = new User("Jane Smith", 25);

            // Act
            controller.UpdateModel(newUser);

            // Assert
            Assert.AreEqual(newUser.Name, model.Name);
            Assert.AreEqual(newUser.Age, model.Age);
        }
    }

    [TestClass]
    public class UserViewTests
    {
        [TestMethod]
        public void GetUserInput_ValidInput_ReturnsUser()
        {
            // Arrange
            string name = "John Doe";
            int age = 30;
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns(name)
                .Returns(age.ToString());

            var view = new UserView(consoleMock.Object);

            // Act
            User user = view.GetUserInput();

            // Assert
            Assert.AreEqual(name, user.Name);
            Assert.AreEqual(age, user.Age);
        }

        [TestMethod]
        public void GetUserInput_NullName_ThrowsException()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.Setup(c => c.ReadLine()).Returns((string?)null);

            var view = new UserView(consoleMock.Object);

            // Act & Assert
            Assert.ThrowsException<Exception>(() => view.GetUserInput());
        }

        [TestMethod]
        public void GetUserInput_NullAge_ThrowsException()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("John Doe")
                .Returns((string?)null);

            var view = new UserView(consoleMock.Object);

            // Act & Assert
            Assert.ThrowsException<Exception>(() => view.GetUserInput());
        }

        [TestMethod]
        public void GetUserInput_InvalidAge_ThrowsException()
        {
            // Arrange
            var consoleMock = new Mock<IConsole>();
            consoleMock.SetupSequence(c => c.ReadLine())
                .Returns("John Doe")
                .Returns("-25");

            var view = new UserView(consoleMock.Object);

            // Act & Assert
            Assert.ThrowsException<Exception>(() => view.GetUserInput());
        }
    }
}
