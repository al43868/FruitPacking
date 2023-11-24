using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
public class GameManager : SerializedSingleTion<GameManager>
{
    private GameStateFSM stateFSM;
    /// <summary>
    /// 描述文件
    /// </summary>
    public Description desText;
    public GameObject eventSystem;
    [SerializeField]
    private DataList dataList;
    //private bool Animing
    //{
    //    get { return animing; }
    //    set
    //    {
    //        if (value == animing) return;
    //        if (value == true)
    //        {
    //            eventSystem.SetActive(false);
    //            InputListener.Instance.PauseInput();
    //        }
    //        else
    //        {
    //            eventSystem.SetActive(true);
    //            InputListener.Instance.PlayInput();
    //        }
    //        animing = value;
    //    }
    //}
    //[SerializeField]
    //private bool animing;
    
    protected override void Awake()
    {
        base.Awake();
        stateFSM = new GameStateFSM(-1);
        eventSystem = this.transform.GetChild(1).gameObject;
        desText = JsonUtility.FromJson<Description>(AddressableLoader.SyncLoad<TextAsset>("Description").text);
    }
    public DataList GetDataList()
    {
        return dataList;
    }
    public void Log(int textID)
    {
        GameRoot.Instance.Log(GetDescriptionByID(textID));
    }

    //internal void NextDay()
    //{
    //    GameSaver.Instance.GetData().NextDay();
    //    ChangeState(1);
    //}

    public async void LoadScene(int sceneIndex)
    {
        GameRoot.Instance.Clear();
        GameRoot.Instance.ShowCutScenePanel();
        ChangeState(-1);

        if (sceneIndex == 0)
        {
            SceneManager.LoadScene(0);
            if (InputManager.Instance != null)
            {
                InputManager.Instance.gamePlay.Disable();
            }
            //pauseing = false;
            ChangeState(-1);
            AudioManager.Instance.PlayMusic(Music.BG3, true);
        }
        else if (sceneIndex == 1)
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.gamePlay.Enable();
            }
            await SceneManager.LoadSceneAsync(1);
            //MapManager.Instance.Init(GameSaver.Instance.GetData().mapData);
            //DevelopManager.Instance.Init();
            ChangeState(1);
        }
    }
    internal int GetState()
    {
        return stateFSM.CurrentState;
    }
    [Button]
    public void DebugState()
    {
        Debug.Log(stateFSM.CurrentState);
    }
    public void ChangeState(int i)
    {
        stateFSM.SwitchToState(i);
    }
    public string GetNameByID(int id)
    {
        foreach (var item in desText.des)
        {
            if (item.ID == id)
            {
                var locale = LocalizationSettings.Instance.GetSelectedLocale();
                if (locale.LocaleName == "Chinese (Simplified) (zh)")
                {
                    return item.Name;
                }
                else if (locale.LocaleName == "English (en)")
                {
                    return item.Name_en;
                }
                else if (locale.LocaleName == "Spanish (es)")
                {
                    return item.Name_es;
                }
                else if (locale.LocaleName == "Russian(ru)")
                {
                    return item.Name_ru;
                }
            }
        }
        return "name";
    }

    public string GetDescriptionByID(int id)
    {
        foreach (var item in desText.des)
        {
            if (item.ID == id)
            {
                var locale = LocalizationSettings.Instance.GetSelectedLocale();
                string text = ""; // 定义一个文本变量
                print(locale.LocaleName);
                if (locale.LocaleName == "Chinese (Simplified) (zh)")
                {
                    text = item.Description; // 获取描述文本
                }
                else if (locale.LocaleName == "English (en)")
                {
                    text = item.Des_en; // 获取描述文本
                }
                else if (locale.LocaleName == "Spanish (es)")
                {
                    text = item.Des_es; // 获取描述文本
                }
                else if (locale.LocaleName == "Russian (ru)")
                {
                    text = item.Des_ru; // 获取描述文本
                }
                if (text == "") return "description no language";
                //Regex regex = new Regex(@"\bID\d{4}\b");
                //MatchCollection matches = regex.Matches(text);
                //// 遍历集合中的每个匹配项
                //foreach (Match match in matches)
                //{
                //    // 获取当前匹配项的值，即id
                //    string idStr = match.Value;

                //    // 将id转换为整数
                //    int idNum = int.Parse(idStr[2..]);
                //    // 获取当前id对应的名字
                //    string name = GetNameByID(idNum);

                //    // 将颜色转换为十六进制格式
                //    string hexColor = ColorUtility.ToHtmlStringRGB(Color.blue);

                //    // 用富文本格式替换文本中的id为名字，并加上颜色标签
                //    text = text.Replace(idStr, "<color=#" + hexColor + ">" + name + "</color>");
                //    //text = text.Replace(idStr, $"<color=#{hexColor}>{name}</color>");
                //}
                string pattern = @"_(\d+)_";
                text = Regex.Replace(text, pattern, match =>
                {
                    // 获取匹配到的下划线内的文本
                    string matchText = match.Groups[1].Value;

                    // 将下划线内的文本转换为整数
                    int matchId = int.Parse(matchText);

                    // 获取当前匹配到的ID对应的名字
                    string matchName = GetNameByID(matchId);

                    // 用富文本格式替换匹配到的下划线内的文本为名字，并加上颜色标签
                    return "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" + matchName + "</color>";
                });
                return text;
            }
        }
        return "description no id";
    }
    /// <summary>
    /// 测试用快速战斗
    /// </summary>
    [Button]
    public void QuickFight()
    {
        //GameSaver.Instance.save.allSave.Add(new SaveGroup());
        //GameSaver.Instance.currentSaveGroup = (GameSaver.Instance.save.allSave.Count - 1);
        //GameSaver.Instance.SetData(new(RandomManager.Instance.dataList.maps[0], new()));
        GameSaver.Instance.CreatNewData();
        LoadScene(1);
    }

    // public int i;
    // /// <summary>
    // /// UnityEngine自带截屏Api，只能截全屏
    // /// </summary>
    // /// <param name="fileName">文件名</param>
    //[Button]
    // public void ScreenShotFile()
    // {
    //     string file = string.Format("Assets/Description/{0}.png",i);
    //     i++;
    //     UnityEngine.ScreenCapture.CaptureScreenshot(file);//截图并保存截图文件
    //     Debug.Log(string.Format("截取了一张图片: {0}", file));
    //     UnityEditor.AssetDatabase.Refresh();
    // }
}
