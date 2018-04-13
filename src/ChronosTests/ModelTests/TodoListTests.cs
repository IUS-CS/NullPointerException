using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronos.Models;
using System.Collections.Generic;

namespace ChronosTests
{
    [TestClass]
    public class TodoListTests
    {
        [TestMethod]
        public void addItemAddsNewItemToTheList()
        {
            //Arrange
            TodoList list = new TodoList
            {
                Items = new List<string>()
            };

            //Act
            list.addItem("New Item");

            //Assert
            Assert.IsTrue(list.Items.Count == 1);
        }
    }
}
