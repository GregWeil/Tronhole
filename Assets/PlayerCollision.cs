using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public float radius;

  private bool collision;

  void Start()
  {
    collision = false;
  }

  void Update()
  {
    var collisionLast = collision;
    var colliders = Physics.OverlapSphere(transform.position, radius);
    collision = colliders.Length > 0;

    if (collision && !collisionLast)
    {
      GetComponent<AudioSource>().Play();
      var fog = GameObject.FindObjectOfType<FogController>();
      fog.HurtPulse();
    }
  }
}
