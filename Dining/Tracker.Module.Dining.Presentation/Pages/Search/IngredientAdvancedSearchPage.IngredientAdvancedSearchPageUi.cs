using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;
using Tracker.Shared.Frontend.Pieces;

namespace Tracker.Module.Dining.Presentation.Pages.Search;

internal sealed partial class IngredientAdvancedSearchPage
{
    internal sealed class IngredientAdvancedSearchPageUi(
        IngredientAdvancedSearchPageLogic logic,
        IngredientAdvancedSearchPageViewModel viewModel)
        : BaseUi<IngredientAdvancedSearchPageLogic, IngredientAdvancedSearchPageViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.Padding = new Thickness(16);
            grid.RowSpacing = 12;
            grid.ColumnSpacing = 8;

            grid.DefineRows(GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto,
                GridLength.Auto, new GridLength(1, GridUnitType.Star), GridLength.Auto);
            grid.DefineColumns(new GridLength(1, GridUnitType.Star), new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeader().SetRow(0).SetColumn(0, 2));
            grid.Children.Add(CreateFuzzySearchGrid().SetRow(1).SetColumn(0, 2));
            grid.Children.Add(CreateInStockOptionBar().SetRow(2).SetColumn(0, 2));
            grid.Children.Add(CreateMinimumDishCountTextBox().SetRow(3).SetColumn(0));
            grid.Children.Add(CreateMaximumDishCountTextBox().SetRow(3).SetColumn(1));
            grid.Children.Add(CreateValidationTextBlock().SetRow(4).SetColumn(0, 2));
            grid.Children.Add(CreateActionButtonGrid().SetRow(6).SetColumn(0, 2));
        }

        private TextBlock CreateHeader()
        {
            return TextBlockFactory.CreateHeader("Advanced Ingredient Search");
        }

        private Grid CreateFuzzySearchGrid()
        {
            return SimplePieceFactory.CreateFuzzySearchGrid(
                nameof(IngredientAdvancedSearchPageViewModel.UseFuzzySearch),
                nameof(IngredientAdvancedSearchPageViewModel.SearchModeText), out var _);
        }

        private NullableBooleanOptionBar CreateInStockOptionBar()
        {
            NullableBooleanOptionBar.NullableBooleanOptionBarArguments arguments = ViewModel.Arguments.ArgumentsFactory
                .CreateNullableBooleanOptionBarArguments("In Stock", ViewModel.SelectedInStock);

            var optionBar = new NullableBooleanOptionBar(arguments);

            ViewModel.ConnectInStockOptionBar(optionBar);

            return optionBar;
        }

        private TextBox CreateMinimumDishCountTextBox()
        {
            TextBox textBox = TextBoxFactory.CreateSearchBox("Minimum dish count", "No minimum",
                nameof(IngredientAdvancedSearchPageViewModel.MinimumDishCountText));

            textBox.InputScope = CreateNumberInputScope();

            return textBox;
        }

        private TextBox CreateMaximumDishCountTextBox()
        {
            TextBox textBox = TextBoxFactory.CreateSearchBox("Maximum dish count", "No maximum",
                nameof(IngredientAdvancedSearchPageViewModel.MaximumDishCountText));

            textBox.InputScope = CreateNumberInputScope();

            return textBox;
        }

        private InputScope CreateNumberInputScope()
        {
            return new InputScope
            {
                Names =
                {
                    new InputScopeName(InputScopeNameValue.Number),
                },
            };
        }

        private TextBlock CreateValidationTextBlock()
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.TextWrapping = TextWrapping.Wrap;
            textBlock.Margin = new Thickness(4);

            textBlock.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(IngredientAdvancedSearchPageViewModel.ValidationMessage)),
                Mode = BindingMode.OneWay,
            });

            return textBlock;
        }

        private Grid CreateActionButtonGrid()
        {
            return SimplePieceFactory.CreateThreeButtonGrid("Reset", Logic.ResetClicked, "Cancel", Logic.CancelClicked,
                "Apply", Logic.ApplyClicked);
        }
    }
}
