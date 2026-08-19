# Restaurant App — Frontend

React + TypeScript (Vite + Zustand), written in C#-flavored syntax (`.csx`) and
compiled by Sharpen — see the repo root's `POC.html` for the language reference.

**Don't hand-edit the `.tsx`/`.ts` files under `src/`.** They're generated. Edit the
matching `.csx` file and regenerate from `CSX Compiler/`:

```bash
cd "../CSX Compiler"
node src/cli.js --dir "../app/src"
```

## Run it

```bash
npm install     # one-time
npm run dev     # starts the dev server, prints a URL (e.g. http://localhost:5173)
npm run build   # type-checks + production build
```

Expects the API running at `http://localhost:4000` — see `../api/README.md`.
