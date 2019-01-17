using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    [SerializeField]
    float waitTime = 1f;

    [SerializeField]
    int enemyCount;

    float timer;

    [SerializeField]
    EnemySpawner enemySpawner;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            print("Timer is done");
            timer = 0f;

            for (int i = 0; i < enemyCount; i++)
            {
                enemySpawner.SpawnEnemy();
            }
        }
    }
}
