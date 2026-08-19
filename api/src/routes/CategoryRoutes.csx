using { Express, Request, Response } from "express";
using { Pool } from "mysql2/promise";
using { Category } from "../categories/Category";
using { CategoryRepository } from "../categories/CategoryRepository";

public class CategoryRoutes
{
    public static void Register(Express app, Pool pool)
    {
        app.get("/categories", (req, res) => List(pool, req, res));
        app.post("/categories", (req, res) => Add(pool, req, res));
        app.put("/categories/:id", (req, res) => Edit(pool, req, res));
        app.delete("/categories/:id", (req, res) => Delete(pool, req, res));
    }

    public static async Task List(Pool pool, Request _req, Response res)
    {
        var categories = await CategoryRepository.List(pool);
        res.json(categories);
    }

    public static async Task Add(Pool pool, Request req, Response res)
    {
        var body = req.body;
        var id = await CategoryRepository.Add(pool, body.Name, body.ImageUrl);
        Category added = new Category(id, body.Name, body.ImageUrl);
        res.json(added);
    }

    public static async Task Edit(Pool pool, Request req, Response res)
    {
        var id = Number(req.params.Id);
        var body = req.body;
        var affected = await CategoryRepository.Edit(pool, id, body.Name, body.ImageUrl);
        if (affected == 0)
        {
            res.status(404).end();
            return;
        }
        Category updated = new Category(id, body.Name, body.ImageUrl);
        res.json(updated);
    }

    public static async Task Delete(Pool pool, Request req, Response res)
    {
        var id = Number(req.params.Id);
        var affected = await CategoryRepository.Delete(pool, id);
        if (affected == 0)
        {
            res.status(404).end();
            return;
        }
        res.status(204).end();
    }
}
