using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBehavior : MonoBehaviour
{
  public Vector3 SlideSpeed;
  public Vector3 SlideRange;
  public Vector3 TimeOffset;

  private float BaseTime;

  void Start()
  {
    BaseTime = Time.time;
  }

  // Update is called once per frame
  void Update()
  {
    var time = Vector3.Scale(new Vector3(1, 1, 1) * (Time.time - BaseTime) + TimeOffset, SlideSpeed);
    transform.localPosition = Vector3.Scale(new Vector3(Mathf.Sin(time.x), Mathf.Sin(time.y), Mathf.Sin(time.z)), SlideRange);
  }
}
