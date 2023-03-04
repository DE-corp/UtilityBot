using UtilityBot.Models;

namespace UtilityBot.Services
{
    interface IStorage
    {
        Session GetSession(long chatId);
    }
}
