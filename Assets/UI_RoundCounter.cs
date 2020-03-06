using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_RoundCounter : MonoBehaviour
{
    [Header("Wheel parameters")]
    public List<TextMeshProUGUI> numbers;
    public Image indicatorImage;
    public Image wheelImage;
    public RectTransform wheelRect;

    private int lastNumber = 0;
    private int nextTextToChange = 0;
    private int startDelay = 4;


    [Header("Animation")]
    public AnimationCurve rotationAnimCurve;
    private float current;
    public float animTime;
    private float percent;
    bool isRotating;
    bool isTargetSet;
    public float oneRotationOf;
    private Vector3 startRotation;
    private Vector3 endRotation;




    private void Update()
    {
        if (isRotating)
        {
            RotateCounter(oneRotationOf);

            if (current < animTime)
            {
                current += Time.deltaTime;

                percent = rotationAnimCurve.Evaluate(current / animTime);
            }
            else
            {
                isRotating = false;
                isTargetSet = false;

                current = 0;
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


        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    SetUpNumbersWheel();
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    TurnWheel();
        //}
    }

    public void ShowRoundUI()
    {
        indicatorImage.gameObject.SetActive(true);
        wheelRect.gameObject.SetActive(true);
    }

    public void HideRoundUI()
    {
        indicatorImage.gameObject.SetActive(false);
        wheelRect.gameObject.SetActive(false);
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

}
