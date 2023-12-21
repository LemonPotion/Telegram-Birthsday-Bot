using Quartz.Impl;
using Quartz;

namespace WebApplication3.Quartz
{
    public class Trigger
    {
        public static async Task trigger()//метод для работы с триггером , планировщиком и задачей
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();//Создаем экзмепляр планировщика

            IScheduler scheduler = await schedulerFactory.GetScheduler();//Создаем планировщик

            await scheduler.Start();//запускаем

            IJobDetail jobDetail = JobBuilder.Create<Job>().WithIdentity("BotStart","Telegram").Build();//создаем задачу описанную в классе Job

            ITrigger trigger = TriggerBuilder.Create().WithIdentity("1", "Telegram").StartNow().//триггер который будет срабатывать каждый день в 9:00 с интервалом 24 часа
                WithDailyTimeIntervalSchedule(s =>s.WithIntervalInHours(24)
                .OnEveryDay()
                .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(9, 0)))
                .Build();

            await scheduler.ScheduleJob(jobDetail,trigger);//Передаем планировщику триггер и задачу
        }
    }
}
