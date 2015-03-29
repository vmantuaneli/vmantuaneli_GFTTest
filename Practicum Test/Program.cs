using System;
using System.Collections.Generic;
using PracticumTest.Domain;
using PracticumTest.Repository;

namespace PracticumTest
{
    public class Program
    {
        static List<Dish> _lstDishes = new List<Dish>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n\nPlease choose an option:\ne.g.: 'morning, 1, 2, 3'");
                //Get data from user input
                var input = Console.ReadLine();

                Console.WriteLine(ProcessData(input));
            }
        }

        public static void FeedData()
        {
            _lstDishes = Seed.Seed.FeedData();
        }

        public static string ProcessData(string input)
        {
            var regInput = Util.Util.RegularizeInput(input);
            var selectionRepository = new SelectionRepository(_lstDishes);
            var dishesViewModel = selectionRepository.GetSelections(regInput);

            return Util.Util.PrintInformation(dishesViewModel);
        }
    }
}
