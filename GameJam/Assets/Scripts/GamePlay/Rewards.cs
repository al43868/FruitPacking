using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards 
{
   public List<RewardObj> rewards;
}
public class RewardModel
{
    public int ID;
    public Reward reward;
    public int level;
    public RewardModel()
    {
        ID = 0;
    }
}

public class RewardObj
{
    public RewardModel model;
    public RewardObj(RewardModel model) 
    {
        this.model = model;
    }
}