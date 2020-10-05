using UnityEngine;


public class GameObjectDestroyer : MonoBehaviour
{
    public void EVENT_DestroyObject()
    {
        Destroy(gameObject);
    }
}
