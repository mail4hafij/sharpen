using { Pool, ResultSetHeader } from "mysql2/promise";

public class ItemRepository
{
    public static async Task<dynamic> List(Pool pool)
    {
        var result = await pool.query("SELECT id, category_id AS categoryId, name, description, price, image_url AS imageUrl FROM items");
        var rows = result[0];
        return rows;
    }

    public static async Task<int> Add(Pool pool, int categoryId, string name, string description, double price, string imageUrl)
    {
        var result = await pool.query(
            "INSERT INTO items (category_id, name, description, price, image_url) VALUES (?, ?, ?, ?, ?)",
            [categoryId, name, description, price, imageUrl]
        );
        var info = result[0] as ResultSetHeader;
        return info.insertId;
    }

    public static async Task<int> Edit(Pool pool, int id, int categoryId, string name, string description, double price, string imageUrl)
    {
        var result = await pool.query(
            "UPDATE items SET category_id = ?, name = ?, description = ?, price = ?, image_url = ? WHERE id = ?",
            [categoryId, name, description, price, imageUrl, id]
        );
        var info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }

    public static async Task<int> Delete(Pool pool, int id)
    {
        var result = await pool.query("DELETE FROM items WHERE id = ?", [id]);
        var info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }
}
