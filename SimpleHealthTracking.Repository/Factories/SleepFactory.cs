namespace SimpleHealthTracking.Repository.Factories
{
    public class SleepFactory
    {
        public SleepFactory() { }

        public DTO.SleepDto CreateSleep(Entities.Sleep sleep)
        {
            return new DTO.SleepDto()
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

        public Entities.Sleep CreateSleep(DTO.SleepDto sleep)
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