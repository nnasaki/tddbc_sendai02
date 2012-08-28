
namespace VenderMachine.Models
{
    public class Coke : Abstract.IJuice
    {

        public override void Setup()
        {
            this.Name = "Coke";
            this.Price = 120;
        }
    }
}