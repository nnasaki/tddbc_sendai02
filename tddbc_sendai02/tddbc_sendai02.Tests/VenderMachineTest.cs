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

        [TestMethod]
        public void 五十円玉と百円玉と五百円玉と千円札が1枚ずつ投入できること()
        {
            VenderMachineController vm = new VenderMachineController();
            vm.Insert(50);
            Assert.AreEqual(50, vm.AmountOfMoney);

            vm = new VenderMachineController();
            vm.Insert(100);
            Assert.AreEqual(100, vm.AmountOfMoney);

            vm = new VenderMachineController();
            vm.Insert(500);
            Assert.AreEqual(500, vm.AmountOfMoney);

            vm = new VenderMachineController();
            vm.Insert(1000);
            Assert.AreEqual(1000, vm.AmountOfMoney);
        }


    }
}
