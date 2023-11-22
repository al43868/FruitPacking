using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class UIManager
{
    public Stack<BasePanel> panelStack;
    public Dictionary<string, GameObject> panelDictionary;
    public GameObject canvas;
    public UIManager()
    {
        //_instance = this;
        panelStack = new Stack<BasePanel>();
        panelDictionary = new Dictionary<string, GameObject>();
    }
    public void Push(BasePanel basePanel)
    {
        if (panelStack.Count == 0)
        {
            panelStack.Push(basePanel);
        }
        else if (panelStack.Peek().uiType.Name == basePanel.uiType.Name) 
            return;
        else 
        {
            panelStack.Peek().OnDisable();
            panelStack.Push(basePanel);
        }
        GameObject go = GetSingleObj(basePanel.uiType);
        panelDictionary.Add(basePanel.uiType.Name, go);
        basePanel.panelObj = go;
        basePanel.OnStart();
    }
    public void Pop()
    {
        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnDisable();
            panelStack.Peek().OnDestory();
            GameObject.Destroy(panelDictionary[panelStack.Peek().uiType.Name]);
            panelDictionary.Remove(panelStack.Peek().uiType.Name);
            panelStack.Pop();
            if (panelStack.Count > 0)
            {
                panelStack.Peek().OnEnable();
            }
        }
    }
    public void Clear()
    {
        while (panelStack.Count > 0)
        {
            Pop();
        }
    }
    public GameObject GetSingleObj(UIType type)
    {
        if (panelDictionary.ContainsKey(type.Name))
        {
            return panelDictionary[type.Name];
        }
        if (canvas == null)
        {
            canvas =UIHelper.FindCanvas();
        }
        return InstantiateAddress(type.Name, canvas.transform);
    }

    private GameObject InstantiateAddress(string name, Transform transform)
    {
        var go2 = GameObject.Instantiate(AddressableLoader.SyncLoad<GameObject>(name), transform);
        return go2;
    }
}
