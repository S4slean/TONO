using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPath : MonoBehaviour
{
    public static BoatPath Instance;

    private void Awake()
    {
        Instance = this;
    }

    public LineRenderer pathLineRenderer;

    public int pathSubdivisions;

    public Vector3[] basePath;

    public Vector3[] startingTangeants;

    public Vector3[] endingTangeants;

    public Vector3[] bezierPath;

    public int[] anchorIndexes;

    public int[] anchorPointsIndexes;
    public float height;

    public void AddAnchorPointsIndex()
    {
        int[] newIndexes = new int[anchorPointsIndexes.Length + 1];

        for(int i = 0; i < anchorPointsIndexes.Length; i++)
        {
            newIndexes[i] = anchorPointsIndexes[i];
        }

        anchorPointsIndexes = newIndexes;
    }

    public void RemoveAnchorPointsIndex()
    {
        if (anchorPointsIndexes.Length < 1) return;

        int[] newIndexes = new int[anchorPointsIndexes.Length - 1];

        for (int i = 0; i < newIndexes.Length; i++)
        {
            newIndexes[i] = anchorPointsIndexes[i];
        }

        anchorPointsIndexes = newIndexes;
    }

    public GameObject anchorPoint;
    public void DrawPath()
    {
        pathLineRenderer.positionCount = bezierPath.Length;
        pathLineRenderer.SetPositions(bezierPath);
        
        //place anchors
        for(int i = 0; i < anchorPointsIndexes.Length; i++)
        {
            GameObject newAnchor = Instantiate(anchorPoint, transform);
            newAnchor.transform.position = bezierPath[anchorPointsIndexes[i]];
        }

        //get final path
        anchorIndexes = new int[anchorPointsIndexes.Length + 2];
        anchorIndexes[0] = 0;
        anchorIndexes[anchorIndexes.Length - 1] = bezierPath.Length;
        for(int i = 1; i < anchorIndexes.Length-1; i++)
        {
            anchorIndexes[i] = anchorPointsIndexes[i-1];
        }
    }

    public Vector3 AnchorPosition(int index)
    {
        return bezierPath[anchorIndexes[index]];
    }

    public void AddBasePathNode()
    {
        Vector3[] newBasePath = new Vector3[basePath.Length + 1];
        for(int i = 0; i < basePath.Length; i++)
        {
            newBasePath[i] = basePath[i];
        }

        if (newBasePath.Length >= 2)
        {
            newBasePath[newBasePath.Length - 1] = newBasePath[newBasePath.Length - 2] + new Vector3(2, 0, 2);
        }
   

        basePath = newBasePath;

        Vector3[] newStartingTangeants = new Vector3[basePath.Length];
        for (int i = 0; i < startingTangeants.Length; i++)
        {
            newStartingTangeants[i] = startingTangeants[i];
        }

        if (newBasePath.Length >= 2)
        {
            newStartingTangeants[newBasePath.Length - 1] = newBasePath[newBasePath.Length - 2] + new Vector3(1f, 0, 1f);
        }


        startingTangeants = newStartingTangeants;

        Vector3[] newEndingTangeants = new Vector3[basePath.Length];
        for (int i = 0; i < endingTangeants.Length; i++)
        {
            newEndingTangeants[i] = endingTangeants[i];
        }

        if (newBasePath.Length >= 2)
        {
            newEndingTangeants[newBasePath.Length - 1] = newBasePath[newBasePath.Length - 2] + new Vector3(3, 0, 3);
        }

        endingTangeants = newEndingTangeants;



    }

    public void RemoveBasePathNode()
    {
        if (basePath.Length < 1) return;

        Vector3[] newBasePath = new Vector3[basePath.Length - 1];
        for (int i = 0; i < newBasePath.Length; i++)
        {
            newBasePath[i] = basePath[i];
        }

        basePath = newBasePath;

        Vector3[] newStartingTangeants = new Vector3[basePath.Length];
        for (int i = 0; i < newStartingTangeants.Length; i++)
        {
            newStartingTangeants[i] = startingTangeants[i];
        }

        startingTangeants = newStartingTangeants;

        Vector3[] newEndingTangeants = new Vector3[basePath.Length];
        for (int i = 0; i < newEndingTangeants.Length; i++)
        {
            newEndingTangeants[i] = endingTangeants[i];
        }

        endingTangeants = newEndingTangeants;
    }
}
