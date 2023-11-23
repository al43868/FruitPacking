using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward
{
    public abstract bool CanGet(Box box);
}
