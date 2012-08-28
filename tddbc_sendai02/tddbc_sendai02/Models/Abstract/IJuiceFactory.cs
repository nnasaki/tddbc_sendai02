
namespace VenderMachine.Models.Abstract
{
    public abstract class IJuiceFactory
    {
        public IJuice Create()
        {
            var juice = CreateJuice();
            juice.Setup();
            return juice;
        }

        abstract protected IJuice CreateJuice();
    }
}