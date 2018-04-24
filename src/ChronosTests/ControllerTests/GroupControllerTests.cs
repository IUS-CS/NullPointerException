using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronos.Controllers;
using Moq;
using Chronos.Entities;
using Chronos.Abstract;

namespace ChronosTests.ControllerTests
{
    [TestClass]
    public class GroupControllerTests
    {
        private Mock<IGroupRepository> mRepo;

        [TestInitialize]
        public void Initialize()
        {
            mRepo = new Mock<IGroupRepository>();
        }

        [TestMethod]
        public void CreateGroupCreatesANewGroup()
        {
            //Arrange
            mRepo.Setup(m => m.CreateGroup(It.IsAny<string>(), It.IsAny<int>())).Verifiable();
            var mController = new Mock<GroupController>(mRepo.Object).Object;
            Group group = new Group
            {
                GroupName = "name",
                Creator = 1,
                Id = 1
            };

            //Act
            mController.CreateGroup(group);

            //Assert
            mRepo.Verify(m => m.CreateGroup(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
            
        }
    }
}
