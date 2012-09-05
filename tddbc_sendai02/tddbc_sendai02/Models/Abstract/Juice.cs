namespace VenderMachine.Models.Abstract
{
    /// <summary>
    /// ジュースのモデルクラス
    /// </summary>
    public abstract class Juice : System.IEquatable<Juice>
    {
        /// <summary>
        /// ジュースの名前
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ジュースの値段
        /// </summary>
        public int Price { get; set; }

        public bool Equals(Juice other)
        {
            if (other == null)
            {
                return false;
            }

            return (Name == other.Name &&
                Price == other.Price);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals((Juice)obj);
        }

        public override int GetHashCode()
        {
            return Price ^ Name.GetHashCode();
        }

        public abstract void Setup();
    }
}