using Tracker.Shared.Frontend.Abstraction;
using Tracker.Shared.Frontend.Core;
using Tracker.Shared.Frontend.Extensions;
using Tracker.Shared.Frontend.Factory;

namespace Tracker.Frontend.Presentation;

internal sealed partial class PageSelector
{
    private sealed class PageSelectorUi(
        PageSelectorLogic logic,
        PageSelectorViewModel viewModel,
        IEnumerable<IPageRegion> regionDefinitions,
        INavigationService navigationService) : BaseUi<PageSelectorLogic, PageSelectorViewModel>(logic, viewModel)
    {
        private const double PANE_COLUMN_WEIGHT = 12d;

        protected override void ConfigureGrid(Grid grid)
        {
            CreateFrames();

            grid.HorizontalAlignment = HorizontalAlignment.Stretch;
            grid.VerticalAlignment = VerticalAlignment.Stretch;
            grid.Margin = new Thickness(0);
            grid.Padding = new Thickness(0);

            const double contentColumnWeight = 100 - PANE_COLUMN_WEIGHT;

            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(PANE_COLUMN_WEIGHT, GridUnitType.Star),
                MinWidth = 180,
            });

            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(contentColumnWeight, GridUnitType.Star),
            });
        }

        protected override void AddControlsToGrid(Grid grid)
        {
            grid.Children.Add(ViewModel.PaneFrame.SetColumn(0));
            grid.Children.Add(ViewModel.ContentFrame.SetColumn(1));
        }

        private void CreateFrames()
        {
            ViewModel.Regions = regionDefinitions.ToList();
            ViewModel.MenuList = CreateMenuList(ViewModel.Regions);

            ViewModel.ContentFrame = new Frame();
            navigationService.RegisterContentFrame(ViewModel.ContentFrame);

            ViewModel.PaneFrame = new Frame
            {
                Content = CreateNavigationPaneContentGrid(),
            };
        }

        private Grid CreateNavigationPaneContentGrid()
        {
            Grid paneRoot = GridFactory.CreateDefaultGrid().DefineRows(GridLength.Auto,
                new GridLength(1, GridUnitType.Star), GridLength.Auto);

            paneRoot.Background = new SolidColorBrush(SimplePieceFactory.MenuBackgroundColour);
            paneRoot.HorizontalAlignment = HorizontalAlignment.Stretch;
            paneRoot.VerticalAlignment = VerticalAlignment.Stretch;
            paneRoot.Margin = new Thickness(0);
            paneRoot.Padding = new Thickness(0);

            paneRoot.Children.Add(CreateBackButton().SetRow(0));
            paneRoot.Children.Add(ViewModel.MenuList.SetRow(1));
            paneRoot.Children.Add(CreateOpenApplicationFolderButton().SetRow(2));

            return paneRoot;
        }

        private Button CreateOpenApplicationFolderButton()
        {
            StackPanel contentPanel = new()
            {
                Orientation = Orientation.Horizontal,
                Spacing = 8,
            };

            contentPanel.Children.Add(new SymbolIcon(Symbol.Folder));

            contentPanel.Children.Add(new TextBlock
            {
                Text = "Application Data",
                VerticalAlignment = VerticalAlignment.Center,
            });

            Button button = new()
            {
                Content = contentPanel,
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5),
                Padding = new Thickness(10, 5, 10, 5),
            };

            button.Click += Logic.OpenApplicationFolderButtonClicked;

            return button;
        }

        private Button CreateBackButton()
        {
            var button = new Button
            {
                Content = new SymbolIcon(Symbol.Back),
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White),
                BorderThickness = new Thickness(0),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(5),
                Padding = new Thickness(10, 5, 10, 5),
            };

            button.Click += Logic.BackButtonClicked;

            return button;
        }

        private ListView CreateMenuList(IEnumerable<IPageRegion> regions)
        {
            var menuList = new ListView
            {
                Background = new SolidColorBrush(Colors.Transparent),
                SelectionMode = ListViewSelectionMode.None,
                ItemsSource = regions,
                ItemTemplate = CreateMenuItemTemplate(),
                ItemContainerStyle = CreateMenuItemStyle(),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(5),
                IsItemClickEnabled = true,
            };

            menuList.ItemClick += Logic.MenuListItemClicked;

            return menuList;
        }

        private DataTemplate CreateMenuItemTemplate()
        {
            return new DataTemplate(() =>
            {
                Grid templateGrid = GridFactory.CreateDefaultGrid();
                templateGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
                templateGrid.Margin = new Thickness(0);

                templateGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = GridLength.Auto,
                });
                templateGrid.ColumnDefinitions.Add(new ColumnDefinition
                    {Width = new GridLength(1, GridUnitType.Star),});

                var iconPresenter = new ContentPresenter
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                iconPresenter.SetBinding(ContentPresenter.ContentProperty,
                    new Binding {Path = new PropertyPath(nameof(IPageRegion.Icon)),});

                var text = new TextBlock
                {
                    Foreground = new SolidColorBrush(Colors.White),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(5, 0, 0, 0),
                    TextWrapping = TextWrapping.NoWrap,
                    TextTrimming = TextTrimming.CharacterEllipsis,
                };
                text.SetBinding(TextBlock.TextProperty,
                    new Binding {Path = new PropertyPath(nameof(IPageRegion.DisplayName)),});

                templateGrid.Children.Add(iconPresenter.SetColumn(0));
                templateGrid.Children.Add(text.SetColumn(1));

                return templateGrid;
            });
        }

        private Style CreateMenuItemStyle()
        {
            var style = new Style(typeof(ListViewItem));
            style.Setters.Add(new Setter(BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            style.Setters.Add(new Setter(BorderThicknessProperty, new Thickness(0)));
            style.Setters.Add(new Setter(PaddingProperty, new Thickness(4, 2, 4, 2)));
            style.Setters.Add(new Setter(ForegroundProperty, new SolidColorBrush(Colors.White)));
            style.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
            return style;
        }
    }
}
