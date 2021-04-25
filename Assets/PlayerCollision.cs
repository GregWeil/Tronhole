using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public float radius;

  private bool collision;
  private int cutoutLayer;

  void Start()
  {
    collision = false;
    cutoutLayer = LayerMask.NameToLayer("NegativeSpace");
  }

  void Update()
  {
    var collisionLast = collision;
    var colliders = Physics.OverlapSphere(transform.position, radius);
    var cutouts = colliders.Where(c => c.gameObject.layer == cutoutLayer).ToArray();
    collision = colliders.Any(c => cutouts.All(cutout => !cutout.transform.IsChildOf(c.transform)));

    if (collision && !collisionLast && Time.timeScale > 0)
    {

      GetComponent<AudioSource>().Play();
      var fog = GameObject.FindObjectOfType<FogController>();
      fog.HurtPulse();
    }
  }

  public bool GetCollision()
  {
    return collision;
  }
}
