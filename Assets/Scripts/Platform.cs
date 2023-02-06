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
    [HideInInspector] public bool isGivingMoney;
    
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
        largeBridge.transform.localPosition = new Vector3(largeBridge.transform.localPosition.x, largeBridge.transform.localPosition.y, 0);
        shortBridge.transform.localPosition = new Vector3(shortBridge.transform.localPosition.x, shortBridge.transform.localPosition.y, 0);
    }

    public override void Init(Vector3 pos)
    {
        base.Init(pos);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
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
