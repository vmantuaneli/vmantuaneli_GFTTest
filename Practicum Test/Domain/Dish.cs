namespace PracticumTest.Domain
{
    public class Dish
    {
        public Period Period { get; set; }
        public DishType Type { get; set; }
        public string Description { get; set; }
        public bool MultipleAllowed { get; set; }
    }
}
