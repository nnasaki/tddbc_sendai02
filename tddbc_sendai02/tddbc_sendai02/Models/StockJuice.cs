
namespace VenderMachine.Models
{
    public class StockJuice : System.IEquatable<StockJuice>
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public bool Equals(StockJuice other)
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

            return this.Equals((StockJuice)obj);
        }

        public override int GetHashCode()
        {
            return this.Price ^ this.Name.GetHashCode();
        }
    }
}
