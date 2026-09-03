using Tracker.Module.Dining.Abstraction.Entity;
using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerPropertiesEditor
{
    internal sealed class DinnerPropertiesEditorLogic : BaseLogic<DinnerPropertiesEditorViewModel>,
        IDinnerPropertiesEditorApi
    {
        #region Implementation of IDinnerPropertiesEditorApi

        /// <inheritdoc />
        public bool IsTakeAway => ViewModel.IsTakeAway;

        /// <inheritdoc />
        public bool HasLeftovers => ViewModel.HasLeftovers;

        /// <inheritdoc />
        public bool LeftoversEnoughForDinner => ViewModel.LeftoversEnoughForDinner;

        /// <inheritdoc />
        public bool IsLeftovers => ViewModel.IsLeftovers;

        /// <inheritdoc />
        public bool IsEatenOut => ViewModel.IsEatenOut;

        /// <inheritdoc />
        public bool IsReadyMadeDish => ViewModel.IsReadyMadeDish;

        /// <inheritdoc />
        public void ApplyTemplate(IDinnerTemplate template)
        {
            ArgumentNullException.ThrowIfNull(template);

            ViewModel.IsTakeAway = template.IsTakeAway;
            ViewModel.HasLeftovers = template.HasLeftovers;
            ViewModel.LeftoversEnoughForDinner = template.LeftoversEnoughForDinner;
            ViewModel.IsLeftovers = template.IsLeftovers;
            ViewModel.IsEatenOut = template.IsEatenOut;
            ViewModel.IsReadyMadeDish = template.IsReadyMadeDish;
        }

        /// <inheritdoc />
        public void SetReadOnly(bool isReadOnly)
        {
            ViewModel.IsReadOnly = isReadOnly;
        }

        #endregion

        public DinnerPropertiesEditorLogic(DinnerPropertiesEditorViewModel viewModel) : base(viewModel)
        {
            ViewModel.IsReadOnlyChanged += OnIsReadOnlyChanged;
        }

        private void OnIsReadOnlyChanged(object? sender, EventArgs e)
        {
            UpdateReadOnlyState();
        }

        internal void UpdateReadOnlyState()
        {
            foreach (CheckBox checkBox in ViewModel.CheckBoxes)
            {
                checkBox.IsEnabled = !ViewModel.IsReadOnly;
            }
        }
    }
}
