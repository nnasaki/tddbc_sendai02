
namespace VenderMachine.Models
{
    public class RedBull : Abstract.IJuice
    {

        public override void Setup()
        {
            this.Name = "RedBull";
            this.Price = 100;
        }
    }
}