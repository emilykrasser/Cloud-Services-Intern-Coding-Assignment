using System;
using MadeWithUnityRandomShowcase.Controllers;
using MadeWithUnityRandomShowcase.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MadeWithUnityRandomShowcase.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIndex()
        {
            // arrange
            var controller = new HomeController();
            
            // act
            var actual = controller.Index();

            // assert
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void Test_GetShowCaseInfo()
        {
            // arrange

            // act
            var actual = ShowcaseRepo.GetShowcaseInfo();

            // assert

        }
    }
}
