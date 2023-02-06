using UnityEngine;

public class Prop : MonoBehaviour
{
    public virtual void Init(Vector3 pos)
    {
        transform.position = pos;
    }
}
