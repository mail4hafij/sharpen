import { useRestaurantStore } from "../../stores/RestaurantStore";
export function ItemEdit() {
    const showEditItemForm: boolean = useRestaurantStore(s => s.showEditItemForm);
    const itemDraftName: string = useRestaurantStore(s => s.itemDraftName);
    const setItemDraftName: (arg0: string) => void = useRestaurantStore(s => s.setItemDraftName);
    const itemDraftDescription: string = useRestaurantStore(s => s.itemDraftDescription);
    const setItemDraftDescription: (arg0: string) => void = useRestaurantStore(s => s.setItemDraftDescription);
    const itemDraftPrice: string = useRestaurantStore(s => s.itemDraftPrice);
    const setItemDraftPrice: (arg0: string) => void = useRestaurantStore(s => s.setItemDraftPrice);
    const saveEditedItem: () => void = useRestaurantStore(s => s.saveEditedItem);
    const closeEditItemForm: () => void = useRestaurantStore(s => s.closeEditItemForm);
    return (<div>{showEditItemForm ? <div className="modal-overlay"><div className="modal"><h3>Edit Menu Item</h3><label>Name</label><input value={itemDraftName} onChange={e => setItemDraftName(e.target.value)}/><label>Description</label><input value={itemDraftDescription} onChange={e => setItemDraftDescription(e.target.value)}/><label>Price</label><input value={itemDraftPrice} onChange={e => setItemDraftPrice(e.target.value)}/><div className="modal-actions"><button onClick={saveEditedItem}>Save</button><button onClick={closeEditItemForm}>Cancel</button></div></div></div> : null}</div>);
}
