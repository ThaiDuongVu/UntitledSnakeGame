# Untitled Snake Game 🐍

Multiplayer snake game of up to 10 players.

---

Engine: Unity.
Language: C#.
Platform: PC 💻 (Windows, Mac, Linux), Mobile 📱 (Android, iOS).

#### Description:

*Food* 🍕 objects will be spawned at random positions on the field.
Each player will control a snake 🐍 to move around.

Each snake has 3 attributes: `length`, `score` & `score multiplier`.
When a snake eats a piece of food:
- `length` += 1
- `score multiplier` += 1
- `score` += `score multiplier` * 1

When a snake's head collides with an obstacle (itself, another snake, etc.):
- `length` is reset
- `score multiplier` is reset to 1
- `score` stays the same

Any snake that reach 100 score first will win the round.

---

#### Requirements:

Set username.
Change username.
Create a new lobby.
Join an existing lobby.
Start game when enough people have joined.
Snake 🐍 movement.
Snake eat food 🍕 objects.
Leaderboard to show each snake's current score.
Win (or lose) a round.
Start a new round.
