using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RewardObj 
{
    public RewardModel model;
    public int endLevel;
    public RewardObj(RewardModel model)
    {
        this.model = model;
        endLevel = model.levels[0];
    }
}
