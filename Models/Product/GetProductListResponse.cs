using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Product
{
    public class GetProductListResponse
    {
        /// <summary>
        /// 商品类型
        /// </summary>
        public string classify { get; set; }

        /// <summary>
        /// 商品类型描述
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        public List<shopData> shopData { get; set; }
    }

    /// <summary>
    /// 商品
    /// </summary>
    public class shopData
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 商品英文名称
        /// </summary>
        public string en { get; set; }

        /// <summary>
        /// 产品(规格,温度，奶油)枚举
        /// </summary>
        public List<Kind> kind { get; set; }

        /// <summary>
        /// 默认属性值
        /// </summary>
        public List<string> Default {get;set;}

        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
    }

    public class Kind
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public List<string> type { get; set; }

        /// <summary>
        /// 已选的字典值
        /// </summary>
        public int selected { get; set; }
    }
}
