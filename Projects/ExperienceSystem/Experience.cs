using System;
using UnityEngine;

public delegate void LevelUpEvent();

[Serializable]
public class Experience
{
    public event LevelUpEvent OnLevelUp = delegate { };
    public event Action OnMaxLevelReached = delegate { };

    /// <summary>
    /// Max level allowed (0 = No limit)
    /// </summary>
    public int maxLevel { get; set; }

    /// <summary>
    /// Current player experience
    /// </summary>
    public float exp
    {
        get => _exp;
        private set
        {
            float oldXP = _exp;
            _exp = Mathf.Max(0, value);
        }
    }

    /// <summary>
    /// Current player level
    /// </summary>
    public int Level { get; private set; }

    private const int EXP_NEEDED_PER_LEVEL = 1000;
    private float _exp;

    public Experience(int startLevel = 1, int maxLevel = 0)
    {
        Level = startLevel;
        this.maxLevel = maxLevel;
        exp = 0;
    }

    /// <summary>
    /// Adds experience and handles level ups
    /// </summary>
    /// <param name="amount">Amount of experience to add</param>
    public void AddXP(float amount)
    {
        if (amount < 0)
            throw new ArgumentException("XP amount cannot be negative!");

        float oldXP = exp;
        exp += amount;

        while (exp >= GetXPRequiredForLevel(Level))
        {
            if (maxLevel > 0 && Level >= maxLevel)
            {
                exp = 0;
                OnMaxLevelReached.Invoke();
                return;
            }

            exp -= GetXPRequiredForLevel(Level);
            Level++;
            OnLevelUp.Invoke();
        }
    }

    /// <summary>
    /// Returns experience needed for next level
    /// </summary>
    private float GetXPRequiredForLevel(int level)
    {
        return EXP_NEEDED_PER_LEVEL * level; // Puedes cambiar esto para usar una fórmula más avanzada
    }

    /// <summary>
    /// Restarts experience to 0 and mantains current level
    /// </summary>
    public void ResetXP()
    {
        exp = 0;
    }

    /// <summary>
    /// Restarts progression to a base level
    /// </summary>
    public void ResetProgression(int newLevel = 1)
    {
        Level = newLevel;
        exp = 0;
    }
}

