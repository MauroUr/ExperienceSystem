# Experience System

This is a simple system which contains an **Experience** class. It tracks the player's experience (XP), handles level progression, and triggers callbacks for significant events.

## Callback events

**OnLevelUp** is raised every time the player levels up.

**OnMaxLevelReached** is raised when the player reaches the maximum level.

## Testing

This system is fully unit tested to ensure reliability and avoid bugs. You can trust that the experience system is stable and functions as expected.

## Some clarifications

### Level Progression

The system calculates the required XP for each level using a simple formula:

XP Required = 1000 * Current Level

This can be modified for more advanced leveling curves.

### Max Level Handling

If a maximum level is defined and reached, additional XP is discarded, and OnMaxLevelReached is triggered.

### Resetting Progress

ResetXP() resets the player's XP to 0 without affecting the level.

ResetProgression(newLevel) resets the player's XP and level to a specified value.

### Forks and collaborations

Feel free to fork this project and open a Pull Request if you have improvements or new features. The only requirement for merging is that all current tests pass and that new features include adequate unit tests.
