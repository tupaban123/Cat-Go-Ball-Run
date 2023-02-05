using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : Prop
{
    [Header("Components")]
    [SerializeField] private Transform leftBound;
    [SerializeField] private Transform rightBound;
    [SerializeField] private ShortBridge shortBridge;
    [SerializeField] private LargeBridge largeBridge;

    [Header("Settings")] 
    [SerializeField] private float bridgeOffset;
    
    private Bridge _bridge;
    private Camera _cam;
    
    public ShortBridge ShortBridge
    {
        get { return shortBridge; }
        private set {}
    }
    
    public LargeBridge LargeBridge
    {
        get { return largeBridge; }
        private set {}
    }
    
    private void Start()
    {
        _cam = Camera.main;
    }

    public override void Init(Vector2 pos)
    {
        base.Init(pos);
    }

    public void SetBridge(Bridge bridge)
    {
        _bridge = bridge;
    }

    public Vector2 GetPosForBridge()
    {
        var yPos = leftBound.position.y + bridgeOffset;
        var xPos = Random.Range(leftBound.position.x, rightBound.position.x);

        return new Vector2(xPos, yPos);
    }

    public void RemoveBridge()
    {
        _bridge.gameObject.SetActive(false);
        _bridge = null;
    }
    
    public bool IsUnderScreen()
    {
        if (_bridge == null)
            return false;

        var bridgeTopPos = _bridge.Top.position;

        var bridgeTopScreenPos = _cam.WorldToScreenPoint(bridgeTopPos);
        
        if(bridgeTopScreenPos.y < -50)
        {
            return true;
        }

        return false;
    }
}
