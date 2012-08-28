
namespace VenderMachine.Models
{
    public class SodaFactory : Abstract.IJuiceFactory
    {
        protected override Abstract.IJuice CreateJuice()
        {
            var juice = new Soda();
            return juice;
        }
    }
}