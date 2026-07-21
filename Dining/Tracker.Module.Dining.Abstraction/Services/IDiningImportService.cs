using Tracker.Module.Dining.Abstraction.Records;

namespace Tracker.Module.Dining.Abstraction.Services;

public interface IDiningImportService
{
    Task<ImportResult> Import(string filePath);
}
