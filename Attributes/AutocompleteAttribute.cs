using System;

namespace DSharpPlus.SlashCommands
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class AutocompleteAttribute : Attribute
    {
        /// <summary>
        /// The type of the provider.
        /// </summary>
        public Type ProviderType { get; }
        
        /// <summary>
        /// Adds an autocomplete provider to this command option.
        /// </summary>
        /// <param name="providerType">The type of the provider.</param>
        public AutocompleteAttribute(Type providerType)
        {
            ProviderType = providerType;
        }
    }
}