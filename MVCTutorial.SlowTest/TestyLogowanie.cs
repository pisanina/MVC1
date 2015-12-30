using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumWebdriverHelpers;

namespace MVCTutorial.SlowTest
{
    [TestFixture]
    class TestyLogowanie
    {
        [Test]
        public void Logowanie()
        {


            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:6487/");
            try
            {
                driver.FindElement(By.Id("logoutForm"));
                Assert.IsTrue(false, "logout button exists before login");
            }
            catch (NoSuchElementException) { }

            driver.FindElement(By.Id("loginLink")).Click();

            driver.FindElement(By.Id("UserName")).SendKeys("kz");
            driver.FindElement(By.Id("Password")).SendKeys("kzkzkz");
            driver.FindElement(By.Id("LoginSubmit")).Click();
            driver.FindElement(By.Id("logoutForm"));
            //Assert.AreEqual(query.Text,"Nasze pierwsze ogloszenie1");
            driver.Quit();
        }

        public void Login(IWebDriver driver)
        {

            driver.Navigate().GoToUrl("http://localhost:6487/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).SendKeys("kz");
            driver.FindElement(By.Id("Password")).SendKeys("kzkzkz");
            driver.FindElement(By.Id("LoginSubmit")).Click();
            driver.FindElement(By.Id("logoutForm"));

        }
    }
}