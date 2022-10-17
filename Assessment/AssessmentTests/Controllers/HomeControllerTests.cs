using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assessment.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assessment.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Validation_0()
        {
            int num;
            Assert.IsFalse(HomeController.Validate("0", out num));
        }
        [TestMethod]
        public void Validation_1()
        {
            int num;
            Assert.IsTrue(HomeController.Validate("1", out num));
        }
        [TestMethod]
        public void Validation_2()
        {
            int num;
            Assert.IsTrue(HomeController.Validate("2", out num));
        }
        [TestMethod]
        public void Validation_199()
        {
            int num;
            Assert.IsTrue(HomeController.Validate("199", out num));
        }
        [TestMethod]
        public void Validation_200()
        {
            int num;
            Assert.IsTrue(HomeController.Validate("200", out num));
        }
        [TestMethod]
        public void Validation_201()
        {
            int num;
            Assert.IsFalse(HomeController.Validate("201", out num));
        }
        [TestMethod]
        public void Validation_negative()
        {
            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int num;
                Assert.IsFalse(HomeController.Validate($"{rand.Next(-500, -1)}", out num));
            }
        }
        [TestMethod]
        public void Validation_inrange()
        {
            var rand = new Random();
            for (int i = 0; i < 100; i++)
            {
                int num;
                Assert.IsTrue(HomeController.Validate($"{rand.Next(2, 199)}", out num));
            }
        }
        [TestMethod]
        public void Validation_text()
        {
            int num;
            Assert.IsFalse(HomeController.Validate("dskgt", out num));
        }
        [TestMethod]
        public void Validation_nummatch1()
        {
            int num;
            HomeController.Validate("400", out num);
            Assert.AreEqual(400, num);
        }
        [TestMethod]
        public void Validation_nummatch2()
        {
            int num;
            HomeController.Validate("123", out num);
            Assert.AreEqual(123, num);
        }
        [TestMethod]
        public void Validation_nummatch3()
        {
            int num;
            HomeController.Validate("3", out num);
            Assert.AreNotEqual(4, num);
        }
        [TestMethod]
        public void Validation_nummatch4()
        {
            int num;
            HomeController.Validate("car", out num);
            Assert.AreEqual(0, num);
        }
        [TestMethod]
        public void TestIndexNullView()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result.ViewName);

        }
        [TestMethod]
        public void TestIndexView()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

        }
        [TestMethod]
        public void TestIndexNullView1()
        {
            var controller = new HomeController();
            var result = controller.Index("45", "-1", "20") as ViewResult;
            Assert.IsNotNull(result.ViewName);

        }
        [TestMethod]
        public void TestIndexView1()
        {
            var controller = new HomeController();
            var result = controller.Index("sfd", "-1", "20") as ViewResult;
            Assert.AreEqual("Index", result.ViewName);

        }
    }
}