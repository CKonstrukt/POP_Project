using UnityEngine;

public class DeathObjectHandler : MonoBehaviour
{
    public void DestroyParent()
    {
        transform.parent.GetComponent<DeathParentHandler>().DestroySelf();
    }
}
