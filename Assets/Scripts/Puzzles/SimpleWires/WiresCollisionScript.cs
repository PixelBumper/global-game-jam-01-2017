using UnityEngine;

public class WiresCollisionScript : MonoBehaviour
{
    public SimpleWireType simpleWireType;

    private void OnMouseDown()
    {
        GetComponentInParent<SimpleWires>().OnWireClick(simpleWireType);
    }
}
