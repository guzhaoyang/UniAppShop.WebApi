using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniAppShop.WebApi.Models;
using ViewModels.Product;

namespace UniAppShop.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpPost("GetProductList")]
        public BaseResponse GetProductList()
        {
            try
            {
                List<Kind> kinds = new List<Kind>()
                {
                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 1 },
                    new Kind(){ name = "糖度", type = new List<string>(){ "单糖","半糖"}, selected = 0 },
                };
                List<Kind> kinds1 = new List<Kind>()
                {
                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 1 },
                    new Kind(){ name = "奶油", type = new List<string>(){ "默认奶油", "无奶油"}, selected = 0 },
                };
                //商品类型、商品信息、商品属性字典（即种类，例如：颜色、大小等）
                List<GetProductListResponse> response = new List<GetProductListResponse>()
                {
                    new GetProductListResponse(){ classify = "人气Top", state = "", shopData = new List<shopData>()
                    {
                        new shopData(){ id = "shop01", title = "榛果拿铁", en = "Hazelnut Latte", kind = kinds
                        , Default = new List<string>(){ "大", "单糖", "热" }, price = "27" },
                        new shopData()
                        {
                            id = "shop02",
                            title = "圣诞姜饼人拿铁",
                            en = "Christmas Gingerbread Latte",
                            kind = kinds1,
                            Default = new List<string>(){ "大","默认奶油","热" },
                            price = "24"
                        }
                    } },
                    new GetProductListResponse()
                    {
                        classify = "大师咖啡",
                        state = "WBC（世界咖啡师大赛）冠军团队拼配",
                        shopData = new List<shopData>()
                        {
                            new shopData()
                            {
                                id = "shop03",
					            title = "拿铁",
					            en = "Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "24"
                            },
                            new shopData()
                            {
                                id = "shop04",
                                title = "香草拿铁",
                                en = "Vanilla Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "27"
                            }
                        }
                    },
                    new GetProductListResponse()
                    {
                        classify = "零度拿铁",
                        state = "不含咖啡的拿铁",
                        shopData = new List<shopData>()
                        {
                            new shopData()
                            {
                                id = "shop05",
                                title = "拿铁",
                                en = "Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "24"
                            },
                            new shopData()
                            {
                                id = "shop06",
                                title = "香草拿铁",
                                en = "Vanilla Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "27"
                            }
                        }
                    },
                    new GetProductListResponse()
                    {
                        classify = "瑞纳冰",
                        state = "",
                        shopData = new List<shopData>()
                        {
                            new shopData()
                            {
                                id = "shop07",
                                title = "拿铁",
                                en = "Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "24"
                            },
                            new shopData()
                            {
                                id = "shop08",
                                title = "香草拿铁",
                                en = "Vanilla Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "27"
                            }
                        }
                    },
                    new GetProductListResponse()
                    {
                        classify = "经典饮品",
                        state = "",
                        shopData = new List<shopData>()
                        {
                            new shopData()
                            {
                                id = "shop09",
                                title = "拿铁",
                                en = "Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "24"
                            },
                            new shopData()
                            {
                                id = "shop10",
                                title = "香草拿铁",
                                en = "Vanilla Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "27"
                            }
                        }
                    },
                    new GetProductListResponse()
                    {
                        classify = "健康轻食",
                        state = "",
                        shopData = new List<shopData>()
                        {
                            new shopData()
                            {
                                id = "shop11",
                                title = "拿铁",
                                en = "Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "24"
                            },
                            new shopData()
                            {
                                id = "shop12",
                                title = "香草拿铁",
                                en = "Vanilla Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 0 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "27"
                            }
                        }
                    },
                    new GetProductListResponse()
                    {
                        classify = "鲜榨果蔬汁",
                        state = "",
                        shopData = new List<shopData>()
                        {
                            new shopData()
                            {
                                id = "shop13",
                                title = "NFC鲜榨橙汁",
                                en = "NFC Fresh Orange Juice",
                                kind = new List<Kind>(),
                                Default = new List<string>(),
                                price = "24"
                            },
                            new shopData()
                            {
                                id = "shop14",
                                title = "香草拿铁",
                                en = "Vanilla Latte",
                                kind = new List<Kind>()
                                {
                                    new Kind(){ name = "规格", type = new List<string>(){ "大"}, selected = 0 },
                                    new Kind(){ name = "温度", type = new List<string>(){ "冰","热"}, selected = 1 },
                                    new Kind(){ name = "糖度", type = new List<string>(){ "无糖","半糖","单糖"}, selected = 0 },
                                },
                                Default = new List<string>(){ "大","无糖","热" },
                                price = "27"
                            }
                        }
                    },
                };
                return BaseResponse.ToResponse<List<GetProductListResponse>>(BackResult.Success, data: response);
            }
            catch(Exception exc)
            {
                return BaseResponse.ToResponse(BackResult.Exception, message: exc.Message);
            }
        }
    }
}
