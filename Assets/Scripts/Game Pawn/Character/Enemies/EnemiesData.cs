using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TONO/Enemy" )]
public class EnemyData : ScriptableObject
{

    public enum EnemyType { Default, Distance, Big}

    public string enemyName = "Enemy";
    public EnemyType enemyType;
    public List<Skill> skills;
    public int health = 1;
    public int movement = 3;

}
