using System;
using Telegram.Bot.Types;

namespace UtilityBot.Services
{
    class ActionHandler : IActionHandler
    {
        private readonly IStorage _memoryStorage;

        public ActionHandler(IStorage memoryStorage)
        {
            _memoryStorage = memoryStorage;
        }

        public string HandleAction(Message message)
        {
            string userActionCode = _memoryStorage.GetSession(message.Chat.Id).Action;
            if (userActionCode == "sum")
            {
                try
                {
                    return Sum(message.Text);
                }
                catch (FormatException)
                {
                    return "Для сложения необходимо отправлять только числа!";
                }
            }

            return CharCount(message.Text);
        }

        string Sum(string message)
        {
            int result = 0;
            var splited_string = message.Split(' ');
            foreach (var item in splited_string)
            {
                result += int.Parse(item);
            }

            return result.ToString();
        }

        string CharCount(string message)
        {
            return message.Length.ToString();
        }
    }
}
