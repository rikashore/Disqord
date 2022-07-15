﻿using System.Globalization;

namespace Disqord.Bot.Commands.Components;

internal class DiscordComponentCommandContext : DiscordCommandContext<ComponentCommand>, IDiscordComponentCommandContext
{
    public override CultureInfo Locale => Interaction.Locale ?? CultureInfo.InvariantCulture;

    public override CultureInfo GuildLocale => Interaction.GuildLocale ?? CultureInfo.InvariantCulture;

    public IUserInteraction Interaction { get; }

    public override IUser Author => Interaction.Author;

    public override Snowflake ChannelId => Interaction.ChannelId;

    public DiscordComponentCommandContext(
        DiscordBotBase bot,
        IUserInteraction interaction)
        : base(bot)
    {
        Interaction = interaction;
    }

    protected override Snowflake? GetGuildId()
        => Interaction.GuildId;
}
