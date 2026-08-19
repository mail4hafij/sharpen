import { useRestaurantStore } from "../../stores/RestaurantStore";
export function ItemAdd() {
    const showAddItemForm: boolean = useRestaurantStore(s => s.showAddItemForm);
    const itemDraftName: string = useRestaurantStore(s => s.itemDraftName);
    const setItemDraftName: (arg0: string) => void = useRestaurantStore(s => s.setItemDraftName);
    const itemDraftDescription: string = useRestaurantStore(s => s.itemDraftDescription);
    const setItemDraftDescription: (arg0: string) => void = useRestaurantStore(s => s.setItemDraftDescription);
    const itemDraftPrice: string = useRestaurantStore(s => s.itemDraftPrice);
    const setItemDraftPrice: (arg0: string) => void = useRestaurantStore(s => s.setItemDraftPrice);
    const saveNewItem: () => void = useRestaurantStore(s => s.saveNewItem);
    const closeAddItemForm: () => void = useRestaurantStore(s => s.closeAddItemForm);
    return (<div>{showAddItemForm ? <div className="modal-overlay"><div className="modal"><h3>Add Menu Item</h3><label>Name</label><input value={itemDraftName} onChange={e => setItemDraftName(e.target.value)}/><label>Description</label><input value={itemDraftDescription} onChange={e => setItemDraftDescription(e.target.value)}/><label>Price</label><input value={itemDraftPrice} onChange={e => setItemDraftPrice(e.target.value)}/><div className="modal-actions"><button onClick={saveNewItem}>Save</button><button onClick={closeAddItemForm}>Cancel</button></div></div></div> : null}</div>);
}
