using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<EnemieBehaviour> enemyList = new List<EnemieBehaviour>();
    private int _enemyIndex = 0;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            instance.gameObject.SetActive(false);
            instance = this;
        }
    }

    public void GetAllenemies()
    {
        enemyList = new List<EnemieBehaviour>();
        EnemieBehaviour[] enemiesArray = GameObject.FindObjectsOfType<EnemieBehaviour>();
        foreach(EnemieBehaviour en in enemiesArray)
        {
            enemyList.Add(en);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayEnemyTurn();
        }
    }

    public void PlayEnemyTurn()
    {

        if(_enemyIndex < enemyList.Count )
        {
            enemyList[_enemyIndex].PlayTurn();
        }
        else
        {
            _enemyIndex = 0;
            //RoundManager.instance.PlayEnemyTurn
        }
    }
    
    public void PlayNextEnemyTurn()
    {
        _enemyIndex++;
        PlayEnemyTurn();
    }
}
