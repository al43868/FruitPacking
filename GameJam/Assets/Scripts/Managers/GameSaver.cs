using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class GameSaver : SerializedSingleTion<GameSaver>
{
    /// <summary>
    /// ���д浵
    /// </summary>
    //public SaveData save;
    /// <summary>
    /// ��ǰ�浵��
    /// </summary>
    //public int currentSaveGroup;
    /// <summary>
    /// ��ǰ�浵
    /// </summary>
    //public int currentSaveGrid;
    /// <summary>
    /// ��ǰ��Ϸ��������
    /// </summary>
    [SerializeField]
    private GameData data;
    /// <summary>
    /// ��ǰ��Ϸ����
    /// </summary>
    public GameSetting setting;
    /// <summary>
    /// ����Ԥ������
    /// </summary>
    public DataList dataList;
    #region Init
    protected override void Awake()
    {
        base.Awake();
        //Load(); todo
       _=InitLocalization();
    }
//    /// <summary>
//    /// ��ȡ���ش浵
//    /// </summary>
//    private async void Load()
//    {
//        string savePath = "";
//        string saveSettingPath = "";
//#if UNITY_EDITOR
//        savePath = Application.persistentDataPath + "/SaveEditor.json";
//        saveSettingPath = Application.persistentDataPath + "/SaveSettingEditor.json";
//#endif
//#if UNITY_STANDALONE_WIN&& !UNITY_EDITOR
//        savePath = Application.persistentDataPath + "/Save.json";
//        saveSettingPath = Application.persistentDataPath + "/SaveSetting.json";
//#endif
//        if (File.Exists(savePath))
//        {
//            string str = File.ReadAllText(savePath);
//            print("LoadSave: " + savePath);
//            save = JsonUtility.FromJson<SaveData>(str);
//        }
//        else
//        {
//            save = new();
//        }
//        if (File.Exists(saveSettingPath))
//        {
//            string str = File.ReadAllText(saveSettingPath);
//            print("LoadSaveSetting: " + saveSettingPath);
//            setting = JsonUtility.FromJson<GameSetting>(str);
//        }
//        else
//        {
//            setting = new GameSetting();
//        }
//        await InitLocalization();
//        SetSaveSetting();
//    }

    internal GameSetting GetSetting()
    {
        return setting;
    }
    public void SetSaveSetting()
    {
        //��������
        foreach (var item in locales)
        {
            if (item.LocaleName == setting.language)
            {
                LocalizationSettings.Instance.SetSelectedLocale(item);
            }
        }
        Screen.SetResolution(setting.width, setting.height, setting.fullScreen);

    }
    #endregion

    #region �浵
    ///// <summary>
    ///// ���ٴ浵
    ///// </summary>
    //internal void QuickSave()
    //{
    //    GameManager.Instance.Log(10002);
    //    SaveGrid SaveGrid = new(data);
    //    currentSaveGrid = save.QuickSave(currentSaveGroup, SaveGrid);
    //    SaveByJson();
    //}
    //[Button]
    //internal void GameWin()
    //{
    //    save.gameEnd.SetMapEnd(data.mapData.model.ID, data.gameRisk.riskValue);
    //    SaveByJson();
    //    GameRoot.Instance.ShowGameWinPanel();
    //}

    //internal void RemoveSaveGroup(int j)
    //{
    //    save.allSave.RemoveAt(j);
    //    if (save.endSaveGroup == j)
    //    {
    //        save.endSaveGroup = -1;
    //    }
    //}

    //internal void SaveAt(int j)
    //{
    //    SaveGrid SaveGrid = new(data);
    //    save.Save(SaveGrid, currentSaveGroup, j);
    //    currentSaveGrid = j;
    //}

    //internal void RemoveSaveGrid(int j)
    //{
    //    save.allSave[currentSaveGroup].Remove(j);
    //    SaveByJson();

    //}

    ///// <summary>
    ///// ���ٶ�ȡ����ʼ���棩
    ///// </summary>
    //public void QuickLoad()
    //{
    //    if (save == null)
    //    {
    //        GameManager.Instance.Log(10004);
    //        return;
    //    }
    //    else
    //    {
    //        if (save.endSaveGroup == -1)
    //        {
    //            GameManager.Instance.Log(10004);
    //            return;
    //        }
    //        else
    //            LoadSaveGrid(save.endSaveGroup, save.allSave[save.endSaveGroup].endSaveIndex);
    //    }
    //}
    ///// <summary>
    ///// ѡ��浵
    ///// </summary>
    ///// <param name="save"></param>
    //internal void LoadSaveGrid(int saveGroup, int saveGrid)
    //{
    //    currentSaveGroup = saveGroup;
    //    currentSaveGrid = saveGrid;
    //    InitData();
    //    GameManager.Instance.LoadScene(1);
    //}

    ///// <summary>
    ///// ѡ��浵��
    ///// </summary>
    ///// <param name="save"></param>
    //public void LoadSaveGroup(int save)
    //{
    //    currentSaveGroup = save;
    //    GameRoot.Instance.ShowSavePanel(false, true);
    //}

    #endregion
    //public void InitData()
    //{
    //    string str = JsonUtility.ToJson(save.allSave[currentSaveGroup].allSaveGrids[currentSaveGrid].data);
    //    data = JsonUtility.FromJson<DevelopData>(str);
    //}
    public GameData GetData()
    {
        return data;
    }
    public void SetData(GameData developData)
    {
        data = developData;
    }
//    /// <summary>
//    /// ���ô浵
//    /// </summary>
//    public void SaveByJson()
//    {

//        string savePath = "";
//#if UNITY_EDITOR
//        savePath = Application.persistentDataPath + "/SaveEditor.json";
//#endif
//#if UNITY_STANDALONE_WIN&& !UNITY_EDITOR
//        savePath = Application.persistentDataPath + "/Save.json";
//#endif
//        if (save == null) save = new();
//        string saveJsonStr = JsonUtility.ToJson(save);
//        StreamWriter sw = File.CreateText(savePath);
//        Debug.Log("Save: " + savePath);
//        sw.Write(saveJsonStr);
//        //�ر�StreamWriter
//        sw.Close();
//        Debug.Log("����ɹ�");
//    }

    internal void CreatNewData()
    {
        data = new();
    }
    public void SaveGameSettingByJson()
    {
        //todo
    }
//    public void SaveGameSettingByJson()
//    {
//        string saveSettingPath = "";
//#if UNITY_EDITOR
//        saveSettingPath = Application.persistentDataPath + "/SaveSettingEditor.json";
//#endif
//#if UNITY_STANDALONE_WIN&& !UNITY_EDITOR
//        saveSettingPath = Application.persistentDataPath + "/SaveSetting.json";
//#endif
//        if (save == null) save = new();
//        string saveJsonStr = JsonUtility.ToJson(setting);
//        StreamWriter sw = File.CreateText(saveSettingPath);
//        Debug.Log("Save: " + saveSettingPath);
//        sw.Write(saveJsonStr);
//        //�ر�StreamWriter
//        sw.Close();
//        GameManager.Instance.Log(12048);
//        Debug.Log("�������óɹ�");
////    }
//    [Button]
//    public void DeleteSave()
//    {
//        string savePath = "";
//#if UNITY_EDITOR
//        savePath = Application.persistentDataPath + "/SaveEditor.json";
//#endif
//#if UNITY_STANDALONE_WIN&& !UNITY_EDITOR
//        savePath = Application.persistentDataPath + "/Save.json";
//#endif
//        File.Delete(savePath);
//        print("delete");
//    }
    #region Localization
    public List<Locale> locales;
    private async UniTask InitLocalization()
    {
        await LocalizationSettings.SelectedLocaleAsync;
        locales = LocalizationSettings.AvailableLocales.Locales;
    }
    /// <summary>
    /// ��ȡ��ǰ����Index
    /// </summary>
    /// <returns></returns>
    public int GetLanguageIndex()
    {
        for (int i = 0; i < locales.Count; i++)
        {
            if (locales[i].LocaleName == setting.language)
            {
                return i;
            }
        }
        return -1;
    }
    public void ChangeLanguage(int index)
    {
        LocalizationSettings.Instance.SetSelectedLocale(locales[index]);
        setting.language = LocalizationSettings.Instance.GetSelectedLocale().LocaleName;
        //SaveGameSettingByJson();
        SetSaveSetting();
    }
    #endregion
}
