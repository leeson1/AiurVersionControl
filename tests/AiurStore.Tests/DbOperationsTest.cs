﻿using AiurStore.Models;
using AiurStore.Providers;
using AiurStore.Tests.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AiurStore.Tests
{
    [TestClass]
    public class DbOperationsTest
    {
        [TestMethod]
        public void BasicTest()
        {
            var memoryDb = new MemoryAiurStoreDb<string>();
            //var fileDb = new FileAiurStoreDb<string>("aiur-store.txt");
            TestDb(memoryDb);
            //TestDb(fileDb);
        }

        private static void TestDb(InOutDatabase<string> store)
        {
            store.Clear();
            store.Add("House");
            store.Add("Home");
            store.Add("Room");
            TestExtends.AssertDb(store, "House", "Home", "Room");

            var afterHouse = store.GetAllAfter("House").ToArray();
            Assert.AreEqual("Home", afterHouse[0]);
            Assert.AreEqual("Room", afterHouse[1]);

            var afternull = store.GetAllAfter(afterWhich: null).ToArray();
            Assert.AreEqual("House", afternull[0]);
            Assert.AreEqual("Home", afternull[1]);
            Assert.AreEqual("Room", afternull[2]);
        }
    }
}
