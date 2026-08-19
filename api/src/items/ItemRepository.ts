import { Pool, ResultSetHeader } from "mysql2/promise";
export class ItemRepository {
    public static async list(pool: Pool): Promise<any> {
        let result = await pool.query("SELECT id, category_id AS categoryId, name, description, price, image_url AS imageUrl FROM items");
        let rows = result[0];
        return rows;
    }
    public static async add(pool: Pool, categoryId: number, name: string, description: string, price: number, imageUrl: string): Promise<number> {
        let result = await pool.query("INSERT INTO items (category_id, name, description, price, image_url) VALUES (?, ?, ?, ?, ?)", [categoryId, name, description, price, imageUrl]);
        let info = result[0] as ResultSetHeader;
        return info.insertId;
    }
    public static async edit(pool: Pool, id: number, categoryId: number, name: string, description: string, price: number, imageUrl: string): Promise<number> {
        let result = await pool.query("UPDATE items SET category_id = ?, name = ?, description = ?, price = ?, image_url = ? WHERE id = ?", [categoryId, name, description, price, imageUrl, id]);
        let info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }
    public static async delete(pool: Pool, id: number): Promise<number> {
        let result = await pool.query("DELETE FROM items WHERE id = ?", [id]);
        let info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }
}
