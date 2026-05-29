# CombatGame

Console turn based combat game in C#.

## Run

```bash
dotnet run --project src/CombatGame.Cli
```

## Tests

```bash
dotnet test
```

## Game rules

- The player chooses a hero name and a class.
- Classes: Warrior, Mage, Rogue.
- The fight has 3 waves.
- Attacks always target the first alive enemy.
- The hero can heal 2 times per combat.
- The hero wins after wave 3.
- The hero loses when HP reaches 0.

## Patterns

| Pattern | Classes / interfaces | Role in the game |
| ------- | -------------------- | ---------------- |
| Strategy | `ICombatAction`, `BasicAttackAction`, `HealAction`, `WarriorSkillAction`, `MageSkillAction`, `RogueSkillAction` | Each combat action has its own logic. |
| State | `ICombatState`, `PlayerTurnState`, `EnemyTurnState`, `BetweenWavesState`, `VictoryState`, `DefeatState` | The combat flow changes with the current state. |
| Factory | `HeroFactory`, `EnemyFactory`, `CombatActionFactory` | Creation of heroes, enemies and combat actions is grouped in factories. |
| Command | `ICommand`, `AttackCommand`, `UseSkillCommand`, `HealCommand`, `ShowJournalCommand`, `ActionInvoker` | Console choices are converted into commands. |
| Observer | `CombatEventPublisher`, `ICombatObserver`, `JournalCombatObserver`, `ConsoleCombatObserver` | Combat events are sent to the journal and console display. |

