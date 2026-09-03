using Tracker.Shared.Frontend.Core;

namespace Tracker.Module.Dining.Presentation.Pieces.Dinners;

internal sealed partial class DinnerTemplateEditor
{
    internal sealed class DinnerTemplateEditorLogic : BaseLogic<DinnerTemplateEditorViewModel>, IDinnerTemplateEditorApi
    {
        public DinnerTemplateEditorLogic(DinnerTemplateEditorViewModel viewModel) : base(viewModel)
        {
            ViewModel.IsReadOnlyChanged += OnIsReadOnlyChanged;
        }

        #region Implementation of IDinnerTemplateEditorApi

        /// <inheritdoc />
        public string Name => ViewModel.Name;

        /// <inheritdoc />
        public bool IsTakeAway => ViewModel.PropertiesEditor.Api.IsTakeAway;

        /// <inheritdoc />
        public bool HasLeftovers => ViewModel.PropertiesEditor.Api.HasLeftovers;

        /// <inheritdoc />
        public bool LeftoversEnoughForDinner =>
            ViewModel.PropertiesEditor.Api.LeftoversEnoughForDinner;

        /// <inheritdoc />
        public bool IsLeftovers => ViewModel.PropertiesEditor.Api.IsLeftovers;

        /// <inheritdoc />
        public bool IsEatenOut => ViewModel.PropertiesEditor.Api.IsEatenOut;

        /// <inheritdoc />
        public bool IsReadyMadeDish => ViewModel.PropertiesEditor.Api.IsReadyMadeDish;

        /// <inheritdoc />
        public void SetReadOnly(bool isReadOnly)
        {
            ViewModel.IsReadOnly = isReadOnly;
        }

        #endregion

        private void OnIsReadOnlyChanged(object? sender, EventArgs e)
        {
            UpdateReadOnlyState();
        }

        internal void UpdateReadOnlyState()
        {
            ViewModel.NameTextBox.IsReadOnly = ViewModel.IsReadOnly;

            ViewModel.PropertiesEditor.Api.SetReadOnly(ViewModel.IsReadOnly);
        }
    }
}
