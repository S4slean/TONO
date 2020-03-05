using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string path;

    public Data data = new Data();

    public bool saved;

    public bool wipe;

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
        data = new Data();
    }

    void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "data.json");
    }

    public void Save(bool game)
    {
        if(SavingIcon.Instance)
        {
            SavingIcon.Instance.PlaySaveAnimation();
        }

        //print("Saving");
        SetData(game);

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

    void SetData(bool game)
    {
        //sounds & music
        if (SoundManager.Instance)
        {
            data.sfxVolume = SoundManager.Instance.sfxVolume;
        }
        if (MusicManager.Instance)
        {
            data.musicVolume = MusicManager.Instance.musicVolume;
        }

        if (game)
        {
            print("Setting Data Game");
            data.playerStats = GameManager.Instance.playerStats;
        }
        else
        {
            print("Setting Data Menu");
        }
    }

    void SerializeData()
    {
        string dataString = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, dataString);
    }

    public void Load(bool exploits, bool game)
    {
        if (File.Exists(path))
        {
            DeserializeData();
        }
        else
        {
            data = new Data();
        }

        if (wipe)
        {
            Wipe();
        }
        else
        {
            if (exploits)
            {
                if (game)
                {
                    ExploitDataGame();
                }
                else
                {
                    ExploitDataMenu();
                }
            }
        }

    }

    public void DeserializeData()
    {
        string loadedString = File.ReadAllText(path);
        data = JsonUtility.FromJson<Data>(loadedString);
    }

    public void ExploitDataGame()
    {
        print("Exploiting Data Game");

        //sounds & music
        if (SoundManager.Instance)
        {
            SoundManager.Instance.sfxVolume = data.sfxVolume;
        }
        if (MusicManager.Instance)
        {
            MusicManager.Instance.musicVolume = data.musicVolume;
        }

        GameManager.Instance.playerStats = data.playerStats;
    }


    public void ExploitDataMenu()
    {
        print("Exploiting Data Menu");

        //sounds & music
        if(SoundManager.Instance)
        {
            SoundManager.Instance.sfxVolume = data.sfxVolume;
        }
        if(MusicManager.Instance)
        {
            MusicManager.Instance.musicVolume = data.musicVolume;
        }
    }
}
