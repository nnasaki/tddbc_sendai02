
namespace VenderMachine.Models
{
    public class Juice : System.IEquatable<Juice>
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public bool Equals(Juice other)
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

            return this.Equals((Juice)obj);
        }

        public override int GetHashCode()
        {
            return this.Price ^ this.Name.GetHashCode();
        }
    }
}
