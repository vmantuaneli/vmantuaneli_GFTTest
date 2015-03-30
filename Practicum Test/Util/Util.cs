using PracticumTest.Domain;

namespace PracticumTest.Util
{
    static class Util
    {
        public static string RegularizeInput(string input)
        {
            return input.Replace(" ", "").ToLower();
        }

        public static string PrintInformation(DishesViewModel dishesViewModel)
        {
            var output = "";

            if (dishesViewModel.LstSelectedDishes != null && dishesViewModel.LstSelectedDishes.Count > 0)
            {
                foreach (var selectedDish in dishesViewModel.LstSelectedDishes)
                {
                    var counter = (selectedDish.Quantity > 1 ? (" (x" + selectedDish.Quantity + ")") : "");
                    if(selectedDish.Quantity > 0)
                        output += ", " + selectedDish.Dish.Description + counter;
                }
            }
            //If there's an error, print error at final
            if (dishesViewModel.Error)
                output += ", error";
            
            //Remove the first space and comma
            output = output.Remove(0, 2);

            return output;
        }
    }
}
