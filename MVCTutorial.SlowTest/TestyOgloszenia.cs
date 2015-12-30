using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCTutorial.SlowTest.PageObjects;
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
            Assert.AreEqual(query.Text,"Nasze pierwsze ogloszenie1");
            driver.Quit();
        }


        [Test]
        public void AdAdd()
        {
            void logowanie();
            
            
            
            
            IWebElement query = driver.FindElement(By.Id("create"));
            query.Click();
            
            
           /* query = driver.FindElement(By.Id("Tytul"));
            query.SendKeys("Nowe ogloszenie");
            query = driver.FindElement(By.Id("Opis"));
            query.SendKeys("To czego tak dlugo szukales");
            query = driver.FindElement(By.Id("Cena"));
            query.SendKeys("99000");
            query = driver.FindElement(By.Id("Create"));
            query.Click();
            * 
            query = driver.FindElement(By.TagName("td"));
            Assert.AreEqual(query.Text, "Nasze pierwsze ogloszenie1"); */
            driver.Quit();
        }
    
}
