using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtHeight : MonoBehaviour
{
  public float Height;

  void Update()
  {
    if (transform.position.y > Height)
    {
      Destroy(gameObject);
    }
  }
}
