using OpenQA.Selenium;

namespace Alura.ByteBank.WebApp.Testes.PageObjects
{
    public class HomePO
    {
        private readonly IWebDriver driver;
        private readonly By linkHome;
        private readonly By linkContaCorrente;
        private readonly By linkClientes;
        private readonly By linkAgencias;
        private readonly By linkLogout;

        public HomePO(IWebDriver driver)
        {
            this.driver = driver;
            linkHome = By.Id("home");
            linkContaCorrente = By.Id("contacorrente");
            linkClientes = By.Id("clientes");
            linkAgencias = By.Id("agencia");
            linkLogout = By.Id("btn-logout");
        }

        public void Navegar(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void LinkHomeClick()
        {
            driver.FindElement(linkHome).Click();
        }

        public void LinkContaCorrenteClick()
        {
            driver.FindElement(linkContaCorrente).Click();
        }

        public void LinkClientesClick()
        {
            driver.FindElement(linkClientes).Click();
        }

        public void LinkAgenciasClick()
        {
            driver.FindElement(linkAgencias).Click();
        }

        public void LinkLogoutClick()
        {
            driver.FindElement(linkLogout).Click();
        }
    }
}
