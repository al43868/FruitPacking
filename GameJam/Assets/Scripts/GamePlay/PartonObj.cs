using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartonObj 
{
    public PartonModel model;
    public List<RewardObj> rewards;
    public PartonObj(PartonModel parton)
    {
        model = parton;
        rewards=new();
        foreach (var item in model.rewards)
        {
            rewards.Add(new(item));
        }
    }
}
