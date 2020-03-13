using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    Vector3 originalPos;
    float count;
    float strength;

    private void Awake()
    {
        Instance = this;
        originalPos = transform.localPosition;

    }
    public void Shake(float _duration, float _strength)
    {
 
        count = _duration;
        strength = _strength;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.I))
        {
            Shake(0.4f, 0.3f);
        }

        if(count <= 0)
        {
            transform.localPosition = originalPos;
            return;
        }

        count -= Time.deltaTime;
        Vector2 disp = Random.insideUnitCircle * strength;
        transform.localPosition = (Vector2)originalPos + disp;

        if (count <= 0)
        {
            count = 0;
        }
    }
}
