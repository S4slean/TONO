using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public List<EnemieBehaviour> enemyList = new List<EnemieBehaviour>();

    public void GetAllenemies()
    {
        enemyList = new List<EnemieBehaviour>();
        EnemieBehaviour[] enemiesArray = GameObject.FindObjectsOfType<EnemieBehaviour>();
        foreach(EnemieBehaviour en in enemiesArray)
        {
            enemyList.Add(en);
        }

    }

    
}
