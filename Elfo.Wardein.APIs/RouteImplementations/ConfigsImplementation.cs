﻿using Elfo.Wardein.APIs.Abstractions;
using Elfo.Wardein.Core;
using Elfo.Wardein.Core.Helpers;
using Microsoft.AspNetCore.Http;
using PeterKottas.DotNetCore.WindowsService.Base;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Elfo.Wardein.APIs
{
    public class ConfigsImplementation : IAmRouteImplementation
    {
        public ConfigsImplementation() : base(blockActionIfInMaintenanceMode: false) { }

        public Task Invalidate(HttpContext context)
        {
            ServicesContainer.MailConfigurationManager(Const.MAIL_CONFIG_PATH).InvalidateCache();
            ServicesContainer.WardeinConfigurationManager(Const.WARDEIN_CONFIG_PATH).InvalidateCache();
            new PollingImplementation().Restart(context);
            return context.Response.WriteAsync($"Cached configs invalidated");
        }
    }
}
