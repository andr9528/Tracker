using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishesGrid
{
    internal sealed class DishesGridUi(DishesGridLogic logic, DishesGridViewModel viewModel)
        : BaseUi<DishesGridLogic, DishesGridViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.RowSpacing = 8;
            grid.ColumnSpacing = 8;

            grid.DefineRows(new GridLength(1, GridUnitType.Star), GridLength.Auto);

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto);
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateDishDataGrid().SetRow(0).SetColumn(0, 2));

            grid.Children.Add(CreateNameSearchTextBox().SetRow(1).SetColumn(0));
        }

        private DataGrid CreateDishDataGrid()
        {
            ViewModel.DataGrid = DataGridFactory.Create<DishGridColumns>(ViewModel.DishItems, GetColumnBindingPath,
                includeColumn: ShouldIncludeColumn);

            ViewModel.DataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding
            {
                Path = new PropertyPath(nameof(DishesGridViewModel.SelectedDishItem)),
                Mode = BindingMode.TwoWay,
            });

            ViewModel.DataGrid.Margin = new Thickness(4);

            return ViewModel.DataGrid;
        }

        private bool ShouldIncludeColumn(DishGridColumns column)
        {
            if (ViewModel.Arguments.DisplayMode == GridDisplayMode.NORMAL)
            {
                return true;
            }

            return column == DishGridColumns.NAME;
        }

        private TextBox CreateNameSearchTextBox()
        {
            return TextBoxFactory.CreateSearchBox("Name", "Search by dish name",
                nameof(DishesGridViewModel.NameSearchText));
        }

        private string GetColumnBindingPath(DishGridColumns column)
        {
            return column switch
            {
                DishGridColumns.NAME => nameof(DishGridItem.Name),

                DishGridColumns.DINNER_COUNT => nameof(DishGridItem.DinnerCount),

                DishGridColumns.INGREDIENTS => nameof(DishGridItem.Ingredients),

                var _ => throw new ArgumentOutOfRangeException(nameof(column), column, null),
            };
        }

        private enum DishGridColumns
        {
            NAME = 0,
            DINNER_COUNT = 1,
            INGREDIENTS = 2,
        }
    }
}
