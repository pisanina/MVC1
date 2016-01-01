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
        // TESTY WYSZUKIWANIA OGLOSZEN

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
        public void SearchAddOnlySeller()
            // Nie da się wyszukac po samym Sprzedajacym, musza byc ceny
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:6487/");
            IWebElement query = driver.FindElement(By.Id("wyszukaj"));
            query.Click();
   
            query = driver.FindElement(By.Id("SprzedajacyID"));
            query.SendKeys("kz");
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

        // TESTY DODAWANIA OGLOSZEN

        [Test]
        public void AdAdd()
        {
            IWebDriver driver = new FirefoxDriver();
            TestyLogowanie Login = new TestyLogowanie();
            Login.Login(driver);


            driver.FindElement(By.Id("create")).Click();
            driver.FindElement(By.Id("Tytul")).SendKeys("Najnowsze");
            driver.FindElement(By.Id("Opis")).SendKeys("Specjalnie tak dobre");
            driver.FindElement(By.Id("Cena")).SendKeys("999000");
            driver.FindElement(By.Id("Create")).Click();
            driver.FindElement(By.TagName("td"));
            
            // Usuwanie dodanego przez test ogloszenia
            driver.FindElement(By.Id("wyszukaj")).Click();
            driver.FindElement(By.Id("CenaOd")).SendKeys("999000");
            driver.FindElement(By.Id("CenaDo")).SendKeys("999000");
            driver.FindElement(By.Id("szukaj")).Click();
            driver.FindElement(By.LinkText("Delete")).Click();
            driver.FindElement(By.Id("delete")).Click();
            
            // To jest sprawdzenie czy wrocil do indeksu ogloszen
            driver.FindElement(By.LinkText("Create New"));
            driver.Quit();
        }

        [Test]
        public void AdAddBezCeny()
        {
            IWebDriver driver = new FirefoxDriver();
            TestyLogowanie Login = new TestyLogowanie();
            Login.Login(driver);

            driver.FindElement(By.Id("create")).Click();
            driver.FindElement(By.Id("Tytul")).SendKeys("Nowe ogloszenie");
            driver.FindElement(By.Id("Opis")).SendKeys("To czego tak dlugo szukales");
            
            driver.FindElement(By.Id("Cena")).SendKeys("");
            driver.FindElement(By.Id("Create")).Click();
            // Sprawdzenie czy został w formularzu Dodaj ogloszenie
            // i czy wyswietlił się komunikat błędu
            driver.FindElement(By.Id("Create"));
            driver.FindElement(By.ClassName("field-validation-error"));
            driver.Quit();
        }

        [Test]
        public void AdAddNieprawidlowaCena() 
        {
            IWebDriver driver = new FirefoxDriver();
            TestyLogowanie Login = new TestyLogowanie();
            Login.Login(driver);

            driver.FindElement(By.Id("create")).Click();
            driver.FindElement(By.Id("Tytul")).SendKeys("Nowe ogloszenie");
            driver.FindElement(By.Id("Opis")).SendKeys("To czego tak dlugo szukales");
            // w polu cena sa wpisywane litery zamiast cyfr
            driver.FindElement(By.Id("Cena")).SendKeys("litery zamiast cyfr");
            driver.FindElement(By.Id("Create")).Click();
            // Sprawdzenie czy został w formularzu Dodaj ogloszenie
            // i czy wyswietlił się komunikat błędu
            driver.FindElement(By.Id("Create"));
            driver.FindElement(By.ClassName("field-validation-error"));
            driver.Quit();
        }
    }
}

