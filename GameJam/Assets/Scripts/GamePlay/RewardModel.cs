using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardModel", menuName = "SO/RewardModel")]
public class RewardModel : SerializedScriptableObject
{
    public int ID;
    public Reward reward;
    public List<int> levels;
    public RewardModel()
    {
        ID = 3001;
        levels = new ();
    }
}
