using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PracticumTest
{
    public enum DishType
    {
        Entree = 1,
        Side,
        Drink,
        Dessert
    }

    public enum Period
    {
        Morning,
        Night
    }

    public class Dish
    {
        public Period Period { get; set; }
        public DishType Type { get; set; }
        public string Description { get; set; }
    }
}
