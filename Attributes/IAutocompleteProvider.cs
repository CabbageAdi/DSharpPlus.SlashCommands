using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.SlashCommands
{
    public interface IAutocompleteProvider
    {
        Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext context);
    }
}