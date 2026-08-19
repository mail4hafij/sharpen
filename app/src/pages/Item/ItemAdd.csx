[Component]
public class ItemAdd
{
    [Inject(Select = nameof(RestaurantStore.ShowAddItemForm))] private bool showAddItemForm;
    [Inject(Select = nameof(RestaurantStore.ItemDraftName))] private string itemDraftName;
    [Inject(Select = nameof(RestaurantStore.SetItemDraftName))] private Action<string> setItemDraftName;
    [Inject(Select = nameof(RestaurantStore.ItemDraftDescription))] private string itemDraftDescription;
    [Inject(Select = nameof(RestaurantStore.SetItemDraftDescription))] private Action<string> setItemDraftDescription;
    [Inject(Select = nameof(RestaurantStore.ItemDraftPrice))] private string itemDraftPrice;
    [Inject(Select = nameof(RestaurantStore.SetItemDraftPrice))] private Action<string> setItemDraftPrice;
    [Inject(Select = nameof(RestaurantStore.SaveNewItem))] private Action saveNewItem;
    [Inject(Select = nameof(RestaurantStore.CloseAddItemForm))] private Action closeAddItemForm;

    public JSX Render() =>
        <div>
            @if (showAddItemForm)
            {
                <div className="modal-overlay">
                    <div className="modal">
                        <h3>Add Menu Item</h3>
                        <label>Name</label>
                        <input value={itemDraftName} onChange={e => setItemDraftName(e.target.value)} />
                        <label>Description</label>
                        <input value={itemDraftDescription} onChange={e => setItemDraftDescription(e.target.value)} />
                        <label>Price</label>
                        <input value={itemDraftPrice} onChange={e => setItemDraftPrice(e.target.value)} />
                        <div className="modal-actions">
                            <button onClick={saveNewItem}>Save</button>
                            <button onClick={closeAddItemForm}>Cancel</button>
                        </div>
                    </div>
                </div>
            }
        </div>;
}
