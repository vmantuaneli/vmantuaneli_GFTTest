using PracticumTest.Domain;

namespace PracticumTest.Repository
{

    public class PeriodRepository
    {
        public Period? Period;
        public bool Error;

        public PeriodRepository(string input)
        {
            Period = GetPeriod(input, ref Error);
        }

        static Period? GetPeriod(string input, ref bool error)
        {
            //Try to parse a valid period
            Period? period = null;
            switch (input)
            {
                case "morning":
                    period = Domain.Period.Morning;
                    break;
                case "night":
                    period = Domain.Period.Night;
                    break;
            }

            if (period == null)
                error = true;

            return period;
        }
    }
}
