using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper 
{
    /// <summary>
    /// 从目标对象中根据名字找到子对象
    /// </summary>
    /// <param name="Find_Panel">目标Panel</param>
    /// <param name="Find_Name">目标子对象名称</param>
    /// <returns></returns>
    public static GameObject FindObjectInChild(GameObject Find_Panel, string Find_Name)
    {
        //注意要加s
        Transform[] transforms_find = Find_Panel.GetComponentsInChildren<Transform>();

        foreach (Transform tra in transforms_find)
        {
            if (tra.gameObject.name == Find_Name)
            {
                return tra.gameObject;
            }
        }
        Debug.LogWarning($"没有在{Find_Panel.name}中找到{Find_Name}物体！");
        return null;
    }
    /// <summary>
    /// 获得当前场景中的Canvas
    /// </summary>
    /// <returns>Canvas Object</returns>
    public static  GameObject FindCanvas()
    {
        GameObject gameObject_canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (gameObject_canvas == null)
        {
            Debug.LogError("当前场景当中没有Canvas,请添加！");
        }
        return gameObject_canvas;
    }
    /// <summary>
    /// 从目标对象中获得对应组件
    /// </summary>
    /// <typeparam name="T">对应组件</typeparam>
    /// <param name="Get_Obj">目标对象</param>
    /// <returns></returns>
    public static  T AddOrGetComponent<T>(GameObject Get_Obj) where T : Component
    {
        if (Get_Obj == null) { Debug.Log("no panel"); Time.timeScale = 0; }
        if (Get_Obj.GetComponent<T>() != null)
        {
            return Get_Obj.GetComponent<T>();
        }
        else
        {
            Get_Obj.AddComponent<T>();
            return Get_Obj.GetComponent<T>();
        }
    }
    /// <summary>
    ///从目标Panel的子物体中，根据子物体的名称获得对应组件 
    /// </summary>
    /// <typeparam name="T">对应组件</typeparam>
    /// <param name="panel">目标Panel</param>
    /// <param name="ComponentName">子物体名称</param>
    /// <returns></returns>
    public static T GetComponentInChild<T>(GameObject panel, string ComponentName) where T : Component
    {
        if (panel == null) Debug.LogError("no Panel");
        Transform[] transforms = panel.GetComponentsInChildren<Transform>();
        foreach (Transform tra in transforms)
        {
            if (tra.gameObject.name == ComponentName)
            {
                return tra.gameObject.GetComponent<T>();
            }
        }
        Debug.LogWarning($"没有在{panel.name}中找到{ComponentName}物体！");
        return null;
    }
}
