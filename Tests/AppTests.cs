using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PracticumTest;
using PracticumTest.Domain;
using PracticumTest.Repository;
using PracticumTest.Seed;

namespace Tests
{
    [TestClass]
    public class AppTests
    {
        [TestMethod]
        public void CheckReturnedOrderEntreeSideDrinkDessert()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 2, 1, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "eggs, toast, coffee");

            input = "night, 2, 4, 3, 2, 1, 2";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "steak, potato (x3), wine, cake");
        }

        [TestMethod]
        public void InputPeriodAsMorningAndNightOnly()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 1, 2, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "eggs, toast, coffee");

            input = "sthingelse, 1, 2, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "error");

            input = "night, 1, 2, 3, 4";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "steak, potato, wine, cake");

            input = "foobar, 1, 2, 3, 4";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "error");
        }

        [TestMethod]
        public void AtLeastOneSeletionOfEachDish()
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

        [TestMethod]
        public void PrintErrorOnlyAtFinal()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 3, 3, 2, 5, 1, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "eggs, toast, coffee (x3), error");

            input = "night, 1, 2, 7, 4, 3";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "steak, potato, wine, cake, error");
        }

        [TestMethod]
        public void DessertsAreNotAllowedAtMorning()
        {
            const string option = "4";
            const Period period = Period.Morning;
            const bool expectedBehavior = true;

            var error = false;
            var type = new DishRepository(period).GetDishType(option, ref error);

            Assert.AreEqual(error, expectedBehavior);
            Assert.AreEqual(type, null);
        }

        //Input is not case sensitive
        [TestMethod]
        public void InputIsNotCaseSensitive()
        {
            const string input1 = "morning, 1, 2, 3, 4";

            var return1 = Program.ProcessData(input1);

            const string input2 = "Morning, 1, 2, 3, 4";

            var return2 = Program.ProcessData(input2);
            Assert.AreEqual(return1, return2);
        }

        [TestMethod]
        public void MultipleCoffeeAreAllowedAtMorning()
        {
            const Period period = Period.Morning;

            var lstDishes = Seed.FeedData();
            var multipleAllowed = lstDishes.FirstOrDefault(d => d.Period == period && d.MultipleAllowed);

            Debug.Assert(multipleAllowed != null, "multipleAllowed != null");
            Assert.AreEqual(multipleAllowed.Description, "coffee");
        }

        [TestMethod]
        public void MultiplePotatoesAreAllowedAtNight()
        {
            const Period period = Period.Night;

            var lstDishes = Seed.FeedData();
            var multipleAllowed = lstDishes.FirstOrDefault(d => d.Period == period && d.MultipleAllowed);

            Debug.Assert(multipleAllowed != null, "multipleAllowed != null");
            Assert.AreEqual(multipleAllowed.Description, "potato");
        }

        [TestMethod]
        public void OnlyOneSelectionPerDishType()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 1, 2, 3, 1";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "eggs, toast, coffee, error");

            input = "night, 3, 4, 1, 3, 2";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "steak, potato, wine, cake, error");
        }

        [TestMethod]
        public void AtLeastOneSelectionPerDishType()
        {
            Program.FeedData();
            string input, output;
            input = "morning, 3, 3, 2";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "toast, coffee (x2), error");

            input = "night, 2, 1, 3, 2";
            output = Program.ProcessData(input);

            Assert.AreEqual(output, "steak, potato (x2), wine, error");
        }
    }
}
