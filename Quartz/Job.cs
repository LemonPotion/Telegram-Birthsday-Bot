using Quartz;
using Telegram.Bot;
using Tg_birthsday_bot.Telegram;
using WebApplication3.DataBase;

namespace WebApplication3.Quartz
{
    public class Job : IJob
    {

        //Здесь обратиться к бд и передать параметр (люди)
        //если true то отправить
        public Task Execute(IJobExecutionContext context)//метод IJob
        {

            try
            {
                var client = new TelegramBotClient("6798780761:AAHUgrxdDM9VRstUOdoCip4aCfnvHyTUYXY");//токен бота
                TelegramRepository repository = new TelegramRepository(new AppDbContext());
                repository.SendNSort();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            return Task.CompletedTask;//возвращаем завершение задачи
        }
    }
}
