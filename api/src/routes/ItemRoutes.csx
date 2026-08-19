using { Express, Request, Response } from "express";
using { Pool } from "mysql2/promise";
using { MenuItem } from "../items/MenuItem";
using { ItemRepository } from "../items/ItemRepository";

public class ItemRoutes
{
    public static void Register(Express app, Pool pool)
    {
        app.get("/items", (req, res) => List(pool, req, res));
        app.post("/items", (req, res) => Add(pool, req, res));
        app.put("/items/:id", (req, res) => Edit(pool, req, res));
        app.delete("/items/:id", (req, res) => Delete(pool, req, res));
    }

    public static async Task List(Pool pool, Request _req, Response res)
    {
        var items = await ItemRepository.List(pool);
        res.json(items);
    }

    public static async Task Add(Pool pool, Request req, Response res)
    {
        var body = req.body;
        var id = await ItemRepository.Add(pool, body.CategoryId, body.Name, body.Description, body.Price, body.ImageUrl);
        MenuItem added = new MenuItem(id, body.CategoryId, body.Name, body.Description, body.Price, body.ImageUrl);
        res.json(added);
    }

    public static async Task Edit(Pool pool, Request req, Response res)
    {
        var id = Number(req.params.Id);
        var body = req.body;
        var affected = await ItemRepository.Edit(pool, id, body.CategoryId, body.Name, body.Description, body.Price, body.ImageUrl);
        if (affected == 0)
        {
            res.status(404).end();
            return;
        }
        MenuItem updated = new MenuItem(id, body.CategoryId, body.Name, body.Description, body.Price, body.ImageUrl);
        res.json(updated);
    }

    public static async Task Delete(Pool pool, Request req, Response res)
    {
        var id = Number(req.params.Id);
        var affected = await ItemRepository.Delete(pool, id);
        if (affected == 0)
        {
            res.status(404).end();
            return;
        }
        res.status(204).end();
    }
}
