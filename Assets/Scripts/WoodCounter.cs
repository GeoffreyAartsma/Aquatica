using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodCounter : MonoBehaviour {

    [SerializeField]
    Text WoodText;

    private int woodCount;

    private void Update()
    {
        WoodText.text = woodCount.ToString();   
    }

    public void AddWood()
    {
        woodCount++;
    }

    public void RemoveWood(int amount)
    {

    }
}
