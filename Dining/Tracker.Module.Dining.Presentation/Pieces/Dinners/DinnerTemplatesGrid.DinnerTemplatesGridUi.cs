using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Data;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Abstraction.Enums;
using Tracker.Shared.Frontend.Converters;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplatesGrid
{
    internal sealed class DinnerTemplatesGridUi(DinnerTemplatesGridLogic logic, DinnerTemplatesGridViewModel viewModel)
        : BaseUi<DinnerTemplatesGridLogic, DinnerTemplatesGridViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.RowSpacing = 8;

            grid.DefineRows(new GridLength(1, GridUnitType.Star), GridLength.Auto);
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateDinnerTemplateDataGrid().SetRow(0));
            grid.Children.Add(CreateNameSearchTextBox().SetRow(1));
        }

        private DataGrid CreateDinnerTemplateDataGrid()
        {
            ViewModel.DataGrid = DataGridFactory.Create<DinnerTemplateGridColumns>(ViewModel.DinnerTemplates,
                GetColumnBindingPath, GetColumnConverter, ShouldIncludeColumn);

            ViewModel.DataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding
            {
                Path = new PropertyPath(nameof(DinnerTemplatesGridViewModel.SelectedDinnerTemplate)),
                Mode = BindingMode.TwoWay,
            });

            ViewModel.DataGrid.Margin = new Thickness(4);

            return ViewModel.DataGrid;
        }

        private TextBox CreateNameSearchTextBox()
        {
            return TextBoxFactory.CreateSearchBox("Name", "Search by template name",
                nameof(DinnerTemplatesGridViewModel.NameSearchText));
        }

        private bool ShouldIncludeColumn(DinnerTemplateGridColumns column)
        {
            if (ViewModel.Arguments.DisplayMode == GridDisplayMode.NORMAL)
            {
                return true;
            }

            return column == DinnerTemplateGridColumns.TEMPLATE;
        }

        private IValueConverter? GetColumnConverter(DinnerTemplateGridColumns column)
        {
            return column switch
            {
                DinnerTemplateGridColumns.IS_TAKE_AWAY => new BooleanConverter(),
                DinnerTemplateGridColumns.HAS_LEFTOVERS => new BooleanConverter(),
                DinnerTemplateGridColumns.LEFTOVERS_ENOUGH_FOR_DINNER => new BooleanConverter(),
                DinnerTemplateGridColumns.IS_LEFTOVERS => new BooleanConverter(),
                DinnerTemplateGridColumns.IS_EATEN_OUT => new BooleanConverter(),
                DinnerTemplateGridColumns.IS_READY_MADE_DISH => new BooleanConverter(),
                var _ => null,
            };
        }

        private string GetColumnBindingPath(DinnerTemplateGridColumns column)
        {
            return column switch
            {
                DinnerTemplateGridColumns.TEMPLATE => nameof(DinnerTemplate.Name),

                DinnerTemplateGridColumns.IS_TAKE_AWAY => nameof(DinnerTemplate.IsTakeAway),

                DinnerTemplateGridColumns.HAS_LEFTOVERS => nameof(DinnerTemplate.HasLeftovers),

                DinnerTemplateGridColumns.LEFTOVERS_ENOUGH_FOR_DINNER => nameof(
                    DinnerTemplate.LeftoversEnoughForDinner),

                DinnerTemplateGridColumns.IS_LEFTOVERS => nameof(DinnerTemplate.IsLeftovers),

                DinnerTemplateGridColumns.IS_EATEN_OUT => nameof(DinnerTemplate.IsEatenOut),

                DinnerTemplateGridColumns.IS_READY_MADE_DISH => nameof(DinnerTemplate.IsReadyMadeDish),

                var _ => throw new ArgumentOutOfRangeException(nameof(column), column, null),
            };
        }

        private enum DinnerTemplateGridColumns
        {
            TEMPLATE = 0,
            IS_TAKE_AWAY = 1,
            HAS_LEFTOVERS = 2,
            LEFTOVERS_ENOUGH_FOR_DINNER = 3,
            IS_LEFTOVERS = 4,
            IS_EATEN_OUT = 5,
            IS_READY_MADE_DISH = 6,
        }
    }
}
