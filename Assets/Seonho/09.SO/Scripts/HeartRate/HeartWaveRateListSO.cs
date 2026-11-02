using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HeartWaveRateListSO", menuName = "SO/HeartWaveRateListSO")]
public class HeartWaveRateListSO : ScriptableObject
{
    public int valueCount;
    
    public HeartRateWaveDataSO[] list;
}
