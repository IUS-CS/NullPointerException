using System;
using System.Collections.Generic;
using Chronos.Abstract;
using Chronos.Controllers;
using Chronos.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChronosTests.ControllerTests
{
    [TestClass]
    public class UserControllerTests
    {
        [TestMethod]
        public void SearchUserReturnsPartialView() //TODO mock session
        {
            //Arrange
            Mock<IGroupRepository> groupRepo = new Mock<IGroupRepository>();
            groupRepo.Setup(x => x.GetMembersByGroupId(It.IsAny<int>())).Returns(new List<User>());
            Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
            userRepo.Setup(x => x.SearchUserInvite(It.IsAny<string>(), It.IsAny<List<int>>())).Returns(new List<User>());
            UserController controller = new UserController(userRepo.Object, groupRepo.Object);

            //Act
            var result = controller.SearchUser("", 0);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
