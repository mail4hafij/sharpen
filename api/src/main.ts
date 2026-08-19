import express from "express";
import { Express } from "express";
import cors from "cors";
import { Database } from "./db/Database";
import { CategoryRoutes } from "./routes/CategoryRoutes";
import { ItemRoutes } from "./routes/ItemRoutes";
export async function main() {
    let pool = Database.connect();
    let app: Express = express();
    app.use(cors());
    app.use(express.json());
    CategoryRoutes.register(app, pool);
    ItemRoutes.register(app, pool);
    app.listen(4000, () => console.log("API listening on http://localhost:4000"));
}
main().catch(err => {
    console.error(err);
    process.exit(1);
});
