using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartonModel", menuName = "SO/PartonModel")]
public class PartonModel : SerializedScriptableObject
{
    public int ID;
    public List<RewardModel> rewards;
    public Box box;
    public PartonModel()
    {
        ID = 5001;
        rewards = new ();
    }
}
