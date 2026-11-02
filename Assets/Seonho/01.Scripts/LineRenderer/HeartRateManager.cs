using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Seonho._01.Scripts.LineRenderer
{
    public class HeartRateManager : MonoBehaviour
    {
        [SerializeField] private HeartRate _heartRate1;
        [SerializeField] private HeartRateWave _heartRateWave;
        [SerializeField] private HeartRate _heartRate2;
        
        [SerializeField] private HeartWaveRateListSO _heartWaveRateListSO;

        [SerializeField] private int _heartCount;
        [SerializeField] private int _heartWaveCount;

        [SerializeField] private bool _onMonitoring;

        private void Awake()
        {
            _heartRate1 = transform.GetChild(0).GetComponentInChildren<HeartRate>();
            _heartRateWave = GetComponentInChildren<HeartRateWave>();
            _heartRate2 = transform.GetChild(2).GetComponentInChildren<HeartRate>();

            Initialize();

            _heartRate1.StartCoroutine(_heartRate1.ComingLineLogic());
        }

        private void Update()
        {
            if (_heartRate1.end && !_onMonitoring)
            {
                _onMonitoring = true;
                HeartWave();
            }
        }
        
        void HeartWave()
        {
            int randomValue = Random.Range(0, _heartWaveRateListSO.list.Length);

            for (int i = 1; i < _heartRateWave.lineRenderer.positionCount - 1; i++)
            {
                _heartRateWave.ChangeLine(i, _heartWaveRateListSO.list[randomValue].value[i - 1]);
                _heartRateWave.ResetLine(i);
            }

            _heartRate2.StartCoroutine(_heartRate2.ComingLineLogic());
        }

        void Initialize()
        {
            _heartRate1.InitializeLineRenderer(_heartCount);
            _heartRate2.InitializeLineRenderer(_heartCount);
            _heartRateWave.InitializeLineRenderer(_heartWaveCount);
        }
    }
}
