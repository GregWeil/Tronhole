using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeCollider : MonoBehaviour
{
    public GameObject Collider;
    public float Count;

    void Start()
    {
        for (var i = 0; i < Count; ++i) {
            var c = Instantiate(Collider,
                Collider.transform.position,
                Collider.transform.rotation,
                Collider.transform.parent);
            var rotation = Quaternion.Euler(0, i * 360f / Count, 0);
            c.transform.localPosition = rotation * c.transform.localPosition;
            c.transform.localRotation = rotation * c.transform.localRotation;
        }
        Destroy(Collider);
    }
}
