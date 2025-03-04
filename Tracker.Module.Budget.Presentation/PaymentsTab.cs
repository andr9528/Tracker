using Tracker.Module.Budget.Presentation.ViewModel;

namespace Tracker.Module.Budget.Presentation;

public sealed partial class PaymentsTab : UserControl
{
    public PaymentsTab()
    {
        var ui = new PaymentsTabUi();

        this.DataContext<PaymentsTabViewModel>((userControl, vm) =>
            userControl.TabNavigation(KeyboardNavigationMode.Cycle).Background(Theme.Brushes.Background.Default)
                .Content(ui.BuildContent(vm)));
    }

    private class PaymentsTabUi
    {
        internal Grid BuildContent(PaymentsTabViewModel viewModel)
        {
            var grid = new Grid();

            grid.SafeArea(SafeArea.InsetMask.VisibleBounds);
            grid.RowDefinitions(GridLength.Auto, GridLength.FromString("*"));

            var dummyContent = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Dummy Payment Tab Text",
            };

            grid.Add(dummyContent);

            return grid;
        }
    }
}
