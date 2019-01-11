using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [SerializeField]
    Text WoodText, WaterText;

    private int woodCount = 10;
    private int waterCount = 5;

    private void Update()
    {
        WoodText.text = woodCount.ToString();
        WaterText.text = waterCount.ToString();
    }

    public void AddWood()
    {
        woodCount++;
    }

    public bool RemoveWood(int amount)
    {
        if (woodCount - amount >= 0)
        {
            woodCount -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddWater()
    {
        waterCount++;
    }

    public bool RemoveWater(int amount)
    {
        if (waterCount - amount >= 0)
        {
            waterCount -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
