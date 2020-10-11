using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Parameter
{
    public int Level { get; set; }

    public int MaxLevel { get; set; }

    public bool IsUpgradable { get { return Level < MaxLevel; } }

    public Parameter(int maxLevel, int currentLevel)
    {
        MaxLevel = maxLevel;
        Level = currentLevel;
    }
    
    public virtual void Upgrade()
    {
        if (IsUpgradable)
            Level += 1;
    }

    public abstract double UpgradeCost();
    public abstract double ValueDouble();
    public abstract int ValueInt();

}
