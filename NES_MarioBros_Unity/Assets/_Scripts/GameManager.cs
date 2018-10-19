using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Enemy;

public static class GameManager {

    public static float deathPlaneY = -5.5f;

    public static bool isRunning = true;

    public static List<EnemyObj> enemies = new List<EnemyObj>();    

    public static void AddEnemy(GameObject prefab, Vector2 pos)
    {
        enemies.Add(new EnemyObj(prefab, pos));
    }


    public static void NewGame ()
    {
        isRunning = true;
    }

    public static void SoftReset()
    {
        ResetEnemyPos();

        PlayerController.instance.GetComponent<BoxCollider2D>().enabled = true;

        PlayerController.instance.rb.position = PlayerManager.instance.startPosition;

        PlayerController.instance.state = Status.Idle;

        isRunning = true;
    }

    private static void ResetEnemyPos ()
    {
        GameObject[] enemiesToDestroy = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyToDestroy in enemiesToDestroy)
        {
            GameObject.Destroy(enemyToDestroy);
        }

        List<EnemyObj> enemiesCp = enemies;
        enemies = new List<EnemyObj>();

        foreach (EnemyObj enemy in enemiesCp)
        {
            GameObject instance = GameObject.Instantiate(enemy.prefab);
            instance.GetComponent<EnemyManager>().Spawn(enemy.pos);
        }
    }

    public static void GameOver()
    {

    }


}

public class EnemyObj
{
    public GameObject prefab;
    public Vector2 pos;

    public EnemyObj (GameObject _prefab, Vector2 _pos)
    {
        prefab = _prefab;
        pos = _pos;
    }
}
