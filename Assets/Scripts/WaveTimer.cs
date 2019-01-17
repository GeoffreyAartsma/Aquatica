using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    [SerializeField]
    float waitTime = 5f;

    [SerializeField]
    int enemyCount;

    [SerializeField]
    Slider waveSlider;

    float timer;

    [SerializeField]
    EnemySpawner enemySpawner;

    void Update()
    {
        timer += Time.deltaTime;

        float timePercent = timer * 100 / waitTime;
        waveSlider.value = timePercent;

        if (timer > waitTime)
        {
            timer = 0f;
            StartCoroutine("SpawnEnemies");
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++) {
            enemySpawner.SpawnEnemy();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
