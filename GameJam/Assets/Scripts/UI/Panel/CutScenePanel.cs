using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutScenePanel : BasePanel
{
    private static readonly string _name = "CutScenePanel";
    private static readonly string _path = "CutScenePanel";
    private static readonly UIType _type = new(_name, _path);

    public CutScenePanel() : base(_type)
    {

    }
    public override void OnStart()
    {
        base.OnStart();
    }
}
