using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTimer : MonoBehaviour
{
    float progress;
    void Start()
    {
        progress = 0f;
    }

    void Update()
    {
        progress += 1f * Time.deltaTime;

        if (progress > 100f)
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {

    }

    public void Reset()
    {
        progress = 0f;
    }
}
