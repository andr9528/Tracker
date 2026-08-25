using Microsoft.UI.Xaml.Data;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Shared.Frontend.Core.Details
{
    public abstract class BaseDetailsPageUi<TLogic, TViewModel>(TLogic logic, TViewModel viewModel)
        : BaseUi<TLogic, TViewModel>(logic, viewModel) where TLogic : BaseDetailsPageLogic<TViewModel>
        where TViewModel : BaseDetailsPageViewModel
    {
        protected Grid CreateDetailsButtonsGrid(params UIElement[] leftButtons)
        {
            var columns = leftButtons.Select(_ => GridLength.Auto).Prepend(GridLength.Auto)
                .Append(new GridLength(1, GridUnitType.Star)).Append(GridLength.Auto).Append(GridLength.Auto)
                .Append(GridLength.Auto).Append(GridLength.Auto).ToArray();

            Grid grid = GridFactory.CreateDefaultGrid().DefineColumns(columns);

            grid.ColumnSpacing = 8;

            for (var i = 0; i < leftButtons.Length; i++)
                grid.Children.Add(leftButtons[i].SetColumn(i + 1));

            int rightSideStartColumn = leftButtons.Length + 2;

            grid.Children.Add(CreateDeleteButton().SetColumn(rightSideStartColumn));
            grid.Children.Add(CreateEditCheckBoxGrid().SetColumn(rightSideStartColumn + 1));
            grid.Children.Add(CreateSaveButton().SetColumn(rightSideStartColumn + 2));
            grid.Children.Add(CreateCancelButton().SetColumn(rightSideStartColumn + 3));

            return grid;
        }

        private Button CreateDeleteButton()
        {
            ViewModel.DeleteButton = new Button
            {
                Content = "Delete",
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new Thickness(20, 8, 20, 8),
            };

            ViewModel.DeleteButton.SetBinding(Control.IsEnabledProperty, new Binding
            {
                Path = new PropertyPath(nameof(BaseDetailsPageViewModel.CanDelete)),
                Mode = BindingMode.OneWay,
            });

            ViewModel.DeleteButton.Click += async (sender, args) => await Logic.DeleteClicked(sender, args);

            return ViewModel.DeleteButton;
        }

        private Grid CreateEditCheckBoxGrid()
        {
            Grid grid = GridFactory.CreateDefaultGrid().DefineColumns(GridLength.Auto, GridLength.Auto);

            TextBlock label = TextBlockFactory.CreateBlackText("Edit");
            label.VerticalAlignment = VerticalAlignment.Center;
            label.Margin = new Thickness(0, 0, 8, 0);

            CheckBox checkBox = CreateEditCheckBox();

            grid.Children.Add(label.SetColumn(0));
            grid.Children.Add(checkBox.SetColumn(1));

            return grid;
        }

        private CheckBox CreateEditCheckBox()
        {
            CheckBox checkBox = CheckBoxFactory.CreateLightCheckBox(nameof(BaseDetailsPageViewModel.IsEditing));

            checkBox.VerticalAlignment = VerticalAlignment.Center;
            checkBox.HorizontalAlignment = HorizontalAlignment.Left;

            checkBox.Checked += Logic.EditCheckBoxChanged;
            checkBox.Unchecked += Logic.EditCheckBoxChanged;

            ViewModel.EditCheckBox = checkBox;

            return checkBox;
        }

        private Button CreateSaveButton()
        {
            ViewModel.SaveButton = SimplePieceFactory.CreateSaveButton(Logic.SaveClicked);

            ViewModel.SaveButton.SetBinding(ContentControl.ContentProperty, new Binding
            {
                Path = new PropertyPath(nameof(BaseDetailsPageViewModel.SaveButtonText)),
                Mode = BindingMode.OneWay,
            });

            return ViewModel.SaveButton;
        }

        private Button CreateCancelButton()
        {
            ViewModel.CancelButton = SimplePieceFactory.CreateCancelButton(Logic.CancelClicked);

            ViewModel.CancelButton.SetBinding(ContentControl.ContentProperty, new Binding
            {
                Path = new PropertyPath(nameof(BaseDetailsPageViewModel.CancelButtonText)),
                Mode = BindingMode.OneWay,
            });

            return ViewModel.CancelButton;
        }
    }
}
