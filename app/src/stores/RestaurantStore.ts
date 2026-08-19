import { create } from "zustand";
export interface RequestOptions {
    method: string;
    headers: Headers;
    body: string;
}
export interface Category {
    id: number;
    name: string;
    imageUrl: string;
}
export interface MenuItem {
    id: number;
    categoryId: number;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
}
export interface NewCategoryBody {
    name: string;
    imageUrl: string;
}
export interface NewItemBody {
    categoryId: number;
    name: string;
    description: string;
    price: number;
    imageUrl: string;
}
export const useRestaurantStore = create<{
    categories: Category[];
    items: MenuItem[];
    selectedCategoryId: number;
    showAddCategoryForm: boolean;
    showEditCategoryForm: boolean;
    editingCategoryId: number;
    categoryDraftName: string;
    showAddItemForm: boolean;
    showEditItemForm: boolean;
    editingItemId: number;
    itemDraftName: string;
    itemDraftDescription: string;
    itemDraftPrice: string;
    loadCategories: () => Promise<void>;
    loadItems: () => Promise<void>;
    selectCategory: (id: number) => void;
    openAddCategoryForm: () => void;
    closeAddCategoryForm: () => void;
    setCategoryDraftName: (name: string) => void;
    saveNewCategory: () => Promise<void>;
    openEditCategoryForm: (id: number, name: string) => void;
    closeEditCategoryForm: () => void;
    saveEditedCategory: () => Promise<void>;
    deleteCategory: (id: number) => Promise<void>;
    openAddItemForm: () => void;
    closeAddItemForm: () => void;
    setItemDraftName: (name: string) => void;
    setItemDraftDescription: (description: string) => void;
    setItemDraftPrice: (price: string) => void;
    saveNewItem: () => Promise<void>;
    openEditItemForm: (id: number, name: string, description: string, price: number) => void;
    closeEditItemForm: () => void;
    saveEditedItem: () => Promise<void>;
    deleteItem: (id: number) => Promise<void>;
}>((set, get) => ({
    categories: [],
    items: [],
    selectedCategoryId: 0,
    showAddCategoryForm: false,
    showEditCategoryForm: false,
    editingCategoryId: 0,
    categoryDraftName: "",
    showAddItemForm: false,
    showEditItemForm: false,
    editingItemId: 0,
    itemDraftName: "",
    itemDraftDescription: "",
    itemDraftPrice: "",
    loadCategories: async () => {
        let response = await fetch(apiUrl("/categories"));
        let data = await response.json();
        set({ categories: data });
        if (get().categories.length > 0) {
            set(s => ({ selectedCategoryId: s.categories[0].id }));
        }
    },
    loadItems: async () => {
        let response = await fetch(apiUrl("/items"));
        let data = await response.json();
        set({ items: data });
    },
    selectCategory: (id: number) => set({ selectedCategoryId: id }),
    openAddCategoryForm: () => {
        set({ categoryDraftName: "" });
        set({ showAddCategoryForm: true });
    },
    closeAddCategoryForm: () => set({ showAddCategoryForm: false }),
    setCategoryDraftName: (name: string) => set({ categoryDraftName: name }),
    saveNewCategory: async () => {
        let body = JSON.stringify({ name: get().categoryDraftName, imageUrl: "/images/category-placeholder.svg" });
        let response = await fetch(apiUrl("/categories"), jsonRequest("POST", body));
        let created = await response.json();
        set(s => ({ categories: [...s.categories, created] }));
        set({ selectedCategoryId: created.id });
        set({ showAddCategoryForm: false });
    },
    openEditCategoryForm: (id: number, name: string) => {
        set({ editingCategoryId: id });
        set({ categoryDraftName: name });
        set({ showEditCategoryForm: true });
    },
    closeEditCategoryForm: () => set({ showEditCategoryForm: false }),
    saveEditedCategory: async () => {
        let existing = get().categories.filter(c => c.id === get().editingCategoryId)[0];
        let body = JSON.stringify({ name: get().categoryDraftName, imageUrl: existing.imageUrl });
        let response = await fetch(apiUrl("/categories/" + get().editingCategoryId), jsonRequest("PUT", body));
        let updated = await response.json();
        set(s => ({ categories: s.categories.map(c => c.id === s.editingCategoryId ? updated : c) }));
        set({ showEditCategoryForm: false });
    },
    deleteCategory: async (id: number) => {
        await fetch(apiUrl("/categories/" + id), jsonRequest("DELETE", ""));
        set(s => ({ categories: s.categories.filter(c => c.id !== id) }));
        set(s => ({ items: s.items.filter(i => i.categoryId !== id) }));
    },
    openAddItemForm: () => {
        set({ itemDraftName: "" });
        set({ itemDraftDescription: "" });
        set({ itemDraftPrice: "" });
        set({ showAddItemForm: true });
    },
    closeAddItemForm: () => set({ showAddItemForm: false }),
    setItemDraftName: (name: string) => set({ itemDraftName: name }),
    setItemDraftDescription: (description: string) => set({ itemDraftDescription: description }),
    setItemDraftPrice: (price: string) => set({ itemDraftPrice: price }),
    saveNewItem: async () => {
        let body = JSON.stringify({ categoryId: get().selectedCategoryId, name: get().itemDraftName, description: get().itemDraftDescription, price: Number(get().itemDraftPrice), imageUrl: "/images/item-placeholder.svg" });
        let response = await fetch(apiUrl("/items"), jsonRequest("POST", body));
        let created = await response.json();
        set(s => ({ items: [...s.items, created] }));
        set({ showAddItemForm: false });
    },
    openEditItemForm: (id: number, name: string, description: string, price: number) => {
        set({ editingItemId: id });
        set({ itemDraftName: name });
        set({ itemDraftDescription: description });
        set({ itemDraftPrice: price.toString() });
        set({ showEditItemForm: true });
    },
    closeEditItemForm: () => set({ showEditItemForm: false }),
    saveEditedItem: async () => {
        let existing = get().items.filter(i => i.id === get().editingItemId)[0];
        let body = JSON.stringify({ categoryId: existing.categoryId, name: get().itemDraftName, description: get().itemDraftDescription, price: Number(get().itemDraftPrice), imageUrl: existing.imageUrl });
        let response = await fetch(apiUrl("/items/" + get().editingItemId), jsonRequest("PUT", body));
        let updated = await response.json();
        set(s => ({ items: s.items.map(i => i.id === s.editingItemId ? updated : i) }));
        set({ showEditItemForm: false });
    },
    deleteItem: async (id: number) => {
        await fetch(apiUrl("/items/" + id), jsonRequest("DELETE", ""));
        set(s => ({ items: s.items.filter(i => i.id !== id) }));
    }
}));
export function apiUrl(path: string) {
    return "http://localhost:4000" + path;
}
export function jsonRequest(method: string, body: string): RequestOptions {
    let headers = new Headers();
    headers.append("Content-Type", "application/json");
    return { method, headers, body };
}
