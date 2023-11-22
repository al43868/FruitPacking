using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper 
{
    /// <summary>
    /// ��Ŀ������и��������ҵ��Ӷ���
    /// </summary>
    /// <param name="Find_Panel">Ŀ��Panel</param>
    /// <param name="Find_Name">Ŀ���Ӷ�������</param>
    /// <returns></returns>
    public static GameObject FindObjectInChild(GameObject Find_Panel, string Find_Name)
    {
        //ע��Ҫ��s
        Transform[] transforms_find = Find_Panel.GetComponentsInChildren<Transform>();

        foreach (Transform tra in transforms_find)
        {
            if (tra.gameObject.name == Find_Name)
            {
                return tra.gameObject;
            }
        }
        Debug.LogWarning($"û����{Find_Panel.name}���ҵ�{Find_Name}���壡");
        return null;
    }
    /// <summary>
    /// ��õ�ǰ�����е�Canvas
    /// </summary>
    /// <returns>Canvas Object</returns>
    public static  GameObject FindCanvas()
    {
        GameObject gameObject_canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        if (gameObject_canvas == null)
        {
            Debug.LogError("��ǰ��������û��Canvas,����ӣ�");
        }
        return gameObject_canvas;
    }
    /// <summary>
    /// ��Ŀ������л�ö�Ӧ���
    /// </summary>
    /// <typeparam name="T">��Ӧ���</typeparam>
    /// <param name="Get_Obj">Ŀ�����</param>
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
    ///��Ŀ��Panel���������У���������������ƻ�ö�Ӧ��� 
    /// </summary>
    /// <typeparam name="T">��Ӧ���</typeparam>
    /// <param name="panel">Ŀ��Panel</param>
    /// <param name="ComponentName">����������</param>
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
        Debug.LogWarning($"û����{panel.name}���ҵ�{ComponentName}���壡");
        return null;
    }
}
