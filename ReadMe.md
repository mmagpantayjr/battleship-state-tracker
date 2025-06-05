# BattleshipStateTracker

A C# application to simulate and track the state of a Battleship game, including board setup, ship placement, and attack processing. Designed with a clean architecture and testability in mind.

---

## Features

* **Battleship creation and positioning**
  Place ships horizontally or vertically with randomized valid starting and ending positions.

* **Board service management**
  Handles board setup, battle start, attack processing, and win/loss detection.

* **Attack handling**
  Validate attack positions, detect hits/misses, track attacked positions.

* **State tracking**
  Maintains ship positions and whether ships have sunk.

* **Extensive unit testing**
  Covers core functionalities including ship creation, attack logic, and board validation.

---

## Architecture

* **Models**
  `Position`, `Battleship`, and related domain entities with validation and equality overrides.

* **Services**
  `BoardService` responsible for game logic and managing Battleship instances.

* **Unit Tests**
  Uses xUnit with fixtures and reflection to test internal logic like private methods and state fields.

---

## Getting Started

### Prerequisites

* [.NET 8+ SDK](https://dotnet.microsoft.com/download)
* IDE like Visual Studio or VS Code

### Building the project

```bash
dotnet build
```

### Running unit tests

```bash
dotnet test
```

---

## Usage

1. **Create a battleship on the board** with randomized positions within valid bounds.
2. **Start the battle**: enter attack positions (row and column) via console input.
3. The game will process attacks and output results:

   * "A direct hit!" when an attack hits a ship.
   * "Attack missed." when it misses.
4. Game continues until all ships have sunk.
5. Displays "Game over! You Win!" when all ships are sunk.

---

## Testing Approach

* Uses **xUnit** for unit tests.
* **Reflection** is used in tests to access and modify private fields and methods for comprehensive coverage.
* `BoardServiceFixture` provides reusable setup for tests involving `BoardService`.
* Tests cover scenarios including:

  * Battleship creation and invalid positions.
  * Attacking ships with hits, misses, and repeated attacks.
  * Validation of board boundaries and overlapping ships.
  
---

## License

MIT License