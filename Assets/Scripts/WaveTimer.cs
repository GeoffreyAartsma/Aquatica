using UnityEngine;

public class Timer : MonoBehaviour
{
    public float waitTime = 1f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            print("Timer is done");
            timer = 0f;
        }
    }
}
