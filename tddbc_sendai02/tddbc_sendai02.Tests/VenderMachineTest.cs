﻿using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VenderMachine.Controllers;
using VenderMachine.Models;
using System.Linq;

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
        public void 想定内の500円を投入してお釣りが出力されない_０であること()
        {
            var vm = new VenderMachineController();
            vm.Insert(500).Is(0);
        }

        [TestMethod]
        public void 初期状態でコーラがあること()
        {
            var vm = new VenderMachineController();
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.StockOfJuice.Contains(coke).Is(true);

            var cokeMissPrice = cokeFactory.Create();
            cokeMissPrice.Price = 100;
            vm.StockOfJuice.Contains(cokeMissPrice).Is(false);
        }

        [TestMethod]
        public void 初期状態でコーラが5本あること()
        {
            var vm = new VenderMachineController();
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.StockOfJuice.Count(x => (x.Name == coke.Name && x.Price == coke.Price)).Is(5);
        }

        [TestMethod]
        public void 格納されているジュースの名前を取得できる()
        {
            var vm = new VenderMachineController();
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            var stock = vm.GetStockOfJuiceInfo().Single(x=>x.Name == coke.Name);
            stock.Is(s => s.Name == coke.Name && s.Price == coke.Price && s.CanOfJuice == 5);
        }

        [TestMethod]
        public void 投入金額が不足していてコーラが購入できないと判定されること()
        {
            var vm = new VenderMachineController();
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.IsPurchase(coke).Is(false);

            vm.Insert(100);
            vm.IsPurchase(coke).Is(false);
        }

        [TestMethod]
        public void コーラが購入できると判定されること()
        {
            var vm = new VenderMachineController();
            vm.Insert(100);
            vm.Insert(10);
            vm.Insert(10);

            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.IsPurchase(coke).Is(true);
        }

        [TestMethod]
        public void ジュースを購入するとジュースの在庫が減って売り上げ金額が増えること()
        {
            var vm = new VenderMachineController();
            vm.Insert(500);
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.Purchase(coke);

            vm.AmountOfMoney.Is(380);
            vm.SaleAmount.Is(120);

            vm.GetStockOfJuiceInfo().First().CanOfJuice.Is(4);

        }

        [TestMethod]
        public void 現在の売上金額が取得できること()
        {
            var vm = new VenderMachineController();
            vm.Insert(500);
            vm.SaleAmount.Is(0);

            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.Purchase(coke);
            vm.SaleAmount.Is(120);
        }

        [TestMethod]
        public void 投入金額が足りないので購入操作を行っても何もしないこと()
        {
            var vm = new VenderMachineController();
            vm.Insert(100);
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.Purchase(coke);

            vm.AmountOfMoney.Is(100);
            vm.SaleAmount.Is(0);
            var stock = vm.GetStockOfJuiceInfo().Single(x => x.Name == coke.Name);
            stock.CanOfJuice.Is(5);
        }

        [TestMethod]
        public void 在庫が足りないので購入操作を行っても何もしないこと()
        {
            var vm = new VenderMachineController();
            vm.Insert(1000);
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            // 5回買う
            for (int i = 0; i < 5; i++)
            {
                vm.Purchase(coke);                
            }

            // 在庫がないことを確認して、もう一度購入してみる
            vm.AmountOfMoney.Is(400);
            vm.SaleAmount.Is(600);
            var stock = vm.GetStockOfJuiceInfo().SingleOrDefault(x => x.Name == coke.Name);
            stock.IsNull();

            vm.Purchase(coke);

            vm.AmountOfMoney.Is(400);
            vm.SaleAmount.Is(600);
            stock.IsNull();
        }

        [TestMethod]
        public void 釣銭を出力する()
        {
            var vm = new VenderMachineController();
            vm.Insert(100);
            vm.Insert(50);
            var cokeFactory = new CokeFactory();
            var coke = cokeFactory.Create();
            vm.Purchase(coke);

            vm.Refund().Is(30);
        }

        [TestMethod]
        public void 在庫にレッドブルが5本あること()
        {
            var vm = new VenderMachineController();
            var factory = new RedBullFactory();
            var redbull = factory.Create();
            var stock = vm.GetStockOfJuiceInfo().SingleOrDefault(x => x.Name == redbull.Name);
            Debug.Assert(stock != null, "stock != null");
            stock.CanOfJuice.Is(5);
        }

        [TestMethod]
        public void レッドブルが200円であること()
        {
            new RedBullFactory().Create().Is(x => x.Price == 200);
        }

        [TestMethod]
        public void 在庫に水が5本あること()
        {
            var vm = new VenderMachineController();
            var water = new WaterFactory().Create();
            var stock = vm.GetStockOfJuiceInfo().SingleOrDefault(x => x.Name == water.Name);
            Debug.Assert(stock != null, "stock != null");
            stock.CanOfJuice.Is(5);
        }

        [TestMethod]
        public void 水が100円であること()
        {
            new WaterFactory().Create().Is(x => x.Price == 100);
        }

        [TestMethod]
        public void 投入金額が不足していて購入可能なドリンクが無いこと()
        {
            var vm = new VenderMachineController();
            vm.Insert(10);
            vm.GetAvailableForPurchaseList().Is(x => x.Count == 0);
        }

        [TestMethod]
        public void 水が購入可能となっていること()
        {
            var vm = new VenderMachineController();
            vm.Insert(50);
            vm.Insert(50);
            var water = new WaterFactory().Create();
            var coke = new CokeFactory().Create();
            var redBull = new RedBullFactory().Create();
            var list = vm.GetAvailableForPurchaseList();
            list.Contains(water).Is(true);
            list.Contains(coke).Is(false);
            list.Contains(redBull).Is(false);
        }

        [TestMethod]
        public void 水とコーラが購入可能となっていること()
        {
            var vm = new VenderMachineController();
            vm.Insert(50);
            vm.Insert(100);
            var water = new WaterFactory().Create();
            var coke = new CokeFactory().Create();
            var redBull = new RedBullFactory().Create();
            var list = vm.GetAvailableForPurchaseList();
            list.Contains(water).Is(true);
            list.Contains(coke).Is(true);
            list.Contains(redBull).Is(false);
        }

        [TestMethod]
        public void 水とコーラとレッドブルが購入可能となっていること()
        {
            var vm = new VenderMachineController();
            vm.Insert(50);
            vm.Insert(100);
            vm.Insert(100);
            var water = new WaterFactory().Create();
            var coke = new CokeFactory().Create();
            var redBull = new RedBullFactory().Create();
            var list = vm.GetAvailableForPurchaseList();
            list.Contains(water).Is(true);
            list.Contains(coke).Is(true);
            list.Contains(redBull).Is(true);
        }
    }
}
