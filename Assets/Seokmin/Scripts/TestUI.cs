using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestUI : MonoBehaviour
{
    private Camera _cam;
    private Vector2 _endPoint;
    private Vector2 _startPoint;
    public float _xSize = 1f;
    private float _targetSize = 1f;
    private float _targetXSize = 1f;
    private float _dir;
    private float _dirRad;
    private bool _isMouse = false;
    public Transform _target;
    public bool _connection = false;
    private bool _oneClick = true;
    private float _relSpeed = 2f;
    
    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        
        if (Mouse.current.leftButton.isPressed && !_connection)
        {
            _isMouse = true;
            if (_oneClick)
            {
                _startPoint = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
                _targetXSize = Mathf.Sqrt(Mathf.Pow(_target.position.x - _startPoint.x, 2) + Mathf.Pow(_target.position.y - _startPoint.y, 2));
                _oneClick = false;
            }
        }
        else
        {
            _isMouse = false;
            _oneClick = true;
        }

        if (_isMouse)
        {
            _endPoint = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _xSize = Mathf.Sqrt(Mathf.Pow(_endPoint.x - _startPoint.x, 2) + Mathf.Pow(_endPoint.y - _startPoint.y, 2));
            _targetSize = Mathf.Sqrt(Mathf.Pow(_endPoint.x - _target.position.x, 2) +
                                     Mathf.Pow(_endPoint.y - _target.position.y, 2));
            if (_targetSize < 0.75f)
            {
                _endPoint = _target.position;
                _xSize = _targetXSize;
            }
            transform.localScale = new Vector3(_xSize, 0.5f, 1);
            _dir = Mathf.Atan2(_endPoint.y - _startPoint.y, _endPoint.x - _startPoint.x) * Mathf.Rad2Deg;
            _dirRad = Mathf.Atan2(_endPoint.y - _startPoint.y, _endPoint.x - _startPoint.x);
            transform.rotation = Quaternion.Euler(0f, 0f, _dir);
            transform.position = new Vector3((_endPoint.x + _startPoint.x) / 2, (_endPoint.y + _startPoint.y) / 2, 0);
        }
        else if(!_connection && !_isMouse)
        {
            if (_targetSize < 0.75f)
            {
                _connection = true;
            }
            else
            {
                Reset();
            }
        }

        if (UIManager._instance._releaseUI._Rel)
        {
            Release();
        }
    }

    public void Release()
    {
        _xSize -= _relSpeed * Time.deltaTime;
        transform.localScale = new Vector3(_xSize, 0.5f, 1);
        transform.position = new Vector3((_xSize * Mathf.Cos(_dirRad)) / 2 + _startPoint.x ,(_xSize * Mathf.Sin(_dirRad)) / 2 + _startPoint.y, 0);
        if (_xSize < 0f)
        {
            Reset();
        }
        _relSpeed *= 1.01f;
    }

    public void Reset()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localScale = new Vector3(0f, 0f, 0f);
        _connection = false;
        _isMouse = false;
        _targetSize = 1f;
        _oneClick = true;
        UIManager._instance._releaseUI._Rel = false;
        _relSpeed = 2f;
    }
}
