using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VenderMachine.Models
{
    /// <summary>
    /// ジュースの在庫情報
    /// </summary>
    public class StockOfJuiceInfo
    {
        /// <summary>
        /// 値段
        /// </summary>
        public int Price { get; set; }
        
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 本数
        /// </summary>
        public int CanOfJuice { get; set; }
    }
}