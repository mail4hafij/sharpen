[Component]
public class CategoryList
{
    // Narrow selects, not a whole-store [Inject] - this component only
    // re-renders when one of these specific slices changes (e.g. typing into
    // an item's draft price no longer re-renders this list at all).
    [Inject(Select = nameof(RestaurantStore.Categories))] private List<Category> categories;
    [Inject(Select = nameof(RestaurantStore.SelectedCategoryId))] private int selectedCategoryId;
    [Inject(Select = nameof(RestaurantStore.SelectCategory))] private Action<int> selectCategory;
    [Inject(Select = nameof(RestaurantStore.OpenEditCategoryForm))] private Action<int, string> openEditCategoryForm;
    [Inject(Select = nameof(RestaurantStore.DeleteCategory))] private Action<int> deleteCategory;
    [Inject(Select = nameof(RestaurantStore.OpenAddCategoryForm))] private Action openAddCategoryForm;

    public JSX Render() =>
        <div>
            <section className="categories">
                <h2>Categories</h2>
                <div className="category-grid">
                    @foreach (var c in categories)
                    {
                        <div key={c.Id} className={c.Id == selectedCategoryId ? "category-card selected" : "category-card"}>
                            <img src={c.ImageUrl} className="category-image" onClick={() => selectCategory(c.Id)} />
                            <p className="category-name" onClick={() => selectCategory(c.Id)}>{c.Name}</p>
                            <div className="card-actions">
                                <button onClick={() => openEditCategoryForm(c.Id, c.Name)}>Edit</button>
                                <button onClick={() => deleteCategory(c.Id)}>Delete</button>
                            </div>
                        </div>
                    }
                    <div className="category-card add-card" onClick={openAddCategoryForm}>
                        <p>+ Add Category</p>
                    </div>
                </div>
            </section>
            <CategoryAdd />
            <CategoryEdit />
        </div>;
}
