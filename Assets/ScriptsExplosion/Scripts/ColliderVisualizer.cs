using UnityEngine;

public class ColliderVisualizer : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        if (boxCollider == null)
            return;

        Gizmos.DrawWireCube(boxCollider.bounds.center,boxCollider.bounds.size);
    }
}