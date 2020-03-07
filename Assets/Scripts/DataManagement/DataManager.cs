using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string path;

    public GameData data = new GameData();

    public bool saved;

    public bool wipe;

    public LevelList levelList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetPath();
    }


    void Wipe()
    {
        data = new GameData();
    }

    void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "data.json");
    }

    public void Save(SceneType sceneType)
    {
        if(SavingIcon.Instance)
        {
            SavingIcon.Instance.PlaySaveAnimation();
        }

        SetData(sceneType);

        if(!data.saved)
        {
            data.saved = true;
            saved = true;
        }

        if (wipe)
        {
            wipe = false;
            Wipe();
        }

        SerializeData();
    }

    void SetData(SceneType sceneType)
    {
        //generic
        if (SoundManager.Instance)
        {
            data.sfxVolume = SoundManager.Instance.sfxVolume;
        }
        if (MusicManager.Instance)
        {
            data.musicVolume = MusicManager.Instance.musicVolume;
        }

        if(LevelManager.levelProgresses != null)
        {
            data.levelProgresses = LevelManager.levelProgresses;
        }


        if (sceneType == SceneType.game)
        {
            data.playerStats = GameManager.Instance.playerStats;
        }
        else if(sceneType == SceneType.map)
        {

        }
        else
        {
            
        }
    }

    void SerializeData()
    {
        string dataString = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, dataString);
    }

    public void Load(bool exploits, SceneType sceneType)
    {
        if (File.Exists(path))
        {
            DeserializeData();
        }
        else
        {
            data = new GameData();
        }

        if (wipe)
        {
            Wipe();
        }
        else
        {
            if (exploits)
            {
                ExploitData(sceneType);
            }
        }

    }

    public void DeserializeData()
    {
        string loadedString = File.ReadAllText(path);
        data = JsonUtility.FromJson<GameData>(loadedString);
    }

    public void ExploitData(SceneType sceneType)
    {
        //generic behaviors
        if (SoundManager.Instance)
        {
            SoundManager.Instance.sfxVolume = data.sfxVolume;
        }
        if (MusicManager.Instance)
        {
            MusicManager.Instance.musicVolume = data.musicVolume;
        }

        if(data.levelProgresses != null)
        {
            LevelManager.levelProgresses = data.levelProgresses;
        }
        else
        {
            if(LevelManager.levelProgresses == null)
            {
                LevelManager.InitializeLevelProgresses(levelList);
            }
        }

        //specific behaviors
        if (sceneType == SceneType.game)
        {
            GameManager.Instance.playerStats = data.playerStats;
            GameManager.Instance.combatsCompleted = data.combatsCompleted;
        }
        else if(sceneType == SceneType.map)
        {
            MapManager.Instance.combatsCompleted = data.combatsCompleted;
        }
        else
        {
            MenuManager.Instance.combatsCompleted = data.combatsCompleted;
        }
    }
}

public enum SceneType
{
    game,
    map,
    menu
}
