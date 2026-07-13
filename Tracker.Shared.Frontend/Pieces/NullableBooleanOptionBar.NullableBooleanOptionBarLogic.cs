namespace Tracker.Shared.Frontend.Pieces;

internal sealed partial class NullableBooleanOptionBar
{
    internal sealed class NullableBooleanOptionBarLogic
    {
        private NullableBooleanOptionBarViewModel ViewModel { get; }

        public NullableBooleanOptionBarLogic(NullableBooleanOptionBarViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        internal void YesClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedValue = true;
        }

        internal void NoClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedValue = false;
        }

        internal void AnyClicked(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedValue = null;
        }
    }
}
