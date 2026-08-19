# Restaurant App — Backend API

Node.js/Express/MySQL, written in C#-flavored syntax (`.csx`) and compiled by
Sharpen — see the repo root's `POC.html` for the language reference.

**Don't hand-edit the `.ts` files under `src/`.** They're generated. Edit the
matching `.csx` file and regenerate from `CSX Compiler/`:

```bash
cd "../CSX Compiler"
node src/cli.js --dir "../api/src"
```

## Run it

Requires MySQL running locally: `root` user, no password, `localhost:3306`, a
`restaurant` database (schema in `schema.sql`).

```bash
npm install     # one-time
npm run dev     # tsx watch - prints "API listening on http://localhost:4000"
npm run build   # real tsc build to plain .js, then runs it
```
