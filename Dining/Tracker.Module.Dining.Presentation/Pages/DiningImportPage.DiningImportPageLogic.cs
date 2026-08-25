using Windows.Storage.Pickers;
using Microsoft.Extensions.Logging;
using Tracker.Module.Dining.Abstraction.Records;
using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Core;
using WinRT.Interop;

namespace Tracker.Module.Dining.Presentation.Pages;

internal sealed partial class DiningImportPage
{
    internal sealed class DiningImportPageLogic : BaseLogic<DiningImportPageViewModel>
    {
        private readonly ILogger<DiningImportPageLogic> logger;

        public DiningImportPageLogic(DiningImportPageViewModel viewModel) : base(viewModel)
        {
            logger = viewModel.Arguments.LoggerFactory.CreateLogger<DiningImportPageLogic>();
        }

        public async void BrowseClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                FileOpenPicker picker = CreateFilePicker();

                StorageFile? file = await picker.PickSingleFileAsync();

                if (file is null)
                {
                    logger.LogInformation("User cancelled file selection.");
                    AddStatusMessage("File selection cancelled.");
                    return;
                }

                logger.LogInformation("Selected file '{FilePath}'.", file.Path);

                ViewModel.SelectedFilePath = file.Path;
                ViewModel.Result = null;

                AddStatusMessage($"Selected file: {file.Path}");
            }
            catch (Exception exe)
            {
                logger.LogError(exe, "Unexpected exception while browsing for Excel file.");
                FailImport(exe);
            }
        }

        public async void ImportClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ViewModel.CanImport)
                    return;

                BeginImport();

                ImportResult result = await ViewModel.Arguments.ImportService.Import(ViewModel.SelectedFilePath);

                CompleteImport(result);
            }
            catch (Exception exe)
            {
                logger.LogWarning(exe, "Error occurred during importing of Excel File.");
                FailImport(exe);
            }
        }

        private void BeginImport()
        {
            ViewModel.IsImporting = true;
            ViewModel.Result = null;

            AddStatusMessage("Import started.");
        }

        private void CompleteImport(ImportResult result)
        {
            ViewModel.Result = result;
            ViewModel.IsImporting = false;

            AddStatusMessage("Import completed successfully.");
        }

        private void FailImport(Exception exception)
        {
            ViewModel.IsImporting = false;

            AddStatusMessage($"Import failed: {exception.Message}");
        }

        private void AddStatusMessage(string message)
        {
            ViewModel.StatusMessages.Add($"{DateTime.Now:HH:mm:ss} - {message}");
        }

        private FileOpenPicker CreateFilePicker()
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                ViewMode = PickerViewMode.List,
            };

            picker.FileTypeFilter.Add(".xlsx");
            picker.FileTypeFilter.Add(".xls");

            Window mainWindow = ViewModel.Arguments.Accessor.MainWindow ??
                                throw new InvalidOperationException("The main window has not been initialized.");

            nint windowHandle = WindowNative.GetWindowHandle(mainWindow);

            if (windowHandle == 0)
                throw new InvalidOperationException("The main window returned an invalid native window handle.");

            InitializeWithWindow.Initialize(picker, windowHandle);

            return picker;
        }
    }
}
