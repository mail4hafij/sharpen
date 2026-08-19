import { useRestaurantStore } from "../../stores/RestaurantStore";
export function CategoryAdd() {
    const showAddCategoryForm: boolean = useRestaurantStore(s => s.showAddCategoryForm);
    const categoryDraftName: string = useRestaurantStore(s => s.categoryDraftName);
    const setCategoryDraftName: (arg0: string) => void = useRestaurantStore(s => s.setCategoryDraftName);
    const saveNewCategory: () => void = useRestaurantStore(s => s.saveNewCategory);
    const closeAddCategoryForm: () => void = useRestaurantStore(s => s.closeAddCategoryForm);
    return (<div>{showAddCategoryForm ? <div className="modal-overlay"><div className="modal"><h3>Add Category</h3><label>Name</label><input value={categoryDraftName} onChange={e => setCategoryDraftName(e.target.value)}/><div className="modal-actions"><button onClick={saveNewCategory}>Save</button><button onClick={closeAddCategoryForm}>Cancel</button></div></div></div> : null}</div>);
}
