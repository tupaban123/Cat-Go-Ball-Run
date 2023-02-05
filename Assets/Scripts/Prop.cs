using UnityEngine;

public class Prop : MonoBehaviour
{
    public virtual void Init(Vector2 pos)
    {
        transform.position = pos;
    }
}
