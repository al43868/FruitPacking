using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward
{
    public abstract bool CanGet(Box box);
    public abstract string GetDes(Box box);
}
public class Reward3001 : Reward
{
    public int count;
    public override bool CanGet(Box box)
    {
        if (box.items.Count > 0)
        {
            List<int> itemID = new();
            foreach (var item in box.items)
            {
                if (!itemID.Contains(item.item.model.ID))
                {
                    itemID.Add(item.item.model.ID);
                }
            }
            if (itemID.Count >= count)
            {
                return true;
            }
        }
        return false;
    }

    public override string GetDes(Box box)
    {
        return count.ToString();
    }
}
public class Reward3002 : Reward
{
    public int count;
    public override bool CanGet(Box box)
    {
        if (box.items.Count > 0)
        {
            List<int> itemID = new();
            foreach (var item in box.items)
            {
                if (!itemID.Contains(item.item.model.ID))
                {
                    itemID.Add(item.item.model.ID);
                }
            }
            if (itemID.Count == count)
            {
                return true;
            }
        }
        return false;
    }

    public override string GetDes(Box box)
    {
        return count.ToString();
    }
}