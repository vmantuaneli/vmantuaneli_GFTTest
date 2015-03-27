using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticumTest
{
    public class Program
    {
        static List<Dish> _lstDishes = new List<Dish>();

        public static void FeedData()
        {
            _lstDishes = new List<Dish>{
                new Dish
                {
                    Type = DishType.Entree,
                    Period = Period.Morning,
                    Description = "eggs"
                },
                new Dish
                {
                    Type = DishType.Side,
                    Period = Period.Morning,
                    Description = "toast"
                },
                new Dish
                {
                    Type = DishType.Drink,
                    Period = Period.Morning,
                    Description = "coffee"
                },
                new Dish
                {
                    Type = DishType.Entree,
                    Period = Period.Night,
                    Description = "steak"
                },
                new Dish
                {
                    Type = DishType.Side,
                    Period = Period.Night,
                    Description = "potato"
                },
                new Dish
                {
                    Type = DishType.Drink,
                    Period = Period.Night,
                    Description = "wine"
                },
                new Dish
                {
                    Type = DishType.Dessert,
                    Period = Period.Night,
                    Description = "cake"
                }
            };
        }

        static void Main(string[] args)
        {
            FeedData();
            while (true)
            {
                Console.WriteLine("\n\nPlease choose an option:\ne.g.: 'morning, 1, 2, 3'");
                //Get data from user input
                var input = Console.ReadLine();

                Console.WriteLine(ProcessData(input));
            }
        }

        public static string ProcessData(string input)
        {
            //Remove spaces and normalize uppercase
            var arrOptions = input.Replace(" ", "").ToLower().Split(',').ToList();

            var error = false;
            var selectedPeriod = GetPeriod(arrOptions, ref error);
            var lstDishes = new Dictionary<Dish, int>();

            //If we don't have errors for period selection, try to get the dishes
            if (!error)
                lstDishes = ValidateDishes(arrOptions, selectedPeriod.Value, ref error);

            return PrintInformation(lstDishes, selectedPeriod.GetValueOrDefault(), error);
        }

        public static string PrintInformation(Dictionary<Dish, int> lstDishes, Period period, bool printError)
        {
            var output = "";
            foreach (var dish in lstDishes)
            {
                if (dish.Value == 1)
                    output += ", " + dish.Key.Description;
                else if (dish.Value > 1 && MultipleAllowed(dish.Key, period))
                {
                    output += ", " + dish.Key.Description + "(x" + dish.Value + ")";
                }
                else
                    printError = true;
            }

            //If an error occured, print "error" at final
            output += (printError ? ", error" : "");

            //Remove the first space and comma
            output = output.Remove(0, 2);

            return output;
        }

        public static Dictionary<Dish, int> ValidateDishes(List<string> lstOptions, Period selectedPeriod, ref bool error)
        {
            //remove first position (period)
            lstOptions.RemoveRange(0, 1);
            var lstSelections = new List<Selection>();
            foreach (var option in lstOptions)
            {
                var dishType = GetType(option, selectedPeriod, ref error);
                lstSelections.Add(new Selection
                {
                    Type = dishType,
                    Error = error
                });
            }
            
            var lstDish = new Dictionary<Dish, int>();

            if (lstSelections.Count > 0)
                foreach (var dish in _lstDishes.FindAll(d => d.Period == selectedPeriod))
                    lstDish.Add(dish, lstSelections.Count(sel => sel.Type == dish.Type));
            else
                error = true;

            //In case there's a problem with any selection
            if (lstSelections.Any(sel => sel.Error))
                error = true;

            return lstDish;
        }

        public static bool MultipleAllowed(Dish dish, Period selectedPeriod)
        {
            var isAllowed = false;
            switch (selectedPeriod)
            {
                //If morning, only multiple cups of coffee are allowed
                case Period.Morning:
                    if (dish.Type == DishType.Drink)
                        isAllowed = true;
                    break;

                //If night, only multiple potatoes are allowed
                case Period.Night:
                    if (dish.Type == DishType.Side)
                        isAllowed = true;
                    break;
            }
            return isAllowed;
        }

        public static DishType? GetType(string option, Period period, ref bool error)
        {
            //Try to parse entry to a valid selection
            DishType? type = null;
            switch (option)
            {
                case "1":
                    type = DishType.Entree;
                    break;
                case "2":
                    type = DishType.Side;
                    break;
                case "3":
                    type = DishType.Drink;
                    break;
                case "4":
                    //Desserts allowed only at night
                    if(period == Period.Night)
                        type = DishType.Dessert;
                    break;
            }
            error = type == null;

            return type;
        }

        public static Period? GetPeriod(List<string> arrOptions, ref bool error)
        {
            //Try to parse a valid period
            Period? period = null;
            switch (arrOptions[0])
            {
                case "morning":
                    period = Period.Morning;
                    break;
                case "night":
                    period = Period.Night;
                    break;
            }

            error = period == null;
            
            return period;
        }
    }
}
