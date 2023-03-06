using Telegram.Bot.Types;

namespace UtilityBot.Services
{
    public interface IActionHandler
    {
        string HandleAction(Message message);
    }
}
