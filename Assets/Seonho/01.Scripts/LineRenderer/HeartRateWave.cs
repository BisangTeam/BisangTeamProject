using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Seonho._01.Scripts.LineRenderer
{
    public class HeartRateWave : MonoBehaviour
    {
        public UnityEngine.LineRenderer lineRenderer;
        
        [SerializeField] private Transform _startPointTrm;
        [SerializeField] private Transform _endPointTrm;
        
        [SerializeField] private float _currentPositionX;
        [SerializeField] private float _rollbackTime;
        
        public bool _end { get; private set; }
        
        private void Awake()
        {
            Initialize();
        }
        
        void Initialize()
        {
            lineRenderer = GetComponent<UnityEngine.LineRenderer>();
            
            lineRenderer.useWorldSpace = true;
        }
    
        public void InitializeLineRenderer(int count)
        {
            lineRenderer.positionCount = 0;

            AddLinePositionCount(count);

            float distance = Mathf.Abs(_startPointTrm.position.x - _endPointTrm.position.x) /
                             (lineRenderer.positionCount - 1); // (최대 길이) / (꼭짓점 개수)

            _currentPositionX = _startPointTrm.position.x;

            lineRenderer.SetPosition(0, new Vector3(_currentPositionX, 0, 0));

            for (int i = 1; i < lineRenderer.positionCount; i++)
            {
                _currentPositionX += distance;
                lineRenderer.SetPosition(i, new Vector3(_currentPositionX, 0, 0));
            }
        }

        private void Update()
        {
            return;
            
            for (int i = 1; i < lineRenderer.positionCount - 1; i++)
            {
                ResetLine(i);
            }
        }
        
        public void AddLinePositionCount(int addCount)
        {
            lineRenderer.positionCount += addCount;
        }

        public void ChangeLine(int index, float value)
        {
            lineRenderer.SetPosition(index, new Vector3(lineRenderer.GetPosition(index).x, value, lineRenderer.GetPosition(index).z));
        }

        public void ResetLine(int index)
        {
            Vector3 currentPoint = lineRenderer.GetPosition(index);

            DOTween.To(() => currentPoint.y, y =>
                {
                    currentPoint.y = y;
                    lineRenderer.SetPosition(index, currentPoint);
                },
                0f,
                _rollbackTime
            ).SetEase(Ease.OutQuint);
        }
    }
}
