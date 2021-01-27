using UnityEngine;

public class DroppedObject : MonoBehaviour
{
   public bool IsDropped { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsDropped)
        {
            IsDropped = true;
        }
    }
}
