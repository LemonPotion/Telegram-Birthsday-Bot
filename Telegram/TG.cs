namespace WebApplication3
{
    using Telegram.Bot;
    using System;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.ReplyMarkups;
    using Microsoft.EntityFrameworkCore;
    using WebApplication3.Controllers;
    using WebApplication3.DataBase;
    using Newtonsoft.Json;
    using Telegram.Bot.Types.Enums;
    using System.Xml.Linq;
    using System.IO;

    public class TG
    {
        static string chatID;
        public  void start()
        {
            var client = new TelegramBotClient("6798780761:AAHUgrxdDM9VRstUOdoCip4aCfnvHyTUYXY");//токен бота
            client.StartReceiving(Update, Error);//Начать получать сообщения от пользователей
        }
        public async  Task SendMessage(ITelegramBotClient client , string name)//метод для отправки 
        {
            var projectDirectory = Directory.GetCurrentDirectory();//получаем путь проекта
            try
            {
                var photoStream = System.IO.File.Open(projectDirectory + "\\Images\\kitty cat.png", FileMode.Open);//открываем путь к картинке
                var fileToSend = new InputFileStream(photoStream);//обьект хранящий картинку
                await client.SendTextMessageAsync(chatID, $"С днем рождения {name}!");//сообщение о поздравлении
                await client.SendPhotoAsync(chatID, fileToSend, caption: $"Удачи, счастья и здоровья!");//пожелания с картинкой
                photoStream.Close();//закрываем
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);//выводим в консоль ошибки
            }

        }
        public async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Message.Chat.Type == ChatType.Supergroup || update.Message.Chat.Type == ChatType.Group)
            {
                var chatid = update.Message.Chat.Id;

                chatID = chatid.ToString();

                Console.WriteLine($"{chatid}");
            }
        }
        private  Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
}
