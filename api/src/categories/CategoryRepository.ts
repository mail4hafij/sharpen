import { Pool } from "mysql2/promise";
import { ResultSetHeader } from "mysql2/promise";
export class CategoryRepository {
    public static async list(pool: Pool): Promise<any> {
        let result = await pool.query("SELECT id, name, image_url AS imageUrl FROM categories");
        let rows = result[0];
        return rows;
    }
    public static async add(pool: Pool, name: string, imageUrl: string): Promise<number> {
        let result = await pool.query("INSERT INTO categories (name, image_url) VALUES (?, ?)", [name, imageUrl]);
        let info = result[0] as ResultSetHeader;
        return info.insertId;
    }
    public static async edit(pool: Pool, id: number, name: string, imageUrl: string): Promise<number> {
        let result = await pool.query("UPDATE categories SET name = ?, image_url = ? WHERE id = ?", [name, imageUrl, id]);
        let info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }
    public static async delete(pool: Pool, id: number): Promise<number> {
        let result = await pool.query("DELETE FROM categories WHERE id = ?", [id]);
        let info = result[0] as ResultSetHeader;
        return info.affectedRows;
    }
}
