using OpenQA.Selenium;

namespace Alura.ByteBank.WebApp.Testes.PageObjects
{
    public class LoginPO
    {
        private readonly IWebDriver driver;
        private readonly By campoEmail;
        private readonly By campoSenha;
        private readonly By btnLogar;

        public LoginPO(IWebDriver driver)
        {
            this.driver = driver;
            this.campoEmail = By.Id("Email");
            this.campoSenha = By.Id("Senha");
            this.btnLogar = By.Id("btn-logar");
        }

        public void Navegar(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void PreencherCampos(string email, string senha)
        {
            driver.FindElement(campoSenha).SendKeys(senha);
            driver.FindElement(campoEmail).SendKeys(email);
        }

        public void Logar()
        {
            driver.FindElement(btnLogar).Click();
        }
        public void Deslogar()
        {
            driver.FindElement(btnLogar).Click();
        }
    }
}
