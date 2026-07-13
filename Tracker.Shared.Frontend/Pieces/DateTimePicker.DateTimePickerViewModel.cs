using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace Tracker.Shared.Frontend.Pieces;

internal sealed partial class DateTimePicker
{
    internal sealed partial class DateTimePickerViewModel : ObservableObject
    {
        private bool shouldSkipNextDateChange = true;
        private bool shouldSkipNextTimeChange = true;

        public DateTimePickerArguments Arguments { get; }
        public event EventHandler? SelectedDateTimeChanged;

        [ObservableProperty] [NotifyPropertyChangedFor(nameof(SelectedDateText))]
        private DateTimeOffset? selectedDate;

        [ObservableProperty] [NotifyPropertyChangedFor(nameof(SelectedTimeText))]
        private TimeSpan? selectedTime;

        private readonly ILogger<DateTimePickerViewModel> logger;

        public string SelectedDateText => SelectedDate.HasValue
            ? SelectedDate.Value.ToString("dd.MM.yyyy")
            : "No Date is Set";

        public string SelectedTimeText => SelectedTime.HasValue
            ? SelectedTime.Value.ToString(@"hh\:mm")
            : "No Time is Set";

        /// <inheritdoc/>
        public DateTimePickerViewModel(DateTimePickerArguments arguments)
        {
            Arguments = arguments;
            selectedDate = arguments.InitialValue;
            selectedTime = arguments.InitialValue?.TimeOfDay;

            logger = Arguments.LoggerFactory.CreateLogger<DateTimePickerViewModel>();
        }

        internal void SetSelectedDateTime(DateTime? dateTime)
        {
            SelectedDate = dateTime;
            SelectedTime = dateTime?.TimeOfDay;
        }

        internal DateTime? SelectedDateTime =>
            SelectedDate.HasValue && SelectedTime.HasValue ? SelectedDate.Value.Date.Add(SelectedTime.Value) : null;

        internal DatePicker DatePicker { get; set; } = null!;
        internal TimePicker TimePicker { get; set; } = null!;
        internal Button DateButton { get; set; } = null!;
        internal Button TimeButton { get; set; } = null!;

        partial void OnSelectedDateChanged(DateTimeOffset? value)
        {
            if (shouldSkipNextDateChange)
            {
                shouldSkipNextDateChange = false;
                return;
            }

            RaiseSelectedDateTimeChangedIfComplete();
        }

        partial void OnSelectedTimeChanged(TimeSpan? value)
        {
            if (shouldSkipNextTimeChange)
            {
                shouldSkipNextTimeChange = false;
                return;
            }

            RaiseSelectedDateTimeChangedIfComplete();
        }

        private void RaiseSelectedDateTimeChangedIfComplete()
        {
            if (!SelectedDate.HasValue || !SelectedTime.HasValue)
            {
                return;
            }

            logger.LogDebug("Date/time changed for '{ArgumentsHeader}'", Arguments.Header);
            SelectedDateTimeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
