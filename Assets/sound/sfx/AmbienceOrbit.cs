using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceOrbit : MonoBehaviour
{
  public float radius;
  public float angularSpeed;
  public float height;

  void Update()
  {
    var position = Vector3.forward * radius;
    position = Quaternion.Euler(0, angularSpeed * Time.time, 0) * position;
    transform.localPosition = position + Vector3.up * height;
  }
}
