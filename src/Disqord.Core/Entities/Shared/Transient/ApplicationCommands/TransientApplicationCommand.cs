﻿using System.Collections.Generic;
using System.Globalization;
using Disqord.Models;
using Qommon;
using Qommon.Collections.ReadOnly;

namespace Disqord
{
    public class TransientApplicationCommand : TransientClientEntity<ApplicationCommandJsonModel>, IApplicationCommand
    {
        /// <inheritdoc/>
        public Snowflake Id => Model.Id;

        /// <inheritdoc/>
        public Snowflake? GuildId => Model.GuildId.GetValueOrNullable();

        /// <inheritdoc/>
        public ApplicationCommandType Type => Model.Type.GetValueOrDefault(ApplicationCommandType.Slash);

        /// <inheritdoc/>
        public Snowflake ApplicationId => Model.ApplicationId;

        /// <inheritdoc/>
        public string Name => Model.Name;

        /// <inheritdoc />
        public IReadOnlyDictionary<CultureInfo, string> NameLocalizations
        {
            get
            {
                if (!Model.NameLocalizations.HasValue)
                    return ReadOnlyDictionary<CultureInfo, string>.Empty;

                return Model.NameLocalizations.Value.ToReadOnlyDictionary(x => CultureInfo.GetCultureInfo(x.Key), x => x.Value);
            }
        }

        /// <inheritdoc />
        public Permission? DefaultRequiredMemberPermissions => (Permission?) Model.DefaultMemberPermissions;

        /// <inheritdoc />
        public bool IsEnabledInPrivateChannels => Model.DmPermission.GetValueOrDefault(true);

        /// <inheritdoc/>
        public bool IsEnabledByDefault => Model.DefaultPermission.GetValueOrDefault() ?? true;

        /// <inheritdoc/>
        public Snowflake Version => Model.Version;

        public TransientApplicationCommand(IClient client, ApplicationCommandJsonModel model)
            : base(client, model)
        { }

        public static IApplicationCommand Create(IClient client, ApplicationCommandJsonModel model)
            => model.Type.HasValue
                ? model.Type.Value switch
                {
                    ApplicationCommandType.Slash => new TransientSlashCommand(client, model),
                    ApplicationCommandType.User => new TransientUserContextMenuCommand(client, model),
                    ApplicationCommandType.Message => new TransientMessageContextMenuCommand(client, model),
                    _ => new TransientApplicationCommand(client, model)
                }
                : new TransientSlashCommand(client, model); // Have to assume that this is a slash command since the type for it is often not provided
    }
}
