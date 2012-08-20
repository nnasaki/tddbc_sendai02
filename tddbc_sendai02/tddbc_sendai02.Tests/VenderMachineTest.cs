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
            var vm = new VenderMachineController();
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
                var vm = new VenderMachineController();
                vm.Insert(actual);
                vm.AmountOfMoney.Is(expected);

            });
        }

        [TestMethod]
        public void 複数回お金を投入する()
        {
            var vm = new VenderMachineController();
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

        [TestMethod]
        public void 総計の初期値は常に０であること()
        {
            var vm = new VenderMachineController();
            vm.AmountOfMoney.Is(0);
        }

        [TestMethod]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(5, false)]
        [TestCase(10, true)]
        [TestCase(50, true)]
        [TestCase(100, true)]
        [TestCase(1000, true)]
        [TestCase(5000, false)]
        [TestCase(10000, false)]
        [TestCase(10001, false)]
        public void 想定外のお金を判定する()
        {
            TestContext.Run((int money, bool expected) =>
            {
                var vm = new VenderMachineController();
                bool ret = vm.AsDynamic().IsExpectedMoney(money);
                ret.Is(expected);
            });
        }

        [TestMethod]
        public void 想定外の壱円を投入して合計金額に加算されていないこと()
        {
            var vm = new VenderMachineController();
            vm.Insert(1);
            vm.AmountOfMoney.Is(0);
        }

        [TestMethod]
        public void 想定外の1万円を投入して1万円がお釣りとして出力されること()
        {
            var vm = new VenderMachineController();
            vm.Insert(10000).Is(10000);
        }

        [TestMethod]
        public void 想定内の500円を投入してお釣りが出力されない_０である_こと()
        {
            var vm = new VenderMachineController();
            vm.Insert(500).Is(0);
        }
    }
}
