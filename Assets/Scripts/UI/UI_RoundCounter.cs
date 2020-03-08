using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RoundCounter : Panel_Behaviour
{
    [Header("Wheel parameters")]
    public List<TextMeshProUGUI> numbers;
    public Image indicatorImage;
    public Image wheelImage;
    public RectTransform wheelRect;

    private int lastNumber = 0;
    private int nextTextToChange = 0;
    private int startDelay = 4;


    [Header("Rotation Animation")]
    public AnimationCurve rotationAnimCurve;
    private float currentTime;
    public float animTime;
    private float percent;
    bool isRotating;
    bool isTargetSet;
    public float oneRotationOf;
    private Vector3 startRotation;
    private Vector3 endRotation;



    private void Awake()
    {
        SetUpNumbersWheel();
    }

    private void Update()
    {
        RotateCounter();

        MovePanel();
    }


    private void RotateCounter()
    {
        if (isRotating)
        {
            RotateCounter(oneRotationOf);

            if (currentTime < animTime)
            {
                currentTime += Time.deltaTime;

                percent = rotationAnimCurve.Evaluate(currentTime / animTime);
            }
            else
            {
                isRotating = false;
                isTargetSet = false;

                currentTime = 0;
                percent = 0;


                startRotation = new Vector3(0, 0, wheelRect.localEulerAngles.z);

                if (startDelay > 0)
                {
                    startDelay--;
                }
                else
                {
                    string newNumber = lastNumber.ToString();
                    numbers[nextTextToChange].text = newNumber;

                    lastNumber++;

                    nextTextToChange++;
                    if (nextTextToChange >= numbers.Count)
                        nextTextToChange = 0;
                }
            }
        }
    }

    public void RotateCounter(float tilt)
    {
        if (!isTargetSet)
        {
            isTargetSet = true;
            endRotation = new Vector3(startRotation.x, startRotation.y, startRotation.z + tilt);

            //Debug.Log("current Rotation = " + currentRotation);
            //Debug.Log("target Rotation = " + targetRotation);
        }
        else
        {
            wheelRect.localEulerAngles = Vector3.Lerp(startRotation, endRotation, percent);
        }
    }

    public void SetUpNumbersWheel()
    {
        indicatorImage.sprite = UI_Manager.instance.uiPreset.indicatorImage;
        wheelImage.sprite = UI_Manager.instance.uiPreset.wheelImage;

        lastNumber = numbers.Count;

        for (int i = 0; i < numbers.Count; i++)
        {
            string number = i.ToString();
            numbers[i].text = number;
        }
    }

    public void TurnWheel()
    {
        if (isRotating)
            return;

        isRotating = true;
    }


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel()
    {
        base.MovePanel();
    }
}
