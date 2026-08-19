import { CategoryAdd } from "./CategoryAdd";
import { CategoryEdit } from "./CategoryEdit";
import { useRestaurantStore } from "../../stores/RestaurantStore";
import type { Category } from "../../stores/RestaurantStore";
export function CategoryList() {
    const categories: Category[] = useRestaurantStore(s => s.categories);
    const selectedCategoryId: number = useRestaurantStore(s => s.selectedCategoryId);
    const selectCategory: (arg0: number) => void = useRestaurantStore(s => s.selectCategory);
    const openEditCategoryForm: (arg0: number, arg1: string) => void = useRestaurantStore(s => s.openEditCategoryForm);
    const deleteCategory: (arg0: number) => void = useRestaurantStore(s => s.deleteCategory);
    const openAddCategoryForm: () => void = useRestaurantStore(s => s.openAddCategoryForm);
    return (<div><section className="categories"><h2>Categories</h2><div className="category-grid">{categories.map(c => <div key={c.id} className={c.id === selectedCategoryId ? "category-card selected" : "category-card"}><img src={c.imageUrl} className="category-image" onClick={() => selectCategory(c.id)}/><p className="category-name" onClick={() => selectCategory(c.id)}>{c.name}</p><div className="card-actions"><button onClick={() => openEditCategoryForm(c.id, c.name)}>Edit</button><button onClick={() => deleteCategory(c.id)}>Delete</button></div></div>)}<div className="category-card add-card" onClick={openAddCategoryForm}><p>+ Add Category</p></div></div></section><CategoryAdd /><CategoryEdit /></div>);
}
