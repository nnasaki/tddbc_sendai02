using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VenderMachine.Models.Abstract
{
    /// <summary>
    /// ジュースのモデルクラス
    /// </summary>
    public abstract class IJuice : System.IEquatable<IJuice>
    {
        /// <summary>
        /// ジュースの名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ジュースの値段
        /// </summary>
        public int Price { get; set; }

        public bool Equals(IJuice other)
        {
            if (other == null)
            {
                return false;
            }

            return (this.Name == other.Name &&
                this.Price == other.Price);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals((IJuice)obj);
        }

        public override int GetHashCode()
        {
            return this.Price ^ this.Name.GetHashCode();
        }

        public abstract void Setup();
    }
}