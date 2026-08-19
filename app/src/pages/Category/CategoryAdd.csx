[Component]
public class CategoryAdd
{
    [Inject(Select = nameof(RestaurantStore.ShowAddCategoryForm))] private bool showAddCategoryForm;
    [Inject(Select = nameof(RestaurantStore.CategoryDraftName))] private string categoryDraftName;
    [Inject(Select = nameof(RestaurantStore.SetCategoryDraftName))] private Action<string> setCategoryDraftName;
    [Inject(Select = nameof(RestaurantStore.SaveNewCategory))] private Action saveNewCategory;
    [Inject(Select = nameof(RestaurantStore.CloseAddCategoryForm))] private Action closeAddCategoryForm;

    public JSX Render() =>
        <div>
            @if (showAddCategoryForm)
            {
                <div className="modal-overlay">
                    <div className="modal">
                        <h3>Add Category</h3>
                        <label>Name</label>
                        <input value={categoryDraftName} onChange={e => setCategoryDraftName(e.target.value)} />
                        <div className="modal-actions">
                            <button onClick={saveNewCategory}>Save</button>
                            <button onClick={closeAddCategoryForm}>Cancel</button>
                        </div>
                    </div>
                </div>
            }
        </div>;
}
