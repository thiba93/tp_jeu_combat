# Diagrammes minimalistes CombatGame

Ces diagrammes sont volontairement simples. Le but est surtout de comprendre les grandes parties du projet sans montrer toutes les classes.

## Diagramme de classe minimaliste

```mermaid
classDiagram
    direction LR

    class Character {
        <<abstract>>
        Name
        Health
        Attack
        TakeDamage()
        Heal()
    }

    class Hero {
        Class
        SkillName
        HealsLeft
        CanUseSkill()
    }

    class Enemy

    class CombatSession {
        Hero
        Enemies
        CurrentState
        CompletePlayerTurn()
        PlayEnemyTurns()
    }

    class ICombatAction {
        <<interface>>
        Execute()
    }

    class BasicAttackAction
    class HealAction
    class WarriorSkillAction
    class MageSkillAction
    class RogueSkillAction

    class ICommand {
        <<interface>>
        Execute()
    }

    class AttackCommand
    class HealCommand
    class UseSkillCommand

    class ICombatState {
        <<interface>>
        Enter()
        Execute()
    }

    class PlayerTurnState
    class EnemyTurnState
    class BetweenWavesState
    class VictoryState
    class DefeatState

    class CombatEventPublisher
    class ICombatObserver
    class HeroFactory
    class EnemyFactory

    Character <|-- Hero
    Character <|-- Enemy

    CombatSession --> Hero
    CombatSession --> Enemy
    CombatSession --> ICombatState
    CombatSession --> CombatEventPublisher
    CombatSession ..> EnemyFactory

    ICombatAction <|.. BasicAttackAction
    ICombatAction <|.. HealAction
    ICombatAction <|.. WarriorSkillAction
    ICombatAction <|.. MageSkillAction
    ICombatAction <|.. RogueSkillAction

    ICommand <|.. AttackCommand
    ICommand <|.. HealCommand
    ICommand <|.. UseSkillCommand
    AttackCommand --> ICombatAction
    HealCommand --> ICombatAction
    UseSkillCommand --> ICombatAction

    ICombatState <|.. PlayerTurnState
    ICombatState <|.. EnemyTurnState
    ICombatState <|.. BetweenWavesState
    ICombatState <|.. VictoryState
    ICombatState <|.. DefeatState

    CombatEventPublisher --> ICombatObserver
    HeroFactory ..> Hero
    EnemyFactory ..> Enemy
```

## Diagramme de sequence minimaliste

```mermaid
sequenceDiagram
    actor Player
    participant Program
    participant Input as ConsoleInput
    participant HF as HeroFactory
    participant Session as CombatSession
    participant Invoker as ActionInvoker
    participant Cmd as ICommand
    participant Action as ICombatAction
    participant Pub as CombatEventPublisher

    Program->>Input: read hero name and class
    Input-->>Program: player choices
    Program->>HF: create hero
    HF-->>Program: hero
    Program->>Session: create combat session

    loop until victory or defeat
        Program->>Input: read menu choice
        Input-->>Program: choice
        Program->>Invoker: execute choice
        Invoker->>Cmd: Execute(session)
        Cmd->>Action: Execute(session)
        Action->>Session: damage or heal
        Action->>Pub: publish message
        Cmd->>Session: CompletePlayerTurn()

        alt enemies are alive
            Session->>Session: PlayEnemyTurns()
        else wave finished
            Session->>Session: prepare next wave or victory
        end
    end
```
