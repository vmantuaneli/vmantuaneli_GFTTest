using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticumTest.Domain
{
    public class DishesViewModel
    {
        public Period Period { get; set; }
        public List<SelectedDish> LstSelectedDishes { get; set; }
        public bool Error { get; set; }
    }
}
