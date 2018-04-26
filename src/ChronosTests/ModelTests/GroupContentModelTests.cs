using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronos.Entities;
using Chronos.Models;

namespace ChronosTests
{
    [TestClass]
    public class GroupContentModelTests
    {
        [TestMethod]
        public void ImplicitOperatorConvertsBetweenGroupModelAndGroupContentModel()
        {
            //Arrange
            Group group = new Group { GroupName = "Group1", Creator = 1, Id = 1 };

            //Act
            GroupContentModel GroupModel = group;

            //Assert
            Assert.AreEqual(GroupModel.GroupName, group.GroupName);
        }
    }
}
