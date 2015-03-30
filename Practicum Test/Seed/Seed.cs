using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticumTest.Domain;

namespace PracticumTest.Seed
{
    public static class Seed
    {
        public static List<Dish> FeedData()
        {
            return new List<Dish>{
                new Dish
                {
                    Type = DishType.Entree,
                    Period = Period.Morning,
                    Description = "eggs",
                    MultipleAllowed = false
                },
                new Dish
                {
                    Type = DishType.Side,
                    Period = Period.Morning,
                    Description = "toast",
                    MultipleAllowed = false
                },
                new Dish
                {
                    Type = DishType.Drink,
                    Period = Period.Morning,
                    Description = "coffee",
                    MultipleAllowed = true
                },
                new Dish
                {
                    Type = DishType.Entree,
                    Period = Period.Night,
                    Description = "steak",
                    MultipleAllowed = false
                },
                new Dish
                {
                    Type = DishType.Side,
                    Period = Period.Night,
                    Description = "potato",
                    MultipleAllowed = true
                },
                new Dish
                {
                    Type = DishType.Drink,
                    Period = Period.Night,
                    Description = "wine",
                    MultipleAllowed = false
                },
                new Dish
                {
                    Type = DishType.Dessert,
                    Period = Period.Night,
                    Description = "cake",
                    MultipleAllowed = false
                }
            };
        }
    }
}
