using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Address
{
    public class GetUserAddressListResponse
    {
		/// <summary>
		/// 联系人
		/// </summary>
		public string linkman { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		public string sex { get; set; }

		/// <summary>
		/// 手机号码
		/// </summary>
		public string phone { get; set; }

		/// <summary>
		/// 收货地址
		/// </summary>
		public string address { get; set; }

		/// <summary>
		/// 标签
		/// </summary>
		public string addressType { get; set; }

		/// <summary>
		/// 是否设为默认地址
		/// </summary>
		public List<string> isDefault { get; set; }

		/// <summary>
		/// 门牌号
		/// </summary>
		public string tower { get; set; }
	}
}
