using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VenderMachine.Controllers;

namespace tddbc_sendai02.Tests
{
    [TestClass]
    public class VenderMachineTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void 十円玉を投入する()
        {
            VenderMachineController vm = new VenderMachineController();
            vm.Insert(10);
            Assert.AreEqual(10, vm.AmountOfMoney);
        }

        [TestMethod]
        [TestCase(50, 50)]
        [TestCase(100, 100)]
        [TestCase(500, 500)]
        [TestCase(1000, 1000)]
        public void 五十円玉と百円玉と五百円玉と千円札が1枚ずつ投入できること()
        {
            TestContext.Run((int expected, int actual) =>
            {
                VenderMachineController vm = new VenderMachineController();
                vm.Insert(actual);
                vm.AmountOfMoney.Is(expected);

            });
        }

        [TestMethod]
        public void 複数回お金を投入する()
        {
            VenderMachineController vm = new VenderMachineController();
            vm.Insert(10);
            vm.Insert(50);
            vm.AmountOfMoney.Is(60);
        }

        [TestMethod]
        public void 投入金額の総計を取得できる()
        {
            // ここのテストはすでに AmountOfMoney が実装されているのでしない
        }

        [TestMethod]
        public void 払い戻し操作を行い投入金額の総計を釣銭として出力する()
        {
            var vm = new VenderMachineController();
            vm.Insert(10);
            vm.Insert(100);
            vm.Refund().Is(110);
            vm.AmountOfMoney.Is(0);
        }
    }
}
