using System.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Data;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Frontend.Converters;

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
            grid.Children.Add(CreateAdvancedSearchButtonGrid().SetRow(2).SetColumn(1));
        }

        private DataGrid CreateIngredientDataGrid()
        {
            ViewModel.DataGrid =
                DataGridFactory.Create<IngredientGridColumns>(ViewModel.Ingredients, GetColumnBindingPath,
                    GetColumnConverter);

            ViewModel.DataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding
            {
                Path = new PropertyPath(nameof(IngredientsGridViewModel.SelectedIngredient)),
                Mode = BindingMode.TwoWay,
            });

            ViewModel.DataGrid.Margin = new Thickness(4);

            return ViewModel.DataGrid;
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
                IngredientGridColumns.IN_STOCK => new Shared.Frontend.Converters.BooleanConverter(),
                var _ => null,
            };
        }

        private string GetColumnBindingPath(IngredientGridColumns column)
        {
            return column switch
            {
                IngredientGridColumns.NAME => nameof(Ingredient.Name),
                IngredientGridColumns.IN_STOCK => nameof(Ingredient.InStock),
                IngredientGridColumns.DISH_COUNT => $"{nameof(Ingredient.DishIngredients)}.Count",
                _ => throw new ArgumentOutOfRangeException(nameof(column), column, null),
            };
        }

        private enum IngredientGridColumns
        {
            NAME = 0,
            IN_STOCK = 1,
            DISH_COUNT = 2,
        }
    }
}
