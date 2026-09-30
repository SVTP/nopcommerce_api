using Nop.Core;
using Nop.Plugin.Api.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Api.Services;

/// <summary>
/// Represents the API plugin admin menu event consumer
/// </summary>
public class EventConsumer : BaseAdminMenuCreatedEventConsumer
{
    private readonly ILocalizationService _localizationService;
    private readonly IWorkContext _workContext;

    public EventConsumer(
        IPluginManager<IPlugin> pluginManager,
        ILocalizationService localizationService,
        IWorkContext workContext) : base(pluginManager)
    {
        _localizationService = localizationService;
        _workContext = workContext;
    }

    protected override string PluginSystemName => "Nop.Plugin.Api";

    protected override async Task<AdminMenuItem> GetAdminMenuItemAsync(IPlugin plugin)
    {
        var workingLanguage = await _workContext.GetWorkingLanguageAsync();

        var pluginMenuName = await _localizationService.GetResourceAsync("Plugins.Api.Admin.Menu.Title", workingLanguage.Id, defaultValue: "API");
        var settingsMenuName = await _localizationService.GetResourceAsync("Plugins.Api.Admin.Menu.Settings.Title", workingLanguage.Id, defaultValue: "API");

        var pluginMainMenu = new AdminMenuItem
        {
            Title = pluginMenuName,
            Visible = true,
            SystemName = "Api-Main-Menu",
            IconClass = "fa-genderless"
        };

        pluginMainMenu.ChildNodes.Add(new AdminMenuItem
        {
            Title = settingsMenuName,
            Url = plugin.GetConfigurationPageUrl(),
            Visible = true,
            SystemName = "Api-Settings-Menu",
            IconClass = "fa-genderless"
        });

        return pluginMainMenu;
    }
}
