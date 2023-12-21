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

    public class TG
    {
        static string chatID;
        public  void start()
        {
            var client = new TelegramBotClient("6798780761:AAHUgrxdDM9VRstUOdoCip4aCfnvHyTUYXY");//токен бота
            client.StartReceiving(Update, Error);//Начать получать сообщения от пользователей
        }
        public async  Task SendMessage(ITelegramBotClient client)//метод для отправки 
        {
            var projectDirectory = Directory.GetCurrentDirectory();//получаем путь проекта

           
            var jsonFilePath = Path.Combine(projectDirectory, "employees.json"); // Путь к файлу JSON в директории проекта

            try
            {

                // Чтение файла json
                var json = System.IO.File.ReadAllText(jsonFilePath);

                // Десериализуем json в список объектов
                List<Employee> dataList = JsonConvert.DeserializeObject<List<Employee>>(json);

                // перебираем список объектов
                foreach (var dataItem in dataList)
                {
                    DateTime dateTime = DateTime.Parse(dataItem.Birth);//парсируем дату человека
                    if (dateTime.Day == DateTime.Now.Day && dateTime.Month == DateTime.Now.Month)//проверяем сегодня ли его день рождения
                    {
                        var photoStream = System.IO.File.Open(projectDirectory + "\\Images\\kitty cat.png", FileMode.Open);//открываем путь к картинке
                        var fileToSend = new InputFileStream(photoStream);//обьект хранящий картинку
                        await client.SendTextMessageAsync(chatID, $"С днем рождения {dataItem.Name}!");//сообщение о поздравлении
                        await client.SendPhotoAsync(chatID, fileToSend, caption: $"Удачи, счастья и здоровья!");//пожелания с картинкой
                        photoStream.Close();//закрываем
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);//выводим в консоль ошибки
            }

        }
        public async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                var message = update.Message;//получаем сообщение

                if (message.Chat.Type == ChatType.Group || message.Chat.Type == ChatType.Supergroup || message.Chat.Type == ChatType.Channel)
                {
                    if (message != null && message.Text != null)//проверяем на null и проверяем текст , вдруг пользователь скинет картинку
                    {
                        Console.WriteLine($"{message.Chat.Username}|{message.Text}");//мониторим запросы в консоли

                        if (message.Text.Contains("/start"))//если true то отправляется сообщение
                        {
                            chatID = update.Message.Chat.Id.ToString();
                            SendMessage(client);
                        }
                        else
                        {
                            await client.SendTextMessageAsync(message.Chat.Id, $"Для начала работы введите /start");//подсказка пользователю
                        }
                    }
                }
            }
            catch(Exception ex ) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        private  Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }
    }
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Birth { get; set; }
       
    }
}
