using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Update = UnityEngine.PlayerLoop.Update;

namespace Seonho._01.Scripts.LineRenderer
{
    public class HeartRate : MonoBehaviour
    {
        public UnityEngine.LineRenderer lineRenderer;

        [SerializeField] private Transform _endPointTrm;

        [SerializeField] private int _currentDoingLine;

        [SerializeField] private float _currentPositionX;
        [SerializeField] private float _goingTime;
        [SerializeField] private float _rollbackTime;

        public bool end { get; private set; }

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

            float distance = Mathf.Abs(_endPointTrm.position.x - transform.position.x) /
                             (lineRenderer.positionCount - 1); // (최대 길이) / (꼭짓점 개수)

            _currentPositionX = transform.position.x;

            lineRenderer.SetPosition(0, new Vector3(_currentPositionX, 0, 0));

            for (int i = 1; i < lineRenderer.positionCount; i++)
            {
                _currentPositionX += distance;
                lineRenderer.SetPosition(i, new Vector3(_currentPositionX, 0, 0));
            }
        }

        void AddLinePositionCount(int addCount)
        {
            lineRenderer.positionCount += addCount;
        }

        public IEnumerator ComingLineLogic()
        {
            ComingLine(_currentDoingLine);
            _currentDoingLine++;
            yield return new WaitForSeconds(_goingTime);

            if (_currentDoingLine < lineRenderer.positionCount - 1)
                StartCoroutine(ComingLineLogic());
            else
            {
                _currentDoingLine = 0;
                end = true;
            }
        }
        
        void ComingLine(int index)
        {
            Vector3 currentPoint = lineRenderer.GetPosition(index);

            DOTween.To(() => currentPoint.y, y =>
                {
                    currentPoint.y = y;
                    lineRenderer.SetPosition(index, currentPoint);
                }
                , 0.1f
                , _rollbackTime
            ).OnComplete(() => DOTween.To(() => currentPoint.y, y =>
                    {
                        currentPoint.y = y;
                        lineRenderer.SetPosition(index, currentPoint);
                    }
                    , 0
                    , _rollbackTime
                )
            );
        }

        public void NotEnd()
        {
            end = false;
        }
    }
}
