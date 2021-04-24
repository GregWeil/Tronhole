using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour
{
  public float Speed;
  public float Acceleration;

  void LateUpdate()
  {
    Speed += Acceleration * Time.deltaTime;
  }
}
