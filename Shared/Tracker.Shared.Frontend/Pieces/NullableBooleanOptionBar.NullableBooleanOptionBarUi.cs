using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Pieces;

internal sealed partial class NullableBooleanOptionBar
{
    internal sealed partial class NullableBooleanOptionBarUi(
        NullableBooleanOptionBarLogic logic,
        NullableBooleanOptionBarViewModel viewModel)
        : BaseUi<NullableBooleanOptionBarLogic, NullableBooleanOptionBarViewModel>(logic, viewModel)
    {
        protected override void ConfigureGrid(Grid grid)
        {
            grid.DefineRows(GridLength.Auto, new GridLength(1, GridUnitType.Star));
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(CreateHeader().SetRow(0));
            grid.Children.Add(CreateOptionsGrid().SetRow(1));
        }

        private TextBlock CreateHeader()
        {
            TextBlock header = TextBlockFactory.CreateBlackText();

            header.Text = ViewModel.Header;
            header.Margin = new Thickness(6, 4, 8, 0);

            return header;
        }

        private Grid CreateOptionsGrid()
        {
            Grid grid = GridFactory.CreateDefaultGrid();

            grid.Margin = new Thickness(4);
            grid.ColumnSpacing = 0;
            grid.DefineColumns(GridUnitType.Star, [1, 1, 1,]);

            grid.Children.Add(CreateYesButton().SetColumn(0));
            grid.Children.Add(CreateNoButton().SetColumn(1));
            grid.Children.Add(CreateAnyButton().SetColumn(2));

            return grid;
        }

        private RadioButton CreateYesButton()
        {
            ViewModel.YesButton = ButtonFactory.CreateNullableBooleanOptionButton("Yes");
            ViewModel.YesButton.Click += Logic.YesClicked;

            return ViewModel.YesButton;
        }

        private RadioButton CreateNoButton()
        {
            ViewModel.NoButton = ButtonFactory.CreateNullableBooleanOptionButton("No");
            ViewModel.NoButton.Click += Logic.NoClicked;

            return ViewModel.NoButton;
        }

        private RadioButton CreateAnyButton()
        {
            ViewModel.AnyButton = ButtonFactory.CreateNullableBooleanOptionButton("Either");
            ViewModel.AnyButton.Click += Logic.AnyClicked;
            ViewModel.AnyButton.IsChecked = true;

            return ViewModel.AnyButton;
        }
    }
}
