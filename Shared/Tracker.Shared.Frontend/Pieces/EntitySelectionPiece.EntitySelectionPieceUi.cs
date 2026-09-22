using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Converters;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectionPiece<T>
{
    private sealed class EntitySelectionPieceUi(
        EntitySelectionPieceLogic logic,
        EntitySelectionPieceViewModel viewModel)
        : BaseUi<EntitySelectionPieceLogic, EntitySelectionPieceViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.ColumnSpacing = 8;
            grid.RowSpacing = 8;

            grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto,
                new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeader().SetRow(0).SetColumn(0, 3));

            grid.Children.Add(ViewModel.Arguments.AvailableGrid.Content.SetRow(1).SetColumn(0));

            grid.Children.Add(CreateButtonGrid().SetRow(1).SetColumn(1));

            grid.Children.Add(ViewModel.Arguments.SuppliedGrid.Content.SetRow(1).SetColumn(2));
        }

        private TextBlock CreateHeader()
        {
            return TextBlockFactory.CreateHeader(ViewModel.Arguments.Header);
        }

        private Grid CreateButtonGrid()
        {
            var grid = new Grid
            {
                RowSpacing = 8,
                VerticalAlignment = VerticalAlignment.Center,
            };

            grid.DefineRows(GridLength.Auto, GridLength.Auto);

            grid.Children.Add(CreateAddButton().SetRow(0));

            grid.Children.Add(CreateRemoveButton().SetRow(1));

            return grid;
        }

        private Button CreateAddButton()
        {
            Button button = ButtonFactory.CreateButton(
                "Add", Symbol.Forward, Logic.AddClicked, HorizontalAlignment.Stretch);

            SetEnabledBinding(button);

            return button;
        }

        private Button CreateRemoveButton()
        {
            Button button = ButtonFactory.CreateButton(Symbol.Back, "Remove", Logic.RemoveClicked,
                HorizontalAlignment.Stretch);

            SetEnabledBinding(button);

            return button;
        }

        private void SetEnabledBinding(Button button)
        {
            button.SetBinding(Button.IsEnabledProperty, new Binding
            {
                Path = new PropertyPath(nameof(EntitySelectionPieceViewModel.IsReadOnly)),
                Converter = new InverseBooleanConverter(),
            });
        }
    }
}
