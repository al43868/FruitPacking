using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController 
{
    public Dictionary<string ,SceneBase> scenes;
    public SceneController()
    {
        scenes = new Dictionary<string ,SceneBase>();
    }
    public void LoadScene(string sceneName,SceneBase sceneBase)
    {
        if (!scenes.ContainsKey(sceneName))
            {
            scenes.Add(sceneName, sceneBase);
        }
        if (scenes.ContainsKey(SceneManager.GetActiveScene().name))
        {
            scenes[SceneManager.GetActiveScene().name].ExitScene();
        }
        //Çå³ý
        GameRoot.Instance.Clear();
        SceneManager.LoadScene(sceneName);
        sceneBase.EnterScene();
    }
}
