using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public float Radius;
  public float Timeout;

  public UnityEngine.Events.UnityEvent OnHit;
  public UnityEngine.Events.UnityEvent OnCheckpoint;

  private bool collision;
  private float timer;
  private float checkpointTimer;
  private int cutoutLayer;

  void Start()
  {
    collision = false;
    timer = Timeout;
    checkpointTimer = Timeout;
    cutoutLayer = LayerMask.NameToLayer("NegativeSpace");
  }

  void Update()
  {
    var colliders = Physics.OverlapSphere(transform.position, Radius);
    var cutouts = colliders.Where(c => c.gameObject.layer == cutoutLayer).ToArray();
    collision = colliders.Any(c => cutouts.All(cutout => !cutout.transform.IsChildOf(c.transform)));

    if (colliders.Any(c => c.tag == "Respawn"))
    {
      if (checkpointTimer <= 0)
      {
        OnCheckpoint.Invoke();
        checkpointTimer = Timeout;
        timer = Timeout;
      }
    }
    else if (collision && timer <= 0)
    {
      GetComponent<AudioSource>().Play();
      OnHit.Invoke();
      timer = Timeout;
    }
    timer -= Time.deltaTime;
    checkpointTimer -= Time.deltaTime;
  }

  public bool GetCollision()
  {
    return collision;
  }
}
