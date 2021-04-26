using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public float Timeout;

  public UnityEngine.Events.UnityEvent OnHit;
  public UnityEngine.Events.UnityEvent OnCheckpoint;

  private SpeedController Speed;
  private Vector3 positionLast;
  private bool collision;
  private float timer;
  private float checkpointTimer;
  private int cutoutLayer;

  void Start()
  {
    Speed = GameObject.FindObjectOfType<SpeedController>();
    positionLast = transform.position;
    collision = false;
    timer = Timeout;
    checkpointTimer = Timeout;
    cutoutLayer = LayerMask.NameToLayer("NegativeSpace");
  }

  void Update()
  {
    positionLast += Vector3.up * Speed.Speed * Time.deltaTime;
    var offset = transform.position - positionLast;
    if (offset.sqrMagnitude <= 0f) return;
    var hits = Physics.RaycastAll(positionLast, offset.normalized, offset.magnitude);
    var cutouts = hits.Where(h => h.collider.gameObject.layer == cutoutLayer).ToArray();
    collision = hits.Any(h => cutouts.All(cutout => !cutout.transform.IsChildOf(h.transform)));

    if (hits.Any(h => h.collider.gameObject.tag == "Respawn"))
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

    positionLast = transform.position;
  }

  public bool GetCollision()
  {
    return collision;
  }
}
