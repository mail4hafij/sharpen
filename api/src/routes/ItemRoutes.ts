import { Express, Request, Response } from "express";
import { Pool } from "mysql2/promise";
import { MenuItem } from "../items/MenuItem";
import { ItemRepository } from "../items/ItemRepository";
export class ItemRoutes {
    public static register(app: Express, pool: Pool): void {
        app.get("/items", (req, res) => this.list(pool, req, res));
        app.post("/items", (req, res) => this.add(pool, req, res));
        app.put("/items/:id", (req, res) => this.edit(pool, req, res));
        app.delete("/items/:id", (req, res) => this.delete(pool, req, res));
    }
    public static async list(pool: Pool, _req: Request, res: Response): Promise<void> {
        let items = await ItemRepository.list(pool);
        res.json(items);
    }
    public static async add(pool: Pool, req: Request, res: Response): Promise<void> {
        let body = req.body;
        let id = await ItemRepository.add(pool, body.categoryId, body.name, body.description, body.price, body.imageUrl);
        let added: MenuItem = { id, categoryId: body.categoryId, name: body.name, description: body.description, price: body.price, imageUrl: body.imageUrl };
        res.json(added);
    }
    public static async edit(pool: Pool, req: Request, res: Response): Promise<void> {
        let id = Number(req.params.id);
        let body = req.body;
        let affected = await ItemRepository.edit(pool, id, body.categoryId, body.name, body.description, body.price, body.imageUrl);
        if (affected === 0) {
            res.status(404).end();
            return;
        }
        let updated: MenuItem = { id, categoryId: body.categoryId, name: body.name, description: body.description, price: body.price, imageUrl: body.imageUrl };
        res.json(updated);
    }
    public static async delete(pool: Pool, req: Request, res: Response): Promise<void> {
        let id = Number(req.params.id);
        let affected = await ItemRepository.delete(pool, id);
        if (affected === 0) {
            res.status(404).end();
            return;
        }
        res.status(204).end();
    }
}
