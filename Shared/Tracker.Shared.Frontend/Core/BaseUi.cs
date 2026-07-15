using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Core;

public abstract class BaseUi<TLogic, TViewModel> where TLogic : class where TViewModel : class
{
    protected BaseUi(TLogic logic, TViewModel viewModel)
    {
        Logic = logic;
        ViewModel = viewModel;
    }

    protected TLogic Logic { get; }
    protected TViewModel ViewModel { get; }

    public Grid CreateContentGrid()
    {
        Grid grid = GridFactory.CreateDefaultGrid();

        ConfigureGrid(grid);
        AddControlsToGrid(grid);

        return grid;
    }

    protected void ConfigureDefaultPageGrid(Grid grid)
    {
        grid.HorizontalAlignment = HorizontalAlignment.Stretch;
        grid.VerticalAlignment = VerticalAlignment.Stretch;
        grid.Margin = new Thickness(0);
        grid.Padding = new Thickness(8);
        grid.RowSpacing = 8;
    }

    protected abstract void ConfigureGrid(Grid grid);

    protected abstract void AddControlsToGrid(Grid grid);
}
