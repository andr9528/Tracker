using CommunityToolkit.WinUI.UI.Controls;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntityDataGrid<TItem, TColumn>
{
    internal sealed class EntityDataGridUi(EntityDataGridLogic logic, EntityDataGridViewModel viewModel)
        : BaseUi<EntityDataGridLogic, EntityDataGridViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.DefineRows(new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            ViewModel.DataGrid = DataGridFactory.Create<TColumn>(ViewModel.Arguments.Items,
                ViewModel.Arguments.GetBindingPath, ViewModel.Arguments.GetColumnConverter,
                ViewModel.Arguments.IncludeColumn);

            ViewModel.DataGrid.Sorting += Logic.SortColumnClicked;
            grid.Children.Add(ViewModel.DataGrid);
        }
    }
}
