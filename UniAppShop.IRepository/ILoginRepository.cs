using System;
using UniAppShop.Model;

namespace UniAppShop.IRepository
{
    public interface ILoginRepository
    {
        Login GetLogin(string name, string password);
    }
}
