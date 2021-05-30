using System;
using UniAppShop.IRepository;
using UniAppShop.Model;
using XCode.DataAccessLayer;
using System.Linq;

namespace UniAppShop.Repository
{
    public class LoginRepository: ILoginRepository
    {
        public Login GetLogin(string name, string password)
        {
            //var dal = DAL.Create("MySql");
            //var db = dal.Query("select * from Login");
            //var login = Login.LoadData(db);//抛异常，没有成功
            var login = Login.Find(Login._.Name, name);
            return login;
        }
    }
}
