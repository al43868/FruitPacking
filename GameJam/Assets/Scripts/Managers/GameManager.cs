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
    /// �����ļ�
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
                string text = ""; // ����һ���ı�����
                print(locale.LocaleName);
                if (locale.LocaleName == "Chinese (Simplified) (zh)")
                {
                    text = item.Description; // ��ȡ�����ı�
                }
                else if (locale.LocaleName == "English (en)")
                {
                    text = item.Des_en; // ��ȡ�����ı�
                }
                else if (locale.LocaleName == "Spanish (es)")
                {
                    text = item.Des_es; // ��ȡ�����ı�
                }
                else if (locale.LocaleName == "Russian (ru)")
                {
                    text = item.Des_ru; // ��ȡ�����ı�
                }
                if (text == "") return "description no language";
                //Regex regex = new Regex(@"\bID\d{4}\b");
                //MatchCollection matches = regex.Matches(text);
                //// ���������е�ÿ��ƥ����
                //foreach (Match match in matches)
                //{
                //    // ��ȡ��ǰƥ�����ֵ����id
                //    string idStr = match.Value;

                //    // ��idת��Ϊ����
                //    int idNum = int.Parse(idStr[2..]);
                //    // ��ȡ��ǰid��Ӧ������
                //    string name = GetNameByID(idNum);

                //    // ����ɫת��Ϊʮ�����Ƹ�ʽ
                //    string hexColor = ColorUtility.ToHtmlStringRGB(Color.blue);

                //    // �ø��ı���ʽ�滻�ı��е�idΪ���֣���������ɫ��ǩ
                //    text = text.Replace(idStr, "<color=#" + hexColor + ">" + name + "</color>");
                //    //text = text.Replace(idStr, $"<color=#{hexColor}>{name}</color>");
                //}
                string pattern = @"_(\d+)_";
                text = Regex.Replace(text, pattern, match =>
                {
                    // ��ȡƥ�䵽���»����ڵ��ı�
                    string matchText = match.Groups[1].Value;

                    // ���»����ڵ��ı�ת��Ϊ����
                    int matchId = int.Parse(matchText);

                    // ��ȡ��ǰƥ�䵽��ID��Ӧ������
                    string matchName = GetNameByID(matchId);

                    // �ø��ı���ʽ�滻ƥ�䵽���»����ڵ��ı�Ϊ���֣���������ɫ��ǩ
                    return "<color=#" + ColorUtility.ToHtmlStringRGB(Color.blue) + ">" + matchName + "</color>";
                });
                return text;
            }
        }
        return "description no id";
    }
    /// <summary>
    /// �����ÿ���ս��
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
    // /// UnityEngine�Դ�����Api��ֻ�ܽ�ȫ��
    // /// </summary>
    // /// <param name="fileName">�ļ���</param>
    //[Button]
    // public void ScreenShotFile()
    // {
    //     string file = string.Format("Assets/Description/{0}.png",i);
    //     i++;
    //     UnityEngine.ScreenCapture.CaptureScreenshot(file);//��ͼ�������ͼ�ļ�
    //     Debug.Log(string.Format("��ȡ��һ��ͼƬ: {0}", file));
    //     UnityEditor.AssetDatabase.Refresh();
    // }
}
