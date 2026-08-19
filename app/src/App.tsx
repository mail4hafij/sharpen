import { useEffect } from "react";
import { CategoryList } from "./pages/Category/CategoryList";
import { ItemList } from "./pages/Item/ItemList";
import { useRestaurantStore } from "./stores/RestaurantStore";
export function App() {
    const loadCategories: () => void = useRestaurantStore(s => s.loadCategories);
    const loadItems: () => void = useRestaurantStore(s => s.loadItems);
    useEffect(() => {
        loadCategories();
        loadItems();
    });
    return (<div className="restaurant-app"><header className="hero"><h1>My Kitchen</h1><p>A simple menu, built one category at a time.</p></header><CategoryList /><ItemList /></div>);
}
