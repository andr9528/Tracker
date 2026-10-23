using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Frontend.Converters;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Frontend.Pieces;
using BooleanConverter = Tracker.Shared.Frontend.Converters.BooleanConverter;

namespace Tracker.Module.Dining.Presentation.Pieces.Ingredients;

internal sealed partial class IngredientsGrid
{
    internal sealed class IngredientsGridUi(IngredientsGridLogic logic, IngredientsGridViewModel viewModel)
        : BaseUi<IngredientsGridLogic, IngredientsGridViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.RowSpacing = 8;
            grid.ColumnSpacing = 8;

            grid.DefineRows(new GridLength(1, GridUnitType.Star));
            grid.DefineRows(GridLength.Auto);

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto);
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateIngredientDataGrid().SetRow(0).SetColumn(0, 2));
            grid.Children.Add(CreateNameSearchTextBox().SetRow(1).SetColumn(0));
            grid.Children.Add(CreateAdvancedSearchButtonGrid().SetRow(1).SetColumn(1));
        }

        private DataGrid CreateIngredientDataGrid()
        {
            EntityDataGrid<IngredientGridItem, IngredientGridColumns>.EntityDataGridArguments arguments =
                ViewModel.Arguments.ArgumentsFactory
                    .CreateEntityDataGridArguments<IngredientGridItem, IngredientGridColumns>(ViewModel.IngredientItems,
                        GetColumnBindingPath, Logic.GetItems, GetColumnConverter, ShouldIncludeColumn);

            ViewModel.EntityDataGrid = new EntityDataGrid<IngredientGridItem, IngredientGridColumns>(arguments);

            ViewModel.DataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding
            {
                Path = new PropertyPath(nameof(IngredientsGridViewModel.SelectedIngredientItem)),
                Mode = BindingMode.TwoWay,
            });

            ViewModel.DataGrid.Margin = new Thickness(4);

            return ViewModel.DataGrid;
        }

        private bool ShouldIncludeColumn(IngredientGridColumns column)
        {
            if (ViewModel.Arguments.DisplayMode == GridDisplayMode.NORMAL)
            {
                return true;
            }

            return column == IngredientGridColumns.NAME;
        }

        private TextBox CreateNameSearchTextBox()
        {
            return TextBoxFactory.CreateSearchBox("Name", "Search by ingredient name",
                nameof(IngredientsGridViewModel.NameSearchText));
        }

        private Grid CreateAdvancedSearchButtonGrid()
        {
            return SimplePieceFactory.CreateRightButtonGrid("Advanced Search", Logic.AdvancedSearchClicked);
        }

        private IValueConverter? GetColumnConverter(IngredientGridColumns column)
        {
            return column switch
            {
                IngredientGridColumns.IN_STOCK => new BooleanConverter(),
                IngredientGridColumns.AVERAGE_DAYS_BETWEEN_USAGE => new TwoDecimalConverter(),
                var _ => null,
            };
        }

        private string GetColumnBindingPath(IngredientGridColumns column)
        {
            return column switch
            {
                IngredientGridColumns.NAME => nameof(IngredientGridItem.Name),
                IngredientGridColumns.IN_STOCK => nameof(IngredientGridItem.InStock),
                IngredientGridColumns.DISH_COUNT => nameof(IngredientGridItem.DishCount),
                IngredientGridColumns.AVERAGE_DAYS_BETWEEN_USAGE => nameof(IngredientGridItem.AverageDaysBetweenUsage),
                var _ => throw new ArgumentOutOfRangeException(nameof(column), column, null),
            };
        }

        internal enum IngredientGridColumns
        {
            NAME = 0,
            IN_STOCK = 1,
            DISH_COUNT = 2,
            AVERAGE_DAYS_BETWEEN_USAGE = 3,
        }
    }
}
