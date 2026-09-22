using Tracker.Module.Dining.Model.Entity;
using Tracker.Shared.Frontend.Factory;
using static Tracker.Module.Dining.Presentation.Pieces.Dishes.DishEditor;

namespace Tracker.Module.Dining.Presentation.Pieces.Dishes;

internal sealed partial class DishEditor : Border
{
    private DishEditorViewModel ViewModel =>
        (DishEditorViewModel) DataContext;

    private DishEditorLogic Logic { get; }

    private DishEditorUi Ui { get; }

    public IDishEditorApi Api => Logic;

    public DishEditor(DishEditorArguments arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.ConfigurePieceBorder();

        DataContext = new DishEditorViewModel(arguments);

        Logic = new DishEditorLogic(ViewModel);
        Ui = new DishEditorUi(Logic, ViewModel);

        Child = Ui.CreateContentGrid();

        Logic.ApplyInitialIngredients();
        Logic.UpdateReadOnlyState();
    }

    internal sealed record DishEditorArguments(
        DiningArgumentsFactory ArgumentsFactory,
        Dish? Dish = null,
        bool IsReadOnly = true);

    internal interface IDishEditorApi
    {
        string Name { get; }

        IReadOnlyCollection<Ingredient> Ingredients { get; }

        event EventHandler? NameChanged;

        void ApplyDish(Dish dish);

        void SetReadOnly(bool isReadOnly);
    }
}
