using System;
using UniAppShop.Model;

namespace UniAppShop.IService
{
    public interface ILoginService
    {
        Login GetLogin(string name, string password);
    }
}
