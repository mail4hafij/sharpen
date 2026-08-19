using express from "express";
using { Express } from "express";
using cors from "cors";
using { Database } from "./db/Database";
using { CategoryRoutes } from "./routes/CategoryRoutes";
using { ItemRoutes } from "./routes/ItemRoutes";

public async void Main()
{
    var pool = Database.Connect();
    Express app = express();
    app.use(cors());
    app.use(express.json());
    CategoryRoutes.Register(app, pool);
    ItemRoutes.Register(app, pool);
    app.listen(4000, () => console.log("API listening on http://localhost:4000"));
}
