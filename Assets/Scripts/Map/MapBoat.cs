using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MapBoat : MonoBehaviour
{
    public static MapBoat Instance;

    public Transform lookingTransform;

    private void Awake()
    {
        Instance = this;
    }

    public int currentPathIndex;

    public void Place(int index)
    {
        currentPathIndex = BoatPath.Instance.anchorIndexes[index];
        transform.position = BoatPath.Instance.AnchorPosition(index);
        targetPos = transform.position;

    }

    public void FaceDirection()
    {
        if(currentPathIndex >= BoatPath.Instance.bezierPath.Length-1)
        {
            return;
        }
        Vector3 toFace = BoatPath.Instance.bezierPath[currentPathIndex+1];
        lookingTransform.position = transform.position;
        lookingTransform.LookAt(toFace);
    }

    Vector3 reference;
    Quaternion reference2;
    public float movementSmoothTime;
    public float movementTurnTime;
    private void FixedUpdate()
    {
        FaceDirection();

        //displace object
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref reference, movementSmoothTime);

        if(moving)
        {
            transform.DORotate(lookingTransform.eulerAngles, movementTurnTime);
        }
        else
        {
            transform.DORotate(lookingTransform.eulerAngles, 0);
        }
    }

    private void Update()
    {
        if (!moving) return;

        Move();
    }

    public float movingDelay;
    float delayCount;
    void Move()
    {
        delayCount += Time.deltaTime;
        if(delayCount >= movingDelay)
        {
            CheckMovement();
            delayCount = 0f;
        }
    }

    void CheckMovement()
    {
        if(movementTargetIndex >= targetIndex)
        {
            FinishMovement();
        }
        else
        {
            IncrementTarget();
        }
    }

    void FinishMovement()
    {
        movementTargetIndex = targetIndex;
        currentPathIndex = targetIndex;
        moving = false;
        LevelPanel.Instance.Display();
    }

    void IncrementTarget()
    {
        movementTargetIndex++;
        currentPathIndex++;
        targetPos = BoatPath.Instance.bezierPath[movementTargetIndex];

    }

    Vector3 targetPos;
    int targetIndex;
    int movementTargetIndex;
    bool moving;
    public void MoveToAnchor(int index)
    {
        movementTargetIndex = currentPathIndex + 1;
        targetIndex = BoatPath.Instance.anchorIndexes[index];
        moving = true;
    }
}
