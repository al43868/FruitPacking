using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardObj 
{
    public RewardModel model;
    public int endLevel;
    public RewardObj(RewardModel model)
    {
        this.model = model;
        endLevel = model.levels[Random.Range(0, model.levels.Count)];
    }
}
