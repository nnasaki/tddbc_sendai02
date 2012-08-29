
namespace VenderMachine.Models
{
    public class RedBullFactory : Abstract.IJuiceFactory
    {
        protected override Abstract.IJuice CreateJuice()
        {
            var juice = new RedBull();
            return juice;
        }
    }
}