# Restaurant App — Built with Sharpen

A small full-stack example — a React frontend and a Node.js/Express/MySQL backend —
written in C#-flavored syntax (`.csx`) and compiled into plain, idiomatic TypeScript
by **Sharpen**.

See [`sharpen.html`](./sharpen.html) for the full idea: what Sharpen is, why you'd
use it, the complete language reference, and more.

## What's in this repo

- `app/` — the React frontend (Vite + Zustand)
- `api/` — the Node.js/Express/MySQL backend

Both include the original `.csx` source alongside the compiled `.tsx`/`.ts` files
that actually run. The Sharpen compiler itself isn't included in this repo — the
`.csx` files are here for reference, but this repo ships the compiled output as a
runnable app on its own.

## Running it

**Frontend**
```bash
cd app
npm install
npm run dev
```
See [`app/README.md`](./app/README.md) for more.

**Backend**
```bash
cd api
npm install
npm run dev
```
See [`api/README.md`](./api/README.md) for more. Requires a MySQL database — see below.

## MySQL setup

The backend needs a local MySQL server.

1. Install MySQL if you don't already have it — [dev.mysql.com/downloads](https://dev.mysql.com/downloads/), or via your package manager (`brew install mysql`, `apt install mysql-server`, etc.).
2. Create a database named `restaurant`.
3. Load the schema:
   ```bash
   mysql -u root restaurant < api/schema.sql
   ```
4. By default, the backend connects as `root` with no password on `localhost:3306`
   (see `api/src/db/Database.ts`). If your setup uses different credentials, edit
   that file directly — since the compiler isn't part of this repo,
   `api/src/db/Database.csx` here is reference-only and can't be recompiled from it.

## About the `.csx` files

Every `.tsx`/`.ts` file has a matching `.csx` file next to it — that's the original
source, written in C#-flavored syntax. It's kept here so you can see what generated
the TypeScript you're actually running. Since this repo doesn't include the Sharpen
compiler, the `.csx` files aren't build inputs here — the `.tsx`/`.ts` files are what
Node/Vite actually use.
