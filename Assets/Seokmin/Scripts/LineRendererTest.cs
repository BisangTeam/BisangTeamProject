using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LineRendererTest : MonoBehaviour
{
    private LineRenderer _lR;
    private int _lineCount = 0;
    private float _maxLineSize = 3f;
    private bool _isClick = false;
    private Camera _cam;
    //private float _lineSize = 1f;
    private Vector3 _mousePos;
    private List<VerletIntegrationTest> _points = new List<VerletIntegrationTest>();
    [SerializeField] private float _damping;
    [SerializeField] private float _springStrength;
    [SerializeField] private int _pointCount;
    private float _t;
    private float _dis;
    private Vector3 _dir;
    private float _error;
    private float _restLength;
    private float _bendRestLength;
    private Vector3 _correction;
    [SerializeField, Range(0f,1f)] 
    private float _stiffness;
    [SerializeField, Range(0f,1f)] 
    private float _bendStiffness;
    [SerializeField] private float _constraintIterations;
    private Vector3 _startPos;

    private void Awake()
    {
        _lR = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _mousePos.z = 0;
            _startPos =  _mousePos;
            _lR.SetPosition(0,_mousePos);
            for (int i = 0; i < _pointCount; ++i)
            {
                _points.Add(new VerletIntegrationTest(_mousePos,_damping,_springStrength));
            }
            _isClick = true;
            _lR.SetPosition(0,_mousePos);
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _points.Clear();
            _isClick = false;
        }
        if (_isClick)
        {
            _mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _mousePos.z = 0;
            //_restLength = (_mousePos - _points[0]._pos).magnitude / (_points.Count - 1);
            //_bendRestLength = _restLength * 2;
            _points[_points.Count - 1]._pos = _mousePos; //너는 또 왜안되????? 진짜 잠좀 자자 새벽 3시인데
            DistanceCon(); //새벽 4시 가까이 되니 미치겠다 내가 이런 주석은 왜쓰는건지도 모르겠다 그냥 쓴다 이무진 노래 잘한다
            //부모님 깨시면 욕먹겠지 내일 12시 비행기라 9시에는 일어나야하는데
            BendCon(); //더 자연스러워지기 위한 추가 계산
        }
        /*if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _mousePos.z = 0;
            transform.position = _mousePos;
            _isClick = true;
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _isClick = false;
        }

        if (_isClick)
        {
            _mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _mousePos.z = 0;
            _lR.SetPosition(_lineCount, _mousePos - transform.position);
            if(_lineCount > 0)
                _lineSize = Mathf.Sqrt(Mathf.Pow(_lR.GetPosition(_lineCount).x - _lR.GetPosition(_lineCount - 1).x,2) 
                                       + Mathf.Pow(_lR.GetPosition(_lineCount).y - _lR.GetPosition(_lineCount - 1).y,2));
            else
                _lineSize = Mathf.Sqrt(Mathf.Pow(_lR.GetPosition(_lineCount).x,2)
                                       + Mathf.Pow(_lR.GetPosition(_lineCount).y,2));
            if (_lineSize > _maxLineSize)
            {
                _lR.positionCount += 1;
                _lineCount++;
                _lR.SetPosition(_lineCount, _mousePos - transform.position);
            }
        }*/
    }

    private void LateUpdate()
    {
        if (_isClick)
        {
            Fuck(); //왜 클릭후 다시하면 오류가 뜰까 정말 새벽 3시에 코딩하는데 미치겠다
        }
    }

    private void Fuck() //List인덱스 왜뜨지????
    {
        for (int i = 0; i < _points.Count; ++i)
        {
            _lR.SetPosition(i, _points[i]._pos);
        }
    }

    private void DistanceCon() //고통의 원인 1 자연스러운 혈관 움직임 구현이 되기 위한 계산하는 곳
    { 
        for (int i = 1; i < _points.Count; ++i)
        {
            //_t = i / (float)(_pointCount - 1);
            //Vector3.Lerp(_points[0]._pos,_points[_pointCount - 1]._pos,_t);
            //Vector3.Lerp(_points[0]._pos, _mousePos, _t);
            _dir = _points[i]._pos - _points[i - 1]._pos;
            _dis = _dir.magnitude; // magnitude = Vector길이 구함 피타고라스 사용
            if (_dis < 0.000001f)
                continue;
                
            _error = _dis - _restLength;
            _correction = _dir.normalized * _error * _stiffness; //보정값
            if (i == 1)
            {
                _points[i]._pos -= _correction;
            }
            else
            {
                _points[i - 1]._pos += _correction * 0.5f;
                _points[i]._pos -= _correction * 0.5f;
            }
        }
    }

    private void BendCon()
    {
        for (int i = 0; i < _points.Count - 2; ++i)
        {
            _dir = _points[i + 2]._pos - _points[i]._pos;
            _dis = _dir.magnitude;
            if (_dis < 0.000001f)
                continue;
            
            _error = _dis - _bendRestLength;
            _correction = (_dir / _dis) * _error * _bendStiffness;
            if (i == 0)
            {
                _points[i + 2]._pos -= _correction;
            }
            else
            {
                _points[i]._pos += _correction * 0.5f;
                _points[i + 2]._pos -= _correction * 0.5f;
            }
        }
    }
}
