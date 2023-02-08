using UnityEngine;

public class Bridge : Prop
{
    [SerializeField] private BridgeType bridgeType;

    [SerializeField] private Transform topBridge;
    [SerializeField] private Transform bottomBridge;
    
    public BridgeType BridgeType
    {
        get { return bridgeType; }
        private set {}
    }
    
    public Transform Top
    {
        get { return topBridge; }
        private set {}
    }
    
    public Transform Bottom
    {
        get { return bottomBridge; }
        private set {}
    }

    public void InitZEulerAngle(float angle)
    {
        var eulerAngles = transform.localEulerAngles;

        transform.localEulerAngles = new Vector3(eulerAngles.x, eulerAngles.y, angle);
    }

    public override void Init(Vector3 pos)
    {
        transform.localPosition = pos;
    }
}
