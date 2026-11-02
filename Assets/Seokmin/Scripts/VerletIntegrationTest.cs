using UnityEngine;

public class VerletIntegrationTest
{
    public Vector3 _pos;
    private Vector3 _prePos;
    private Vector3 _nextPos;
    private bool _isPinned;
    private float _damping; //감속 계수 감쇠 계수
    private Vector3 _startPos;
    private float _springStrength;
    private Vector3 _dir;
    private Vector3 _acc;
    
    

    public VerletIntegrationTest(Vector3 _startPositon, float _damping, float _springStrength)
    {
        _pos = _startPositon;
        _prePos = _pos;
        this._damping =  _damping;
        _startPos = _startPositon;
        this._springStrength = _springStrength;
    }

    public Vector3 Simulate(Vector3 _movePos)
    {
        _dir = _movePos - _pos;
        _acc = _dir * _springStrength;
        _nextPos = _pos + (_pos - _prePos) * _damping + _acc * Mathf.Pow(Time.deltaTime, 2);
        _prePos = _pos;
        _pos = _nextPos;
        return _pos;
    }
}
