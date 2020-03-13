using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceSound : MonoBehaviour
{
    AudioSource source;

    public static AmbianceSound Instance;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if(!source.isPlaying)
        {
            source.Play();
        }
    }
}
