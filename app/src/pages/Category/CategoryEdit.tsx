import { useRestaurantStore } from "../../stores/RestaurantStore";
export function CategoryEdit() {
    const showEditCategoryForm: boolean = useRestaurantStore(s => s.showEditCategoryForm);
    const categoryDraftName: string = useRestaurantStore(s => s.categoryDraftName);
    const setCategoryDraftName: (arg0: string) => void = useRestaurantStore(s => s.setCategoryDraftName);
    const saveEditedCategory: () => void = useRestaurantStore(s => s.saveEditedCategory);
    const closeEditCategoryForm: () => void = useRestaurantStore(s => s.closeEditCategoryForm);
    return (<div>{showEditCategoryForm ? <div className="modal-overlay"><div className="modal"><h3>Edit Category</h3><label>Name</label><input value={categoryDraftName} onChange={e => setCategoryDraftName(e.target.value)}/><div className="modal-actions"><button onClick={saveEditedCategory}>Save</button><button onClick={closeEditCategoryForm}>Cancel</button></div></div></div> : null}</div>);
}
