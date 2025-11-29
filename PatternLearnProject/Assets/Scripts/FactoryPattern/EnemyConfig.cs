using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    public string enemyName = "Goblin";
    public float baseHP = 10;
    public float baseDamage = 3;
    public float dropEXP = 100;
    public Sprite enemySprite;

    public WeaponConfig weapon;
         

    //public List<IEnemyAction> enemyPossibleAction = new List<IEnemyAction>() { };

}
