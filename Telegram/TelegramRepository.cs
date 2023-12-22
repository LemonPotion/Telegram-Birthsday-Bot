using WebApplication3.DataBase;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using WebApplication3;
using Telegram.Bot;

namespace Tg_birthsday_bot.Telegram
{
    public class TelegramRepository
    {
        public AppDbContext AppDbContext;

        public TelegramRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        public void SendNSort()
        {
            using var bb = new AppDbContext();
            var client = new TelegramBotClient("6798780761:AAHUgrxdDM9VRstUOdoCip4aCfnvHyTUYXY");//токен бота
            DbSet<Employees> entities1 = AppDbContext.entities;
            foreach (var dataItem in entities1)
            {
                DateTime dateTime = dataItem.birth;//парсируем дату человека
                if (dateTime.Day == DateTime.Now.Day && dateTime.Month == DateTime.Now.Month)//проверяем сегодня ли его день рождения
                {
                    TG tG = new TG();
                    Console.WriteLine(dataItem.name);
                    tG.SendMessage(client,dataItem.name);
                }
            }
        }

    }
}
