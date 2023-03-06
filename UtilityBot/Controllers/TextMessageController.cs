using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IActionHandler _actionHandler;

        public TextMessageController(ITelegramBotClient telegramBotClient, IActionHandler actionHandler)
        {
            _telegramClient = telegramBotClient;
            _actionHandler = actionHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Сложить числа" , $"sum"),
                        InlineKeyboardButton.WithCallbackData($" Сосчитать символы " , $"count")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот обработчик сообщений.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Можно написать нам сообщение и мы сосчитаем сумму чисел или отправим вам количество символов.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    string result = _actionHandler.HandleAction(message);
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
                    break;
            }
        }
    }
}
