using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VenderMachine.Controllers;

namespace tddbc_sendai02.Tests
{
    [TestClass]
    public class VenderMachineTest
    {
        [TestMethod]
        public void 十円玉を投入する()
        {
            VenderMachineController vm = new VenderMachineController();
            vm.Insert(10);
            Assert.AreEqual(10, vm.AmountOfMoney);
        }
    }
}
