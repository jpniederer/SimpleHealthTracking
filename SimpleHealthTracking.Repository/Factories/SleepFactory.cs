namespace SimpleHealthTracking.Repository.Factories
{
    public class SleepFactory
    {
        public SleepFactory() { }

        public DTO.Sleep CreateSleep(Entities.Sleep sleep)
        {
            return new DTO.Sleep()
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

        public Entities.Sleep CreateSleep(DTO.Sleep sleep)
        {
            return new Entities.Sleep()
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