[Component]
public class App
{
    [Inject(Select = nameof(RestaurantStore.LoadCategories))] private Action loadCategories;
    [Inject(Select = nameof(RestaurantStore.LoadItems))] private Action loadItems;

    [Effect]
    private void LoadInitialData()
    {
        loadCategories();
        loadItems();
    }

    public JSX Render() =>
        <div className="restaurant-app">
            <header className="hero">
                <h1>My Kitchen</h1>
                <p>A simple menu, built one category at a time.</p>
            </header>
            <CategoryList />
            <ItemList />
        </div>;
}
