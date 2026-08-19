[Component]
public class ItemList
{
    // Narrow selects, same reasoning as CategoryList - this is exactly the
    // component that used to re-render on every category-form keystroke
    // (CategoryDraftName) despite having nothing to do with it, since the old
    // whole-store [Inject] subscribed to every field in RestaurantStore.
    [Inject(Select = nameof(RestaurantStore.Items))] private List<MenuItem> items;
    [Inject(Select = nameof(RestaurantStore.SelectedCategoryId))] private int selectedCategoryId;
    [Inject(Select = nameof(RestaurantStore.OpenEditItemForm))] private Action<int, string, string, double> openEditItemForm;
    [Inject(Select = nameof(RestaurantStore.DeleteItem))] private Action<int> deleteItem;
    [Inject(Select = nameof(RestaurantStore.OpenAddItemForm))] private Action openAddItemForm;

    public JSX Render() =>
        <div>
            <section className="menu-items">
                <h2>Menu Items</h2>
                <div className="item-list">
                    @foreach (var item in items.Where(i => i.CategoryId == selectedCategoryId).ToList())
                    {
                        <div key={item.Id} className="item-card">
                            <img src={item.ImageUrl} className="item-image" />
                            <div className="item-info">
                                <p className="item-name">{item.Name}</p>
                                <p className="item-description">{item.Description}</p>
                                <p className="item-price">${item.Price}</p>
                            </div>
                            <div className="card-actions">
                                <button onClick={() => openEditItemForm(item.Id, item.Name, item.Description, item.Price)}>Edit</button>
                                <button onClick={() => deleteItem(item.Id)}>Delete</button>
                            </div>
                        </div>
                    }
                </div>
                <button className="add-item-button" onClick={openAddItemForm}>+ Add Item</button>
            </section>
            <ItemAdd />
            <ItemEdit />
        </div>;
}
