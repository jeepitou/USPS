using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionEvent : UnityEvent<Collider2D>
{
}


public class EventOnCollisionEnter : MonoBehaviour
{
    public CollisionEvent collisionEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionEvent.Invoke(collision.gameObject.GetComponent<Collider2D>());
    }
}
