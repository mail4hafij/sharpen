using mysql from "mysql2/promise";
using { Pool } from "mysql2/promise";

public record PoolConfig(string Host, string User, string Password, string Database, bool DecimalNumbers);

public class Database
{
    // decimalNumbers: true - mysql2 returns a DECIMAL column (like Item's
    // Price) as a string by default, to avoid float precision loss on the
    // driver's side. We want it as a real number to match Price's declared
    // `double` type, so opt in explicitly rather than parsing it back out by
    // hand at every call site.
    public static Pool Connect() => mysql.createPool(new PoolConfig("localhost", "root", "", "restaurant", true));
}
