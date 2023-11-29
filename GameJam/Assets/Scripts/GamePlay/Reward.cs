using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reward
{
    public abstract bool CanGet(Box box);
    public abstract string GetDes(Box box);
}
/// <summary>
/// 品种大于
/// </summary>
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
/// <summary>
/// 品种等于
/// </summary>
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
/// <summary>
/// 品种小于
/// </summary>
public class Reward3003 : Reward
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
            if (itemID.Count < count)
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
/// <summary>
/// 箱子填满
/// </summary>
public class Reward3004 : Reward
{
    public override bool CanGet(Box box)
    {
        foreach (var item in box.grids)
        {
            if (!item.Value.isSet)
            {
                return false;
            }
        }
        return true;
    }
    public override string GetDes(Box box)
    {
        return "";
    }
}
/// <summary>
/// 箱子一半
/// </summary>
public class Reward3005 : Reward
{
    public override bool CanGet(Box box)
    {
       int  all = 0;
       int have = 0;
        foreach (var item in box.grids)
        {
            all++;
            if (item.Value.isSet)
            {
                have++;
            }
        }
        if (have * 2 > all)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public override string GetDes(Box box)
    {
        return "";
    }
}
/// <summary>
/// 不要tag
/// </summary>
public class Reward3006 : Reward
{
    public ItemType type;
    public override bool CanGet(Box box)
    {
        foreach (var item in box.items)
        {
            if (!item.item.model.tags.Contains(type))
            {
                return true;
            }
        }
        return false;
    }
    public override string GetDes(Box box)
    {
        return GameManager.Instance.GetNameByID((int)type);
    }
}
/// <summary>
/// 想要品种
/// </summary>
public class Reward3007 : Reward
{
    public int itemID;
    public override bool CanGet(Box box)
    {
        foreach (var item in box.items)
        {
            if (item.item.model.bindID==itemID)
            {
                return true;
            }
        }
        return false;
    }
    public override string GetDes(Box box)
    {
        return GameManager.Instance.GetNameByID(itemID);
    }
}