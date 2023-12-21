using Quartz;
using Telegram.Bot;

namespace WebApplication3.Quartz
{
    public class Job : IJob
    {
        public Task Execute(IJobExecutionContext context)//метод IJob
        {
            var client = new TelegramBotClient("6798780761:AAHUgrxdDM9VRstUOdoCip4aCfnvHyTUYXY");//токен бота
            TG tg = new TG();
            tg.SendMessage(client);//отправляем поздравления
            return Task.CompletedTask;//возвращаем завершение задачи
        }
    }
}
