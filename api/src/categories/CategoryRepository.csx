using { Pool } from "mysql2/promise";
using { ResultSetHeader } from "mysql2/promise";

public class CategoryRepository
{
    public static async Task<dynamic> List(Pool pool)
    {
        var result = await pool.query("SELECT id, name, image_url AS imageUrl FROM categories");
        var rows = result[0];
        return rows;
    }

    public static async Task<int> Add(Pool pool, string name, string imageUrl)
    {
        var result = await pool.query("INSERT INTO categories (name, image_url) VALUES (?, ?)", [name, imageUrl]);
        var info = result[0] as ResultSetHeader;
        return info.insertId;
    }

    // Returns the affected-row count, not just void - the caller needs it to
    // tell "updated a real row" apart from "id didn't match anything" (a real
    // gap caught by actually testing this against the database: editing a
    // non-existent id was silently returning a success-looking response).
    public static async Task<int> Edit(Pool pool, int id, string name, string imageUrl)
    {
        var result = await pool.query("UPDATE categories SET name = ?, image_url = ? WHERE id = ?", [name, imageUrl, id]);
        var info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }

    public static async Task<int> Delete(Pool pool, int id)
    {
        var result = await pool.query("DELETE FROM categories WHERE id = ?", [id]);
        var info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }
}
