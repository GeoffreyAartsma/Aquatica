using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCounter : MonoBehaviour {

    [SerializeField]
    Text WaterText;

    private int waterCount;

    private void Update()
    {
        WaterText.text = waterCount.ToString();
    }

    public void AddWater() 
    {
        waterCount++;
    }

    public void RemoveWater(int amount)
    {

    }
}
