using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumTest;

namespace Tests
{
    [TestClass]
    public class AppTests
    {
        //Food in the following order: entrée, side, drink, dessert
        //Period as "morning" and "night"
        [TestMethod]
        public void FollowingOrder()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 2, 1, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "eggs, toast, coffee");

            input = "night, 2, 4, 3, 2, 1, 2";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "steak, potato(x3), wine, cake");
        }

        //At least one selection of each dish type
        [TestMethod]
        public void AtLeastOneSeletion()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "coffee, error");

            input = "night, 2, 4";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "potato, cake, error");
        }

        //Print error only at final
        [TestMethod]
        public void ErrorOnlyAtFinal()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 3, 3, 2, 5, 1, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "eggs, toast, coffee(x3), error");

            input = "night, 1, 2, 7, 4, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "steak, potato, wine, cake, error");
        }

        //Desserts are not allowed at morning
        [TestMethod]
        public void NoDessertAtMorning()
        {
            const string option = "4";
            const Period period = Period.Morning;
            const bool expectedBehavior = true;

            var error = false;
            var type = Program.GetType(option, period, ref error);

            Assert.AreEqual(error, expectedBehavior);
            Assert.AreEqual(type, null);
        }

        //Input is not case sensitive
        [TestMethod]
        public void NotCaseSensitive()
        {
            var input1 = new List<string> {"morning", "1", "2", "3", "4"};
            var error1 = false;

            var return1 = Program.GetPeriod(input1, ref error1);

            var input2 = new List<string> { "morning", "1", "2", "3", "4" };
            var error2 = false;

            var return2 = Program.GetPeriod(input2, ref error2);

            Assert.AreEqual(error1, error2);
            Assert.AreEqual(return1, return2);
        }

        //Multiple coffees at morning
        [TestMethod]
        public void MultipleCoffeeAtMorning()
        {
            var dish = new Dish
            {
                Type = DishType.Drink
            };
            const Period period = Period.Morning;
            const bool expectedBehavior = true;

            var isAllowed = Program.MultipleAllowed(dish, period);

            Assert.AreEqual(isAllowed, expectedBehavior);
        }

        //Multiple potatoes at night
        [TestMethod]
        public void MultiplePotatoesAtNight()
        {
            var dish = new Dish
            {
                Type = DishType.Side
            };
            const Period period = Period.Night;
            const bool expectedBehavior = true;

            var isAllowed = Program.MultipleAllowed(dish, period);

            Assert.AreEqual(isAllowed, expectedBehavior);
        }
    }
}
