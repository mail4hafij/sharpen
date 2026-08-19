[Component]
public class CategoryEdit
{
    [Inject(Select = nameof(RestaurantStore.ShowEditCategoryForm))] private bool showEditCategoryForm;
    [Inject(Select = nameof(RestaurantStore.CategoryDraftName))] private string categoryDraftName;
    [Inject(Select = nameof(RestaurantStore.SetCategoryDraftName))] private Action<string> setCategoryDraftName;
    [Inject(Select = nameof(RestaurantStore.SaveEditedCategory))] private Action saveEditedCategory;
    [Inject(Select = nameof(RestaurantStore.CloseEditCategoryForm))] private Action closeEditCategoryForm;

    public JSX Render() =>
        <div>
            @if (showEditCategoryForm)
            {
                <div className="modal-overlay">
                    <div className="modal">
                        <h3>Edit Category</h3>
                        <label>Name</label>
                        <input value={categoryDraftName} onChange={e => setCategoryDraftName(e.target.value)} />
                        <div className="modal-actions">
                            <button onClick={saveEditedCategory}>Save</button>
                            <button onClick={closeEditCategoryForm}>Cancel</button>
                        </div>
                    </div>
                </div>
            }
        </div>;
}
