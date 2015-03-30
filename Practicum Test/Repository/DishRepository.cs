using PracticumTest.Domain;

namespace PracticumTest.Repository
{
    public class DishRepository
    {
        private readonly Period _period;

        public DishRepository(Period period)
        {
            _period = period;
        }

        public bool IsMultipleAllowed(Dish dish)
        {
            var isAllowed = false;
            switch (_period)
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

        public DishType? GetDishType(string option, ref bool error)
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
                    if (_period == Period.Night)
                        type = DishType.Dessert;
                    break;
            }
            if (type == null)
                error = true;

            return type;
        }
        
    }
}
