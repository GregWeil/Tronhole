using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSlide : MonoBehaviour
{
  public GameObject Surface;
  public float ModelHeight;
  public float TextureTiling;

  private SpeedController Controller;

  void Start()
  {
    Controller = GameObject.FindObjectOfType<SpeedController>();
  }

  void Update()
  {
    float distance = ModelHeight / TextureTiling;
    transform.localPosition += Vector3.up * Controller.Speed * Time.deltaTime;
    if (transform.localPosition.y > distance)
    {
      transform.localPosition = new Vector3(
        transform.localPosition.x,
        transform.localPosition.y - distance - distance,
        transform.localPosition.z);

      if (Surface != null)
      {
        Destroy(Surface);
        Surface = null;
      }
    }
  }
}
