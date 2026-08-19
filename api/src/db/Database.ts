import mysql from "mysql2/promise";
import { Pool } from "mysql2/promise";
export interface PoolConfig {
    host: string;
    user: string;
    password: string;
    database: string;
    decimalNumbers: boolean;
}
export class Database {
    public static connect(): Pool {
        return mysql.createPool({ host: "localhost", user: "root", password: "", database: "restaurant", decimalNumbers: true });
    }
}
