namespace Tracker.Shared.Frontend.Core.Details
{
    public abstract class BaseDetailsPageLogic<TViewModel>(TViewModel viewModel)
        : BaseLogic<TViewModel>(viewModel) where TViewModel : BaseDetailsPageViewModel
    {
        internal void EditCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            bool isEditing = ViewModel.EditCheckBox.IsChecked == true;

            ViewModel.IsEditing = isEditing;
            ViewModel.CanDelete = isEditing;

            SetEditorReadOnly(!isEditing);
        }

        internal async Task SaveClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ViewModel.HasChanges)
                {
                    NavigateBack();
                    return;
                }

                await SaveChanges();

                UpdateHasChanges();
                DisableEditing();
            }
            catch (Exception exe)
            {
                LogSaveError(exe);
            }
        }

        internal void CancelClicked(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.HasChanges)
            {
                NavigateBack();
                return;
            }

            ApplyEntityToEditor();
            DisableEditing();
        }

        private void DisableEditing()
        {
            ViewModel.IsEditing = false;
            ViewModel.EditCheckBox.IsChecked = false;
            ViewModel.CanDelete = false;

            SetEditorReadOnly(true);
        }

        protected void UpdateSaveAndCancelText()
        {
            ViewModel.SaveButtonText = ViewModel.HasChanges ? "Save" : "Okay";
            ViewModel.CancelButtonText = ViewModel.HasChanges ? "Cancel" : "Back";
        }

        protected async Task<ContentDialogResult> ShowDeleteConfirmation(string title, string content)
        {
            ContentDialog dialog = new()
            {
                Title = title,
                Content = content,
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = ViewModel.DeleteButton.XamlRoot,
            };

            return await dialog.ShowAsync();
        }

        public abstract Task DeleteClicked(object sender, RoutedEventArgs e);

        protected abstract void SetEditorReadOnly(bool isReadOnly);

        protected abstract Task SaveChanges();

        protected abstract void ApplyEntityToEditor();

        protected abstract void UpdateHasChanges();

        protected abstract void NavigateBack();

        protected abstract void LogSaveError(Exception exception);
    }
}
