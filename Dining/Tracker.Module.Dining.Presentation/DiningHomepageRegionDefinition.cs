using Microsoft.Extensions.DependencyInjection;
using Tracker.Module.Dining.Presentation.Pages;
using Tracker.Shared.Frontend.Abstraction;

namespace Tracker.Module.Dining.Presentation;

public sealed class DiningHomepageRegionDefinition : IPageRegion
{
    /// <inheritdoc />
    public string DisplayName => "Dining";

    /// <inheritdoc />
    public UIElement Icon => new SymbolIcon(Symbol.Home);

    /// <inheritdoc />
    public UIElement CreateControl(IServiceProvider services)
    {
        var argumentsFactory = services.GetRequiredService<DiningArgumentsFactory>();

        DiningHomepage.DiningHomepageArguments arguments = argumentsFactory.CreateDiningHomepageArguments();

        return new DiningHomepage(arguments);
    }
}
