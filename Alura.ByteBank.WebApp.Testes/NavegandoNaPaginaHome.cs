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
    public class NavegandoNaPaginaHome : IDisposable
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<String, Object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }
        private ITestOutputHelper SaidaConsoleTeste;

        public NavegandoNaPaginaHome(ITestOutputHelper saidaConsoleTeste)
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
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

        [Fact]
        public void RealizaLoginAcessaMenuECadastraCliente()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

            var login = driver.FindElement(By.Name("Email"));
            var senha = driver.FindElement(By.Name("Senha"));

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            driver.FindElement(By.CssSelector(".btn")).Click();
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
            Assert.Contains("Logout",driver.PageSource);
        }

        [Fact]
        public void RealizaLoginAcessaListagemDeContas()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

            var login = driver.FindElement(By.Name("Email"));
            var senha = driver.FindElement(By.Name("Senha"));

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            driver.FindElement(By.Id("btn-logar")).Click();

            driver.FindElement(By.Id("contacorrente")).Click();
            
            //Act
            var elements = driver.FindElements(By.TagName("a"));

            //foreach (var e in elements)
            //{
            //    SaidaConsoleTeste.WriteLine(e.Text);
            //}

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhe")
                            select webElemento).First();
            //Act
            elemento.Click();

            //Assert
            //Assert.True(elements.Count >= 12);
            Assert.Contains("Voltar", driver.PageSource);
        }


        public void Dispose()
        {
            driver.Quit();
        }
    }
}
