using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniAppShop.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EasyCachingTestController : BaseController
    {
        private readonly IEasyCachingProviderFactory _factory;
        public EasyCachingTestController(IEasyCachingProviderFactory factory)
        {
            _factory = factory;
        }

        [HttpGet]
        [Route("GetCache")]
        public string Get(string name = EasyCachingConstValue.DefaultInMemoryName)
        {
            var provider = _factory.GetCachingProvider(name);
            var val = name.Equals("cus") ? "cus" : "default";
            //var res = provider.Get("demo", () => val, TimeSpan.FromMinutes(1));//如果没有，就执行() => val委托
            var res = provider.Get<string>("demo");
            if(res == null)
            {
                //res = "no cache";
                //创建实例，保存到缓存里，保留10秒
            }
            return $"cached value : {res}";
        }

        [HttpGet]
        [Route("SetCache")]
        public string Set(string name = EasyCachingConstValue.DefaultInMemoryName)
        {
            var provider = _factory.GetCachingProvider(name);
            var val = name.Equals("cus") ? "cus" : "default";
            provider.Set<string>("demo", val, TimeSpan.FromMinutes(1));
            return $"cached value : {val}";
        }
    }
}
