using System.Collections.Generic;
using System.Linq;
using PracticumTest.Domain;

namespace PracticumTest.Repository
{
    public class SelectionRepository
    {
        private readonly List<Dish> _lstDishes;
        private DishRepository _dishRepository;


        public SelectionRepository(List<Dish> lstDishes)
        {
            _lstDishes = lstDishes;
        }

        public DishesViewModel GetSelections(string input)
        {
            //Remove spaces and normalize uppercase
            var arrOptions = input.Replace(" ", "").ToLower().Split(',').ToList();
            //Value expected to be the period definition
            var inputPeriod = arrOptions[0];

            var periodRepository = new PeriodRepository(inputPeriod);

            var dishesViewModel = new DishesViewModel();
            
            arrOptions.RemoveAt(0);

            if (!periodRepository.Error && periodRepository.Period.HasValue && arrOptions.Count > 0)
            {
                _dishRepository = new DishRepository(periodRepository.Period.Value);

                var lstSelection = arrOptions.Select(option => GetSelection(option, periodRepository.Period.Value)).ToList();

                dishesViewModel = GetSelectedDishes(lstSelection);
            }
            //If invalid period encountered or no dish options selected
            else
                dishesViewModel.Error = true;

            return dishesViewModel;
        }

        public Selection GetSelection(string input, Period period)
        {
            var error = false;
            var dishType = _dishRepository.GetDishType(input, ref error);

            var selection = new Selection
            {
                DishType = dishType,
                Period = period,
                Error = error
            };

            return selection;
        }

        public DishesViewModel GetSelectedDishes(List<Selection> lstSelections)
        {
            var dishesViewModel = new DishesViewModel();
            dishesViewModel.LstSelectedDishes = new List<SelectedDish>();

            if (lstSelections.Count > 0)
            {
                var lstDishes = _lstDishes.FindAll(d => d.Period == lstSelections.First().Period);

                foreach (var dish in lstDishes)
                {
                    var curQuantity = lstSelections.Count(sel => sel.DishType == dish.Type);

                    if (curQuantity > 1 && !dish.MultipleAllowed)
                    {
                        dishesViewModel.Error = true;
                        curQuantity = 1;
                    }

                    dishesViewModel.LstSelectedDishes.Add(new SelectedDish
                    {
                        Dish = dish,
                        Quantity = curQuantity
                    });
                }
            }
            
            //In case there's a problem with any selection
            if(lstSelections.Any(sel => sel.Error))
                dishesViewModel.Error = true;

            //If any dish from the period were not selected
            else if (dishesViewModel.LstSelectedDishes.Any(sel => sel.Quantity == 0))
                dishesViewModel.Error = true;

            return dishesViewModel;
        }
    }
}
