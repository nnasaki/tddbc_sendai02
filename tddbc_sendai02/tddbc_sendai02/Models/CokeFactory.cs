
namespace VenderMachine.Models
{
    public class CokeFactory : Abstract.IJuiceFactory
    {
        protected override Abstract.IJuice CreateJuice()
        {
            var juice = new Coke();
            return juice;
        }
    }
}