using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _instance {get; private set;}
    public TestUI _testUI;
    public ReleaseUI _releaseUI;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        _instance = this;
        _releaseUI = GetComponent<ReleaseUI>();
    }
}
