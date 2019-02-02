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

        /// <summary>
        /// Using this test to step through GetShowcaseInfo() 
        /// as well as view HtmlAgilityPack nodes and their attributes
        /// </summary>
        [TestMethod]
        public void Test_GetShowCaseInfo()
        {
            // arrange

            // act
            var actual = ShowcaseRepo.GetShowcaseInfo("/madewith/hollow-knight");

            // assert

        }

        /// <summary>
        /// Using this test to step through GetShowcaseInfo() 
        /// as well as view HtmlAgilityPack nodes and their attributes
        /// </summary>
        [TestMethod]
        public void Test_GetShowCaseInfo2()
        {
            // arrange

            // act
            var actual = ShowcaseRepo.GetShowcaseInfo("/madewith/praey-for-the-gods");

            // assert

        }
    }
}
