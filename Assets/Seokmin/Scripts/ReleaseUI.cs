using UnityEngine;
using UnityEngine.InputSystem;

public class ReleaseUI : MonoBehaviour
{
    private Camera _cam;
    private Vector2 _mousePos;
    private float _targetPos;
    public bool _Rel = false;
    private float _xSize;
    
    private void Awake()
    {
        _cam = Camera.main;
        _xSize = UIManager._instance._testUI._xSize;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && UIManager._instance._testUI._connection)
        {
            _mousePos = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            _targetPos = Mathf.Sqrt(Mathf.Pow(_mousePos.x - UIManager._instance._testUI._target.position.x, 2)
                                    + Mathf.Pow(_mousePos.y - UIManager._instance._testUI._target.position.y, 2));
            _Rel = _targetPos < 0.75f;
        }

        if (_Rel)
        {
            UIManager._instance._testUI.Release();
        }
    }
}
