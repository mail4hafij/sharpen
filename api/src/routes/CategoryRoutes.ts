import { Express, Request, Response } from "express";
import { Pool } from "mysql2/promise";
import { Category } from "../categories/Category";
import { CategoryRepository } from "../categories/CategoryRepository";
export class CategoryRoutes {
    public static register(app: Express, pool: Pool): void {
        app.get("/categories", (req, res) => this.list(pool, req, res));
        app.post("/categories", (req, res) => this.add(pool, req, res));
        app.put("/categories/:id", (req, res) => this.edit(pool, req, res));
        app.delete("/categories/:id", (req, res) => this.delete(pool, req, res));
    }
    public static async list(pool: Pool, _req: Request, res: Response): Promise<void> {
        let categories = await CategoryRepository.list(pool);
        res.json(categories);
    }
    public static async add(pool: Pool, req: Request, res: Response): Promise<void> {
        let body = req.body;
        let id = await CategoryRepository.add(pool, body.name, body.imageUrl);
        let added: Category = { id, name: body.name, imageUrl: body.imageUrl };
        res.json(added);
    }
    public static async edit(pool: Pool, req: Request, res: Response): Promise<void> {
        let id = Number(req.params.id);
        let body = req.body;
        let affected = await CategoryRepository.edit(pool, id, body.name, body.imageUrl);
        if (affected === 0) {
            res.status(404).end();
            return;
        }
        let updated: Category = { id, name: body.name, imageUrl: body.imageUrl };
        res.json(updated);
    }
    public static async delete(pool: Pool, req: Request, res: Response): Promise<void> {
        let id = Number(req.params.id);
        let affected = await CategoryRepository.delete(pool, id);
        if (affected === 0) {
            res.status(404).end();
            return;
        }
        res.status(204).end();
    }
}
