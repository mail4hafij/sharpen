[Component]
public class ItemEdit
{
    [Inject(Select = nameof(RestaurantStore.ShowEditItemForm))] private bool showEditItemForm;
    [Inject(Select = nameof(RestaurantStore.ItemDraftName))] private string itemDraftName;
    [Inject(Select = nameof(RestaurantStore.SetItemDraftName))] private Action<string> setItemDraftName;
    [Inject(Select = nameof(RestaurantStore.ItemDraftDescription))] private string itemDraftDescription;
    [Inject(Select = nameof(RestaurantStore.SetItemDraftDescription))] private Action<string> setItemDraftDescription;
    [Inject(Select = nameof(RestaurantStore.ItemDraftPrice))] private string itemDraftPrice;
    [Inject(Select = nameof(RestaurantStore.SetItemDraftPrice))] private Action<string> setItemDraftPrice;
    [Inject(Select = nameof(RestaurantStore.SaveEditedItem))] private Action saveEditedItem;
    [Inject(Select = nameof(RestaurantStore.CloseEditItemForm))] private Action closeEditItemForm;

    public JSX Render() =>
        <div>
            @if (showEditItemForm)
            {
                <div className="modal-overlay">
                    <div className="modal">
                        <h3>Edit Menu Item</h3>
                        <label>Name</label>
                        <input value={itemDraftName} onChange={e => setItemDraftName(e.target.value)} />
                        <label>Description</label>
                        <input value={itemDraftDescription} onChange={e => setItemDraftDescription(e.target.value)} />
                        <label>Price</label>
                        <input value={itemDraftPrice} onChange={e => setItemDraftPrice(e.target.value)} />
                        <div className="modal-actions">
                            <button onClick={saveEditedItem}>Save</button>
                            <button onClick={closeEditItemForm}>Cancel</button>
                        </div>
                    </div>
                </div>
            }
        </div>;
}
