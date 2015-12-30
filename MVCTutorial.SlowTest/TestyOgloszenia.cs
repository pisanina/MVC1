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
    class TestyOgloszenia
    {

      
        [Test]
        public void SearchAdd()
        {


            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:6487/");
            IWebElement query = driver.FindElement(By.Id("wyszukaj"));
            query.Click();
            query = driver.FindElement(By.Id("Tytul"));
            query.SendKeys("ogloszenie");
            query = driver.FindElement(By.Id("CenaOd"));
            query.SendKeys("1");
            query = driver.FindElement(By.Id("CenaDo"));
            query.SendKeys("999999");
            query = driver.FindElement(By.Id("szukaj"));
            query.Click();
            query = driver.FindElement(By.TagName("td"));
            Assert.AreEqual(query.Text, "Nasze pierwsze ogloszenie1");
            driver.Quit();
        }


        [Test]
        public void AdAdd()
        {
            IWebDriver driver = new FirefoxDriver();
            TestyLogowanie Login = new TestyLogowanie();
            Login.Login(driver);
            

            driver.FindElement(By.Id("create")).Click();
            driver.FindElement(By.Id("Tytul")).SendKeys("Nowe ogloszenie");
            driver.FindElement(By.Id("Opis")).SendKeys("To czego tak dlugo szukales");
            driver.FindElement(By.Id("Cena")).SendKeys("99000");
            driver.FindElement(By.Id("Create")).Click();
            driver.FindElement(By.TagName("td"));
            driver.Quit();
        }

     /*   public void Login(IWebDriver driver)
        {
         
            driver.Navigate().GoToUrl("http://localhost:6487/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("UserName")).SendKeys("kz");
            driver.FindElement(By.Id("Password")).SendKeys("kzkzkz");
            driver.FindElement(By.Id("LoginSubmit")).Click();
            driver.FindElement(By.Id("logoutForm"));

        } */
    }
}

