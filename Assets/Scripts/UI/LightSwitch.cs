using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<GameObject> lights;

    public static LightSwitch Instance;

    public float startingDelay;
    public float endDelay;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SwitchLightsOffNoSound();
        StartCoroutine(SwitchOnAtStart());
    }

    IEnumerator SwitchOnAtStart()
    {
        yield return new WaitForSeconds(startingDelay);
        SwitchLightsOn();
    }

    public void SwitchOffThenLoad(string sceneName)
    {
        StartCoroutine(SwitchOffThenLoadCoroutine(sceneName));
    }

    IEnumerator SwitchOffThenLoadCoroutine(string sceneName)
    {
        SwitchLightsOff();
        yield return new WaitForSeconds(endDelay);
        LevelManager.GoToSceneDirectly(sceneName);
    }

    public void SwitchLightsOn()
    {
        for(int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(true);
        }
    }

    public void SwitchLightsOff()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(false);
        }

    }

    public void SwitchLightsOffNoSound()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].SetActive(false);
        }

    }
}
