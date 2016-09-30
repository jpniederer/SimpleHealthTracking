namespace SimpleHealthTracking.Repository.Factories
{
    using DTO;
    using Entities;

    public class SleepFactory
    {
        public SleepFactory() { }

        public SleepDto CreateSleep(Sleep sleep)
        {
            return new SleepDto()
            {
                Id = sleep.Id,
                UserId = sleep.UserId,
                StartTime = sleep.StartTime,
                EndTime = sleep.EndTime,
                SleepQuality = sleep.SleepQuality,
                MinutesSlept = sleep.MinutesSlept,
                TimeAdded = sleep.TimeAdded,
                UpdateTime = sleep.UpdateTime
            };
        }

        public Sleep CreateSleep(SleepDto sleep)
        {
            return new Sleep()
            {
                Id = sleep.Id,
                UserId = sleep.UserId,
                StartTime = sleep.StartTime,
                EndTime = sleep.EndTime,
                SleepQuality = sleep.SleepQuality,
                MinutesSlept = sleep.MinutesSlept,
                TimeAdded = sleep.TimeAdded,
                UpdateTime = sleep.UpdateTime
            };
        }
    }
}