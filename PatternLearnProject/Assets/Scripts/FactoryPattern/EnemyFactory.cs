using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.STP;

public class EnemyFactory : MonoBehaviour
{
    public List<EnemyConfig> enemyConfigList = new List<EnemyConfig>();
    [SerializeField]
    EnemyConfig bossConfig;
    public Enemy CreateEnemy(int room)
    {
        EnemyConfig usingConfig = null;


        if(room %10 == 0) // ห้อง Boss
        {
            usingConfig = bossConfig;
        }
        else
        {
            if (enemyConfigList.Count > 0)
            {
                int rngIndex = Random.Range(0, enemyConfigList.Count);
                usingConfig = enemyConfigList[rngIndex];
            }
            else
            {
                return null;
            }
        }
            return BuildEnemy(usingConfig);
    }

    Enemy BuildEnemy(EnemyConfig enemyConfig)
    {
        Enemy newEnemy = new Enemy();
        newEnemy.enemyName = enemyConfig.enemyName;
        newEnemy.maxHP = enemyConfig.baseHP;
        newEnemy.currentHP = newEnemy.maxHP;
        newEnemy.baseDamage = enemyConfig.baseDamage;
        newEnemy.dropExp = enemyConfig.dropEXP;

        if (enemyConfig.weapon != null)
        {
            newEnemy.weapon = new PhysicalWeapon(enemyConfig.weapon);
        }
        else
        {
            newEnemy.weapon = new PhysicalWeapon(new WeaponConfig {
            weaponName = "Enemy Basic Weapon",
            basePhysicalDamage = 1,
            baseElementalDamage = 0,
            description = "Basic weapon"
        });
        }

        EventManager.Publish(new EnemyCreatedEvent
        {
            EnemyName = newEnemy.enemyName,
            EnemyHP = newEnemy.maxHP,
            EnemySprite = enemyConfig.enemySprite
        });
        Debug.Log($"Build Enemy {newEnemy.enemyName} completed");

        return newEnemy; 
    }

}
