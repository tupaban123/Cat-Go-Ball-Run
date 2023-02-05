using UnityEngine;

public class ShortBridge : Bridge
{
    public override void Init(Vector2 pos)
    {
        base.Init(pos);
        //Transform.localPosition = pos;
    }
}

public enum BridgeType
{
    Short,
    Large
}