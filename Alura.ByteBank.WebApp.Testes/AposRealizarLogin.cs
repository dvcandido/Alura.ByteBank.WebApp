using Alura.ByteBank.WebApp.Testes.PageObjects;
using Alura.ByteBank.WebApp.Testes.Utilitarios;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;
namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin : IClassFixture<Gerenciador>
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<String, Object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }
        public AposRealizarLogin(Gerenciador gerenciador)
        {
            driver = gerenciador.Driver;
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<String, Object>();
        }


        [Fact]
        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            //Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            loginPO.PreencherCampos("andre@email.com", "senha01");

            //Act
            loginPO.Logar();

            //Assert
            Assert.Contains("Agência", driver.PageSource);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
