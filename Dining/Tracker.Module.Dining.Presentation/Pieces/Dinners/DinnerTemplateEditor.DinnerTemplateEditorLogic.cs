using Tracker.Module.Dining.Model.Entity;
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
        public event EventHandler? NameChanged
        {
            add => ViewModel.NameChanged += value;
            remove => ViewModel.NameChanged -= value;
        }

        /// <inheritdoc />
        public event EventHandler? IsTakeAwayChanged
        {
            add => ViewModel.PropertiesEditor.Api.IsTakeAwayChanged += value;
            remove => ViewModel.PropertiesEditor.Api.IsTakeAwayChanged -= value;
        }

        /// <inheritdoc />
        public event EventHandler? HasLeftoversChanged
        {
            add => ViewModel.PropertiesEditor.Api.HasLeftoversChanged += value;
            remove => ViewModel.PropertiesEditor.Api.HasLeftoversChanged -= value;
        }

        /// <inheritdoc />
        public event EventHandler? LeftoversEnoughForDinnerChanged
        {
            add => ViewModel.PropertiesEditor.Api.LeftoversEnoughForDinnerChanged += value;
            remove => ViewModel.PropertiesEditor.Api.LeftoversEnoughForDinnerChanged -= value;
        }

        /// <inheritdoc />
        public event EventHandler? IsLeftoversChanged
        {
            add => ViewModel.PropertiesEditor.Api.IsLeftoversChanged += value;
            remove => ViewModel.PropertiesEditor.Api.IsLeftoversChanged -= value;
        }

        /// <inheritdoc />
        public event EventHandler? IsEatenOutChanged
        {
            add => ViewModel.PropertiesEditor.Api.IsEatenOutChanged += value;
            remove => ViewModel.PropertiesEditor.Api.IsEatenOutChanged -= value;
        }

        /// <inheritdoc />
        public event EventHandler? IsReadyMadeDishChanged
        {
            add => ViewModel.PropertiesEditor.Api.IsReadyMadeDishChanged += value;
            remove => ViewModel.PropertiesEditor.Api.IsReadyMadeDishChanged -= value;
        }

        /// <inheritdoc />
        public void ApplyDinnerTemplate(DinnerTemplate dinnerTemplate)
        {
            ArgumentNullException.ThrowIfNull(dinnerTemplate);

            ViewModel.Name = dinnerTemplate.Name;

            ViewModel.PropertiesEditor.Api.ApplyTemplate(dinnerTemplate);
        }

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
