using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome : IDisposable
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<String, Object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }
        public NavegandoNaPaginaHome()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<String, Object>();
        }


        [Fact]
        public void CarregandoPaginaHomeEVerificaTituloDaPagina()
        {
            //Arrange
            //Act
            driver.Navigate().GoToUrl("https://localhost:44309/");

            //Assert
            Assert.Contains("WebApp", driver.Title);
        }

        [Fact]
        public void CarregandoPaginaHomeEVerificaExistenciaLinkLoginHome()
        {
            //Arrange
            //Act
            driver.Navigate().GoToUrl("https://localhost:44309/");

            //Assert
            Assert.Contains("Login", driver.PageSource);
            Assert.Contains("Home", driver.PageSource);

        }

        [Fact]
        public void LogandoNoSistema()
        {
            driver.Navigate().GoToUrl("https://localhost:44309/");
            driver.Manage().Window.Size = new System.Drawing.Size(1524, 814);
            driver.FindElement(By.LinkText("Login")).Click();
            {
                var element = driver.FindElement(By.Id("btn-logar"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element).Perform();
            }
            {
                var element = driver.FindElement(By.TagName("body"));
                Actions builder = new Actions(driver);
                builder.MoveToElement(element, 0, 0).Perform();
            }
            driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            driver.FindElement(By.Id("Senha")).SendKeys("senha01");
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("btn-logar")).Click();
            driver.FindElement(By.Id("agencia")).Click();
            driver.FindElement(By.Id("home")).Click();
        }

        [Fact]
        public void ValidaLinkDeLoginHome()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/");

            var linkLogin = driver.FindElement(By.LinkText("Login"));
            
            //Act
            linkLogin.Click();

            //Assert
            Assert.Contains("img", driver.PageSource);
        }

        [Fact]
        public void TentaAcessarPaginaSemEstarLogado()
        {
            //Arrange
            //Act
            driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");
            //Assert
            Assert.Contains("401", driver.PageSource);
        }

        [Fact]
        public void AcessaPaginaSemEstarLogadoVerificaUrl()
        {
            //Arrange
            //Act
            driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");
            //Assert
            Assert.Contains("https://localhost:44309/Agencia/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);
        }


        public void Dispose()
        {
            driver.Quit();
        }
    }
}
