
namespace VenderMachine.Models
{
    public class Soda : Abstract.IJuice
    {

        public override void Setup()
        {
            this.Name = "Soda";
            this.Price = 100;
        }
    }
}