using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSlide : MonoBehaviour
{
    public float Distance;
    public float Speed;

    void Update()
    {
        transform.localPosition += Vector3.up * Speed * Time.deltaTime;
        if (transform.localPosition.y > Distance) {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y - Distance - Distance,
                transform.localPosition.z);
        }
    }
}
