using System;
using UniAppShop.IRepository;
using UniAppShop.IService;
using UniAppShop.Model;

namespace UniAppShop.Service
{
    public class LoginService: ILoginService
    {
        public readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public Login GetLogin(string name, string password)
        {
            return _loginRepository.GetLogin(name, password);
        }
    }
}
