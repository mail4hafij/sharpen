import { ItemAdd } from "./ItemAdd";
import { ItemEdit } from "./ItemEdit";
import { useRestaurantStore } from "../../stores/RestaurantStore";
import type { MenuItem } from "../../stores/RestaurantStore";
export function ItemList() {
    const items: MenuItem[] = useRestaurantStore(s => s.items);
    const selectedCategoryId: number = useRestaurantStore(s => s.selectedCategoryId);
    const openEditItemForm: (arg0: number, arg1: string, arg2: string, arg3: number) => void = useRestaurantStore(s => s.openEditItemForm);
    const deleteItem: (arg0: number) => void = useRestaurantStore(s => s.deleteItem);
    const openAddItemForm: () => void = useRestaurantStore(s => s.openAddItemForm);
    return (<div><section className="menu-items"><h2>Menu Items</h2><div className="item-list">{items.filter(i => i.categoryId === selectedCategoryId).map(item => <div key={item.id} className="item-card"><img src={item.imageUrl} className="item-image"/><div className="item-info"><p className="item-name">{item.name}</p><p className="item-description">{item.description}</p><p className="item-price">${item.price}</p></div><div className="card-actions"><button onClick={() => openEditItemForm(item.id, item.name, item.description, item.price)}>Edit</button><button onClick={() => deleteItem(item.id)}>Delete</button></div></div>)}</div><button className="add-item-button" onClick={openAddItemForm}>+ Add Item</button></section><ItemAdd /><ItemEdit /></div>);
}
