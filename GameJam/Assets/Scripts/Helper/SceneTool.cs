#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class SceneTool : OdinMenuEditorWindow
{
    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new(supportsMultiSelect: true)
        {
            { "Scene", this, EditorIcons.House },
        };
        return tree;
    }
    [MenuItem("Tools/Scene1")]
    public static void Scene1()
    {
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
        EditorSceneManager.OpenScene("Assets/Scenes/StartScene.unity");
    }
    [MenuItem("Tools/Scene2")]
    public static void Scene2()
    {
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
        EditorSceneManager.OpenScene("Assets/Scenes/PlayScene.unity");
    }
    [MenuItem("Tools/Scene3")]
    public static void Scene3()
    {
        EditorSceneManager.SaveOpenScenes();
        AssetDatabase.SaveAssets();
        EditorSceneManager.OpenScene("Assets/Scenes/DevelopScene.unity");
    }
}
# endif