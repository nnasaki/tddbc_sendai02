
namespace VenderMachine.Models
{
    public class Water : Abstract.IJuice
    {

        public override void Setup()
        {
            this.Name = "Water";
            this.Price = 100;
        }
    }
}