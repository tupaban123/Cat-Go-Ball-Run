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

        if (bridge.BridgeType == BridgeType.Large)
        {
            bridge.InitZEulerAngle(Random.Range(-35, 35));
            
            var topBridgePointPos = bridge.Top.position;
            var bottomBridgePointPos = bridge.Bottom.position;

            var leftBoundPos = leftBound.position;
            var rightBoundPos = rightBound.position;

            var bridgeLocalPos = bridge.transform.localPosition;

            if (bottomBridgePointPos.x < leftBoundPos.x || topBridgePointPos.x < leftBoundPos.x)
            {
                var distance = Mathf.Abs(leftBoundPos.x - bottomBridgePointPos.x);

                bridge.transform.localPosition =
                    new Vector3(bridgeLocalPos.x + distance, bridgeLocalPos.y, bridgeLocalPos.z);
                
                Debug.Log($"Large bridge fix < left. {gameObject.name}\nDistance {distance}");
            }
            else if (bottomBridgePointPos.x > rightBoundPos.x || topBridgePointPos.x > rightBoundPos.x)
            {
                var distance = Mathf.Abs(rightBoundPos.x - bottomBridgePointPos.x);
                
                bridge.transform.localPosition =
                    new Vector3(bridgeLocalPos.x - distance, bridgeLocalPos.y, bridgeLocalPos.z);
                
                Debug.Log($"Large bridge fix > right. {gameObject.name}\nDistance {distance}");
            }
        }
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
