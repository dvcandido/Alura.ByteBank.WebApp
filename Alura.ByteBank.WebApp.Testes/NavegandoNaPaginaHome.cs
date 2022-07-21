using Alura.ByteBank.WebApp.Testes.PageObjects;
using Alura.ByteBank.WebApp.Testes.Utilitarios;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome : IClassFixture<Gerenciador>
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<String, Object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }
        private ITestOutputHelper SaidaConsoleTeste;

        public NavegandoNaPaginaHome(Gerenciador gerenciador, ITestOutputHelper saidaConsoleTeste)
        {
            driver = gerenciador.Driver;
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<String, Object>();
            SaidaConsoleTeste = saidaConsoleTeste;
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

            var loginPO = new LoginPO(driver);
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.Logar();

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
            var home = new HomePO(driver);
            home.LinkLogoutClick();
            //Act
            home.Navegar("https://localhost:44309/Agencia/Index");
            //Assert
            Assert.Contains("401", driver.PageSource);
        }

        [Fact]
        public void AcessaPaginaSemEstarLogadoVerificaUrl()
        {
            //Arrange
            var home = new HomePO(driver);
            home.Navegar("https://localhost:44309/Home/Index");
            home.LinkLogoutClick();
            //Act
            home.Navegar("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("https://localhost:44309/Agencia/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);
        }

        [Fact]
        public void RealizaLoginAcessaMenuECadastraCliente()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.Logar();

            driver.FindElement(By.LinkText("Cliente")).Click();
            driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            driver.FindElement(By.Name("Identificador")).Click();
            driver.FindElement(By.Name("Identificador")).SendKeys("b96f2ac2-b142-41ae-ab81-98eb8d783436");
            driver.FindElement(By.Name("CPF")).Click();
            driver.FindElement(By.Name("CPF")).SendKeys("69981034096");
            driver.FindElement(By.Name("Nome")).Click();
            driver.FindElement(By.Name("Nome")).SendKeys("Tobey Garfield");
            driver.FindElement(By.Name("Profissao")).Click();
            driver.FindElement(By.Name("Profissao")).SendKeys("Cientista");

            //Act
            driver.FindElement(By.CssSelector(".btn-primary")).Click();
            driver.FindElement(By.LinkText("Home")).Click();

            //Assert
            Assert.Contains("Logout", driver.PageSource);
        }

        [Fact]
        public void RealizaLoginAcessaListagemDeContas()
        {
            //Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.Logar();

            //Act
            var homePO = new HomePO(driver);
            homePO.LinkContaCorrenteClick();

            //Assert
            Assert.Contains("Adicionar Conta-Corrente", driver.PageSource);
        }


        public void Dispose()
        {
            driver.Quit();
        }
    }
}
