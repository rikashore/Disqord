﻿using Disqord.Models;
using Microsoft.Extensions.Logging;

namespace Disqord.AuditLogs
{
    public class TransientWebhookAuditLogChanges : IWebhookAuditLogChanges
    {
        public AuditLogChange<string> Name { get; }

        public AuditLogChange<WebhookType> Type { get; }

        public AuditLogChange<string> AvatarHash { get; }

        public AuditLogChange<Snowflake> ChannelId { get; }

        public AuditLogChange<Snowflake?> ApplicationId { get; }

        public TransientWebhookAuditLogChanges(IClient client, AuditLogEntryJsonModel model)
        {
            for (var i = 0; i < model.Changes.Value.Length; i++)
            {
                var change = model.Changes.Value[i];
                switch (change.Key)
                {
                    case "name":
                    {
                        Name = AuditLogChange<string>.Convert(change);
                        break;
                    }
                    case "type":
                    {
                        Type = AuditLogChange<WebhookType>.Convert(change);
                        break;
                    }
                    case "avatar_hash":
                    {
                        AvatarHash = AuditLogChange<string>.Convert(change);
                        break;
                    }
                    case "channel_id":
                    {
                        ChannelId = AuditLogChange<Snowflake>.Convert(change);
                        break;
                    }
                    case "application_id":
                    {
                        ApplicationId = AuditLogChange<Snowflake?>.Convert(change);
                        break;
                    }
                    default:
                    {
                        client.Logger.LogDebug("Unknown key {0} for {1}", change.Key, this);
                        break;
                    }
                }
            }
        }
    }
}
