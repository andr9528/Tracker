using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

public sealed partial class EntitySelectorPiece<T>
{
    private sealed class EntitySelectorPieceUi(EntitySelectorPieceLogic logic, EntitySelectorPieceViewModel viewModel)
        : BaseUi<EntitySelectorPieceLogic, EntitySelectorPieceViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.RowSpacing = 8;

            grid.DefineRows(GridLength.Auto, GridLength.Auto, new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeader().SetRow(0));
            grid.Children.Add(CreateSelectedEntityText().SetRow(1));
            grid.Children.Add(ViewModel.Arguments.Grid.Content.SetRow(2));
        }

        private TextBlock CreateHeader()
        {
            return TextBlockFactory.CreateHeader(ViewModel.Arguments.Header);
        }

        private TextBlock CreateSelectedEntityText()
        {
            TextBlock textBlock = TextBlockFactory.CreateBlackText();

            textBlock.TextWrapping = TextWrapping.Wrap;

            textBlock.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(EntitySelectorPieceViewModel.SelectedEntityText)),
            });

            return textBlock;
        }
    }
}
