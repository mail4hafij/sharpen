// Small same-file helpers the store's actions below call into - plain
// top-level items living alongside the [Store] in one file, same as any other
// .csx file that mixes multiple top-level declarations.
public string ApiUrl(string path)
{
    return "http://localhost:4000" + path;
}

// A fetch RequestInit-shaped record. `Headers` is the real Fetch API type
// (globally available via the DOM lib) - used instead of a plain object
// literal because "Content-Type" isn't a valid identifier for a record field,
// so it can't be expressed as one of our usual record-based object literals.
public record RequestOptions(string Method, Headers Headers, string Body);

public RequestOptions JsonRequest(string method, string body)
{
    var headers = new Headers();
    headers.append("Content-Type", "application/json");
    return new RequestOptions(method, headers, body);
}

[Store]
public class RestaurantStore
{
    public record Category(int Id, string Name, string ImageUrl);
    public record MenuItem(int Id, int CategoryId, string Name, string Description, double Price, string ImageUrl);
    public record NewCategoryBody(string Name, string ImageUrl);
    public record NewItemBody(int CategoryId, string Name, string Description, double Price, string ImageUrl);

    [Observable] public List<Category> Categories { get; set; } = [];
    [Observable] public List<MenuItem> Items { get; set; } = [];
    [Observable] public int SelectedCategoryId { get; set; } = 0;

    // Category form UI state - kept here (not in a component's local [State])
    // specifically so List/Add/Edit can each live in their own file and still
    // share it, without needing to pass a Category object across files as a prop.
    [Observable] public bool ShowAddCategoryForm { get; set; } = false;
    [Observable] public bool ShowEditCategoryForm { get; set; } = false;
    [Observable] public int EditingCategoryId { get; set; } = 0;
    [Observable] public string CategoryDraftName { get; set; } = "";

    [Observable] public bool ShowAddItemForm { get; set; } = false;
    [Observable] public bool ShowEditItemForm { get; set; } = false;
    [Observable] public int EditingItemId { get; set; } = 0;
    [Observable] public string ItemDraftName { get; set; } = "";
    [Observable] public string ItemDraftDescription { get; set; } = "";
    [Observable] public string ItemDraftPrice { get; set; } = "";

    public async Task LoadCategories()
    {
        var response = await fetch(ApiUrl("/categories"));
        var data = await response.json();
        Categories = data;
        if (Categories.Count > 0)
        {
            SelectedCategoryId = Categories[0].Id;
        }
    }

    public async Task LoadItems()
    {
        var response = await fetch(ApiUrl("/items"));
        var data = await response.json();
        Items = data;
    }

    public void SelectCategory(int id) => SelectedCategoryId = id;

    public void OpenAddCategoryForm()
    {
        CategoryDraftName = "";
        ShowAddCategoryForm = true;
    }

    public void CloseAddCategoryForm() => ShowAddCategoryForm = false;

    public void SetCategoryDraftName(string name) => CategoryDraftName = name;

    public async Task SaveNewCategory()
    {
        var body = JSON.stringify(new NewCategoryBody(CategoryDraftName, "/images/category-placeholder.svg"));
        var response = await fetch(ApiUrl("/categories"), JsonRequest("POST", body));
        var created = await response.json();
        Categories = [.. Categories, created];
        SelectedCategoryId = created.Id;
        ShowAddCategoryForm = false;
    }

    public void OpenEditCategoryForm(int id, string name)
    {
        EditingCategoryId = id;
        CategoryDraftName = name;
        ShowEditCategoryForm = true;
    }

    public void CloseEditCategoryForm() => ShowEditCategoryForm = false;

    public async Task SaveEditedCategory()
    {
        var existing = Categories.Where(c => c.Id == EditingCategoryId).ToList()[0];
        var body = JSON.stringify(new NewCategoryBody(CategoryDraftName, existing.ImageUrl));
        var response = await fetch(ApiUrl("/categories/" + EditingCategoryId), JsonRequest("PUT", body));
        var updated = await response.json();
        Categories = Categories.Select(c => c.Id == EditingCategoryId ? updated : c).ToList();
        ShowEditCategoryForm = false;
    }

    public async Task DeleteCategory(int id)
    {
        await fetch(ApiUrl("/categories/" + id), JsonRequest("DELETE", ""));
        Categories = Categories.Where(c => c.Id != id).ToList();
        Items = Items.Where(i => i.CategoryId != id).ToList();
    }

    public void OpenAddItemForm()
    {
        ItemDraftName = "";
        ItemDraftDescription = "";
        ItemDraftPrice = "";
        ShowAddItemForm = true;
    }

    public void CloseAddItemForm() => ShowAddItemForm = false;

    public void SetItemDraftName(string name) => ItemDraftName = name;
    public void SetItemDraftDescription(string description) => ItemDraftDescription = description;
    public void SetItemDraftPrice(string price) => ItemDraftPrice = price;

    public async Task SaveNewItem()
    {
        var body = JSON.stringify(new NewItemBody(SelectedCategoryId, ItemDraftName, ItemDraftDescription, Number(ItemDraftPrice), "/images/item-placeholder.svg"));
        var response = await fetch(ApiUrl("/items"), JsonRequest("POST", body));
        var created = await response.json();
        Items = [.. Items, created];
        ShowAddItemForm = false;
    }

    public void OpenEditItemForm(int id, string name, string description, double price)
    {
        EditingItemId = id;
        ItemDraftName = name;
        ItemDraftDescription = description;
        ItemDraftPrice = price.ToString();
        ShowEditItemForm = true;
    }

    public void CloseEditItemForm() => ShowEditItemForm = false;

    public async Task SaveEditedItem()
    {
        var existing = Items.Where(i => i.Id == EditingItemId).ToList()[0];
        var body = JSON.stringify(new NewItemBody(existing.CategoryId, ItemDraftName, ItemDraftDescription, Number(ItemDraftPrice), existing.ImageUrl));
        var response = await fetch(ApiUrl("/items/" + EditingItemId), JsonRequest("PUT", body));
        var updated = await response.json();
        Items = Items.Select(i => i.Id == EditingItemId ? updated : i).ToList();
        ShowEditItemForm = false;
    }

    public async Task DeleteItem(int id)
    {
        await fetch(ApiUrl("/items/" + id), JsonRequest("DELETE", ""));
        Items = Items.Where(i => i.Id != id).ToList();
    }
}
