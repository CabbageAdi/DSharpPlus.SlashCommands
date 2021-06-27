﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;

namespace DSharpPlus.SlashCommands
{
    /// <summary>
    /// Defines various extension methods for slash commands
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Enables slash commands on this <see cref="DiscordClient"/>
        /// </summary>
        /// <param name="client">Client to enable slash commands for</param>
        /// <param name="config">Configuration to use</param>
        /// <returns>Created <see cref="SlashCommandsExtension"/></returns>
        public static SlashCommandsExtension UseSlashCommands(this DiscordClient client,
            SlashCommandsConfiguration config = null)
        {
            if (client.GetExtension<SlashCommandsExtension>() != null)
                throw new InvalidOperationException("Slash commands are already enabled for that client.");

            var scomm = new SlashCommandsExtension(config);
            client.AddExtension(scomm);
            return scomm;
        }

        /// <summary>
        /// Gets the slash commands module for this client
        /// </summary>
        /// <param name="client">Client to get slash commands for</param>
        /// <returns>The module, or null if not activated</returns>
        public static SlashCommandsExtension GetSlashCommands(this DiscordClient client)
            => client.GetExtension<SlashCommandsExtension>();

        /// <summary>
        /// Enables slash commands on this <see cref="DiscordShardedClient"/>.
        /// </summary>
        /// <param name="client">Client to enable slash commands on.</param>
        /// <param name="config">Configuration to use.</param>
        /// <returns>A dictionary of created <see cref="SlashCommandsExtension"/> with the key being the shard id.</returns>
        public static async Task<IReadOnlyDictionary<int, SlashCommandsExtension>> UseSlashCommandsAsync(this DiscordShardedClient client, SlashCommandsConfiguration config = null)
        {
            var modules = new Dictionary<int, SlashCommandsExtension>();
            await (Task)client.GetType().GetMethod("InitializeShardsAsync", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(client, null);
            foreach(var shard in client.ShardClients.Values)
            {
                var scomm = shard.GetSlashCommands();
                if (scomm == null)
                    scomm = shard.UseSlashCommands(config);

                modules[shard.ShardId] = scomm;
            }

            return modules;
        }

        /// <summary>
        /// Registers a commands class.
        /// </summary>
        /// <typeparam name="T">The command class to register</typeparam>
        /// <param name="modules">The modules to register it on</param>
        /// <param name="guildId">The guild id to register it on. If you want global commands, leave it null.</param>
        public static void RegisterCommands<T>(this IReadOnlyDictionary<int, SlashCommandsExtension> modules, ulong? guildId = null) where T : SlashCommandModule
        {
            foreach (var module in modules.Values)
                module.RegisterCommands<T>(guildId);
        }

        /// <summary>
        /// Registers a command class
        /// </summary>
        /// <param name="modules">The modules to register it on</param>
        /// <param name="type">The <see cref="Type"/> of the command class to register</param>
        /// <param name="guildId">The guild id to register it on. If you want global commands, leave it null.</param>
        public static void RegisterCommands(this IReadOnlyDictionary<int, SlashCommandsExtension> modules, Type type, ulong? guildId = null)
        {
            foreach (var module in modules.Values)
                module.RegisterCommands(type, guildId);
        }
    }
}