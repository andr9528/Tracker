﻿namespace Tracker.Frontend.Uno.Presentation;

public sealed partial class SecondPage : Page
{
    public SecondPage()
    {
        this.DataContext<SecondViewModel>((page, vm) => page
            .Background(Theme.Brushes.Background.Default)
            .Content(new Grid()
                .SafeArea(SafeArea.InsetMask.VisibleBounds)
                .Children(
                new NavigationBar()
                    .Content("Second Page")
                    .MainCommand(new AppBarButton()
                        .Icon(new BitmapIcon().UriSource(new Uri("ms-appx:///Assets/Images/back.png")))
                    ),
                new TextBlock()
                    .Text(() => vm.Entity.Name)
                    .HorizontalAlignment(HorizontalAlignment.Center)
                    .VerticalAlignment(VerticalAlignment.Center))));
    }
}

