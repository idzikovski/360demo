﻿using System.Threading.Tasks;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Settings;
using Task = System.Threading.Tasks.Task;

namespace SmartHotel.Clients.Maintenance.Services.Settings
{
    public class SettingsService : BaseSettingsLoader<RemoteSettings>, ISettingsService<RemoteSettings>
    {
        public string RemoteFileUrl { get => AppSettings.SettingsFileUrl; set => AppSettings.SettingsFileUrl = value; }

        public Task<RemoteSettings> LoadSettingsAsync()
        {
            var settings = new RemoteSettings();
            settings.Urls.Tasks = AppSettings.TasksEndpoint;

            return Task.FromResult(settings);
        }

        public Task PersistRemoteSettingsAsync(RemoteSettings remote)
        {
            AppSettings.TasksEndpoint = remote.Urls.Tasks;

            return Task.FromResult(false);
        }
    }
}
