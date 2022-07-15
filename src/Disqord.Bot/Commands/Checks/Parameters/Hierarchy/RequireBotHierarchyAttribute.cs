﻿using Disqord.Gateway;

namespace Disqord.Bot.Commands;

/// <summary>
///     Specifies that the <see cref="IMember"/> parameter must be of lower guild hierarchy than the bot.
/// </summary>
public class RequireBotHierarchyAttribute : RequireHierarchyBaseAttribute
{
    protected override (string Name, IMember Member) GetTarget(IDiscordGuildCommandContext context)
        => ("bot", context.Bot.GetMember(context.GuildId, context.Bot.CurrentUser.Id));
}
