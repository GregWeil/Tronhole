using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotation : MonoBehaviour
{
  public float SpinSpeed;

  void Update()
  {
    transform.localRotation *= Quaternion.Euler(0, SpinSpeed * Time.deltaTime, 0);
  }
}
