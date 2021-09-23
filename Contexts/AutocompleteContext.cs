using System.Collections.Generic;
using DSharpPlus.Entities;

namespace DSharpPlus.SlashCommands
{
    /// <summary>
    /// Represents a context for an autocomplete interaction.
    /// </summary>
    public class AutocompleteContext
    {
        /// <summary>
        /// The interaction created.
        /// </summary>
        public DiscordInteraction Interaction { get; internal set; }
        
        /// <summary>
        /// The options already provided.
        /// </summary>
        public IReadOnlyList<DiscordInteractionDataOption> Options { get; internal set; }
        
        /// <summary>
        /// The option to auto-fill for.
        /// </summary>
        public DiscordInteractionDataOption FocusedOption { get; internal set; }

        /// <summary>
        /// The given value of the focused option.
        /// </summary>
        public string OptionValue
            => this.FocusedOption.Value as string;
    }
}