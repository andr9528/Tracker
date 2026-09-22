using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectionPiece<T>
{
    private sealed class EntitySelectionPieceUi(
        EntitySelectionPieceLogic logic,
        EntitySelectionPieceArguments arguments)
    {
        internal Grid CreateContentGrid()
        {
            var grid = new Grid
            {
                ColumnSpacing = 8,
            };

            grid.DefineColumns(new GridLength(1, GridUnitType.Star), GridLength.Auto,
                new GridLength(1, GridUnitType.Star));

            grid.Children.Add(arguments.AvailableGrid.Content.SetColumn(0));
            grid.Children.Add(CreateButtonGrid().SetColumn(1));
            grid.Children.Add(arguments.SuppliedGrid.Content.SetColumn(2));

            return grid;
        }

        private Grid CreateButtonGrid()
        {
            var grid = new Grid
            {
                RowSpacing = 8,
                VerticalAlignment = VerticalAlignment.Center,
            };

            grid.DefineRows(GridLength.Auto, GridLength.Auto);

            grid.Children.Add(ButtonFactory.CreateButton("Add", Symbol.Forward, logic.AddClicked).SetRow(0));

            grid.Children.Add(ButtonFactory.CreateButton(Symbol.Back, "Remove", logic.RemoveClicked).SetRow(1));

            return grid;
        }
    }
}
