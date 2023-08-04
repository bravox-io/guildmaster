using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeritems : MonoBehaviour
{

    [Header("Amounts")]
    public int totalWood;
    public int carrots; 
    public float currentWater;
    public int fishes;
    
    [Header("Limits")]
    public float waterLimit = 50f;
    public float woodLimit = 50f;
    public float CarrotLimit = 50f;
    public float fishesLimited = 3f;

    public void WaterLimit(float water)
    {
        if(currentWater <= waterLimit)
        {
            currentWater += water;
        }

    }
}
