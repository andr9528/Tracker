using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Automation.Provider;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Shared.Frontend.Pieces;

internal sealed partial class DateTimePicker
{
    internal sealed class DateTimePickerLogic(DateTimePickerViewModel viewModel)
        : BaseLogic<DateTimePickerViewModel>(viewModel)
    {
        public void DateButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenPicker(ViewModel.DatePicker);
        }

        public void TimeButtonClicked(object sender, RoutedEventArgs e)
        {
            OpenPicker(ViewModel.TimePicker);
        }

        private void OpenPicker(FrameworkElement picker)
        {
            picker.Visibility = Visibility.Visible;
            picker.UpdateLayout();

            var templateButton = FindDescendant<Button>(picker);

            if (templateButton is null)
            {
                return;
            }

            ButtonAutomationPeer peer = new(templateButton);

            if (peer.GetPattern(PatternInterface.Invoke) is IInvokeProvider invokeProvider)
            {
                invokeProvider.Invoke();
            }

            picker.Visibility = Visibility.Collapsed;
        }

        private T? FindDescendant<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);

            for (var i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is T matchingChild)
                {
                    return matchingChild;
                }

                var nestedChild = FindDescendant<T>(child);

                if (nestedChild is not null)
                {
                    return nestedChild;
                }
            }

            return default;
        }
    }
}
