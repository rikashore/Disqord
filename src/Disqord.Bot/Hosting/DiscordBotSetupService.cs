﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Disqord.Hosting;
using Microsoft.Extensions.Logging;

namespace Disqord.Bot.Hosting;

/// <inheritdoc/>
[EditorBrowsable(EditorBrowsableState.Never)]
public class DiscordBotSetupService : DiscordClientSetupService
{
    /// <inheritdoc/>
    public DiscordBotSetupService(
        ILogger<DiscordBotSetupService> logger,
        DiscordClientBase client)
        : base(logger, client)
    { }

    /// <inheritdoc/>
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await base.StartAsync(cancellationToken).ConfigureAwait(false);

        var bot = (Client as DiscordBotBase)!;
        try
        {
            await bot.InitializeAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Logger.LogCritical(ex, "An exception occurred while initializing the bot.");
            throw;
        }
    }
}
