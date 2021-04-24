using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
  private SpeedController Speed;

  void Start()
  {
    Speed = GameObject.FindObjectOfType<SpeedController>();
  }

  // Update is called once per frame
  void Update()
  {
    transform.localPosition += Vector3.up * Speed.Speed * Time.deltaTime;
  }
}
