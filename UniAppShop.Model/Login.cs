using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace UniAppShop.Model
{
    /// <summary></summary>
    [Serializable]
    [DataObject]
    [BindTable("login", Description = "", ConnName = "MySql", DbType = DatabaseType.None)]
    public partial class Login
    {
        #region 属性
        private Int32 _id;
        /// <summary></summary>
        [DisplayName("id")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("id", "", "int")]
        public Int32 id { get => _id; set { if (OnPropertyChanging("id", value)) { _id = value; OnPropertyChanged("id"); } } }

        private String _Name;
        /// <summary></summary>
        [DisplayName("Name")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("name", "", "varchar(255)", Master = true)]
        public String Name { get => _Name; set { if (OnPropertyChanging("Name", value)) { _Name = value; OnPropertyChanged("Name"); } } }

        private String _Password;
        /// <summary></summary>
        [DisplayName("Password")]
        [DataObjectField(false, false, true, 255)]
        [BindColumn("password", "", "varchar(255)")]
        public String Password { get => _Password; set { if (OnPropertyChanging("Password", value)) { _Password = value; OnPropertyChanged("Password"); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case "id": return _id;
                    case "Name": return _Name;
                    case "Password": return _Password;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case "id": _id = value.ToInt(); break;
                    case "Name": _Name = Convert.ToString(value); break;
                    case "Password": _Password = Convert.ToString(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得Login字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary></summary>
            public static readonly Field id = FindByName("id");

            /// <summary></summary>
            public static readonly Field Name = FindByName("Name");

            /// <summary></summary>
            public static readonly Field Password = FindByName("Password");

            static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        /// <summary>取得Login字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary></summary>
            public const String id = "id";

            /// <summary></summary>
            public const String Name = "Name";

            /// <summary></summary>
            public const String Password = "Password";
        }
        #endregion
    }
}