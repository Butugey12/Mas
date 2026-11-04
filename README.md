# 🐍 Mas — A Grid-Based Snake Game in MonoGame

A Snake-style game built from scratch in **C#** with **[MonoGame](https://monogame.net/)** on Linux.

This is a learning project. The goal is not just to end up with a working Snake game, but to properly understand the basics of game programming: the game loop, grid-based worlds, rendering, input, and timing. I'm building it step by step instead of copying a finished tutorial.

> Inspired by a WPF Snake tutorial on YouTube. I'm rebuilding the idea in MonoGame instead.

---

## 📸 Current State

<!-- TODO: add a screenshot or GIF here, e.g. ![gameplay](docs/gameplay.gif) -->

Right now the game has:

- ✅ A **1600 × 800** window split into a **40 × 20 grid** of 40px cells
- ✅ A **snake head** (aquamarine block) that moves one cell at a time with the arrow keys
- ✅ **Wall boundaries**: the snake can't move off the grid
- ✅ **Food** (red block) that appears in a random cell
- ✅ **Eating**: when the snake reaches the food, new food appears somewhere else
- ✅ A **150 ms movement tick**, so the snake moves at a steady speed instead of once per frame

---

## 🎮 Controls

| Key            | Action     |
|----------------|------------|
| `↑` `↓` `←` `→` | Move snake |
| `Esc`          | Quit       |

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (the version in `Mas/Mas.csproj`)
- MonoGame templates (only needed to create new projects, not to run this one):
  ```bash
  dotnet new install MonoGame.Templates.CSharp
  ```

### Run it

```bash
git clone https://github.com/Butugey12/Mas.git
cd Mas/Mas
dotnet tool restore   # restores the MonoGame content builder (mgcb)
dotnet run
```

---

## 🧠 How It Works

### 1. The game loop

MonoGame calls these methods for you:

```
Initialize()  → runs once: set window size, work out grid size, place snake & food
LoadContent() → runs once: create textures
┌──────────────────────────────┐
│ Update()  → game logic        │  ← repeats about 60 times per second
│ Draw()    → render the frame  │
└──────────────────────────────┘
```

`Update` decides **what is true** (where things are). `Draw` only **shows** what is true. Keeping them separate is one of the main ideas in game programming.

### 2. Grid vs. pixels

The game world is a grid of cells, not pixels. Every object stores its position as a **cell** `(x, y)` and is only turned into pixels when it's drawn:

```
pixelX = cellX * cellSize
pixelY = cellY * cellSize
```

```
  Columns = 1600 / 40 = 40
  Rows    =  800 / 40 = 20

  (0,0) (1,0) (2,0) ...         (39,0)
  (0,1)  ■                        ...
   ...         🍎
  (0,19)                       (39,19)
```

This makes the game logic simple. "Did the snake eat the food?" just means "are they in the same cell?"

### 3. The 1×1 texture trick

`SpriteBatch.Draw` can't make colours by itself. It always copies pixels from a texture. So the game creates a **1×1 pixel texture** in one colour and stretches it into a cell-sized rectangle. No image files are needed.

### 4. Movement on a timer

`Update` runs about every 16 ms, which is far too fast for Snake. The game adds up the elapsed time and only allows a move once **150 ms** have passed. This "tick" is what gives Snake its step-by-step feel.

---

## 🗺️ Roadmap

- [ ] **Continuous movement**: the snake keeps moving in its current direction (`dx`/`dy`)
- [ ] **Stop 180° turns**: you can't reverse straight into yourself
- [ ] **Growing tail**: store the body as a list or queue of cells
- [ ] **Self-collision**: hitting your own body ends the game
- [ ] **Wall collision = game over** (or wrap around the edges)
- [ ] **Food never spawns on the snake**
- [ ] **Score** shown on screen (`SpriteFont`)
- [ ] **Game over and restart** screen
- [ ] **Speed increases** as the snake grows
- [ ] Sprites and sounds instead of plain blocks

---

## 📁 Project Structure

```
.
├── Mas.sln
└── Mas/
    ├── .config/
    │   └── dotnet-tools.json # pins the MonoGame content builder tool
    ├── Content/
    │   └── Content.mgcb      # MonoGame content pipeline project (fonts, images, sounds)
    ├── Game1.cs              # the game: loop, input, logic, drawing
    ├── Sprite.cs
    ├── Program.cs            # entry point
    ├── Icon.ico / Icon.bmp   # window and app icon
    ├── app.manifest
    └── Mas.csproj
```

---

## 📚 Resources

- [MonoGame Documentation](https://docs.monogame.net/)
- [MonoGame "Building 2D Games" tutorial](https://docs.monogame.net/articles/tutorials/building_2d_games/)
- [`Game` class API](https://docs.monogame.net/api/Microsoft.Xna.Framework.Game.html)
- [`SpriteBatch` API](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteBatch.html)
- [Fix Your Timestep! (Glenn Fiedler)](https://gafferongames.com/post/fix_your_timestep/): a classic article on game-loop timing

---

## 📝 License

[MIT](LICENSE). Feel free to learn from it.
