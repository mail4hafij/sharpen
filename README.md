# Sharpen — Write TypeScript and React Like C#

*Example project: a full-stack Restaurant App (React frontend + Node.js/Express/MySQL backend).*

## What is Sharpen?

Write your frontend and backend in C#-style syntax (`.csx` files). Sharpen compiles it into
real, plain, idiomatic TypeScript (`.tsx`/`.ts`) that you own outright — nothing downstream
depends on Sharpen itself: no runtime, no bundler plugin. It's built for teams who already
think in C# and want that fluency on a brand new TypeScript/React/Node project.

- Not compiled C# — output is plain TypeScript, nothing else runs.
- Not for existing codebases — new projects only, from day one.
- Not a runtime or bundler plugin — it just generates TypeScript files; your app never depends on it.

See **[the Sharpen docs](https://mail4hafij.github.io/sharpen/sharpen.html)** for the full
language reference — every syntax feature with a code snippet, both React and TypeScript.

## How to Use Sharpen

Install it globally:

```bash
npm install -g @mail4hafij/sharpen
```

Then compile a `.csx` file:

```bash
sharpen Foo.csx -o Foo.tsx   # a single file
sharpen --dir ./src          # every .csx file under a folder
```

Sharpen reads a `.csx` file once and writes a real `.tsx`/`.ts` file next to it —
nothing downstream ever touches `.csx` again; the generated file is what you actually
run, commit, and build.

## Editor Support

A VS Code extension adds syntax highlighting for `.csx` files:

```bash
code --install-extension mail4hafij.sharpen-csx
```

Or search "Sharpen" in VS Code's Extensions panel. Syntax highlighting only for
now — no IntelliSense or go-to-definition yet.

## The Restaurant App Example

This repo is a real, working app — categories and menu items for a restaurant — showing
Sharpen used on both ends of a real stack:

- `app/` — the React frontend (Vite + Zustand)
- `api/` — the Node.js/Express/MySQL backend, 8 REST endpoints

Both include the original `.csx` source alongside the compiled `.tsx`/`.ts` files
that actually run. You don't need Sharpen installed to run this app — but if you
install it, you can recompile any `.csx` file here yourself.

## Running it

Run both together to use the app for real — the frontend expects the API at
`http://localhost:4000`.

**Frontend**
```bash
cd app
npm install     # one-time
npm run dev     # starts the dev server, prints a URL (e.g. http://localhost:5173)
npm run build   # type-checks + production build
```

**Backend** — needs a MySQL database first, see [MySQL setup](#mysql-setup) below.
```bash
cd api
npm install     # one-time
npm run dev     # tsx watch - prints "API listening on http://localhost:4000"
npm run build   # real tsc build to plain .js, then runs it
```

## MySQL setup

The backend needs a local MySQL server.

1. Install MySQL if you don't already have it — [dev.mysql.com/downloads](https://dev.mysql.com/downloads/), or via your package manager (`brew install mysql`, `apt install mysql-server`, etc.).
2. Create a database named `restaurant`.
3. Load the schema:
   ```bash
   mysql -u root restaurant < api/schema.sql
   ```
4. By default, the backend connects as `root` with no password on `localhost:3306`
   (see `api/src/db/Database.ts`). If your setup uses different credentials, either
   edit `Database.ts` directly, or edit `Database.csx` and recompile it yourself:
   ```bash
   sharpen api/src/db/Database.csx -o api/src/db/Database.ts
   ```
   (needs Sharpen installed — see [How to Use Sharpen](#how-to-use-sharpen) above).

## About the `.csx` files

Every `.tsx`/`.ts` file has a matching `.csx` file next to it — the original source,
written like C#. The `.tsx`/`.ts` files are what actually run; the `.csx` files are
kept so you can see what produced them, and you can recompile any of them yourself
with Sharpen installed (`sharpen Foo.csx -o Foo.tsx`).

## How this demonstrates Sharpen

Every file under `app/src` and `api/src` started as a `.csx` file. A few specific places
worth opening if you want to see real syntax next to what it compiled to:

**Frontend**
- `stores/RestaurantStore.csx` — a `[Store]` holding both domain data (categories/items)
  *and* shared UI state, with real async actions (`await fetch(...)`, real response
  handling) — see `SaveNewCategory()`.
- `pages/Category/CategoryList.csx` — narrow `[Inject(Select = ...)]` selectors, so this
  component only re-renders when the specific store slice it reads actually changes.
- `pages/Category/CategoryAdd.csx` — `@if` in JSX markup, a modal driven entirely by
  shared store state instead of local component state.

**Backend**
- `db/Database.csx` — a static-method namespace (`Database.Connect()`), the pattern every
  backend class here follows since there are no constructors yet.
- `categories/CategoryRepository.csx` — real MySQL queries via `pool.query(...)`, and an
  `as` type assertion on the raw driver result (`result[0] as ResultSetHeader`).
- `routes/CategoryRoutes.csx` — registering real Express routes, checking the affected-row
  count before reporting success on Edit/Delete.
- `main.csx` — the `Main()` entry-point convention: a plain function literally named `Main`
  is auto-invoked at the bottom of the compiled file.

For what `[Store]`, `[Inject(Select = ...)]`, `@if`, and everything else actually compile
to, see **[the Sharpen docs](https://mail4hafij.github.io/sharpen/sharpen.html)**.
