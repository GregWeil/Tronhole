using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  public float Timeout;
  public float Radius;

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
  }

  void LateUpdate()
  {
    var colliders = GetColliding();
    var cutouts = colliders.Where(c => c.gameObject.layer == cutoutLayer).ToArray();
    collision = colliders.Any(c => cutouts.All(cutout => !cutout.transform.IsChildOf(c.transform)));

    if (colliders.Any(c => c.gameObject.tag == "Respawn"))
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

  private IEnumerable<Collider> GetColliding()
  {
    var cols = Physics.OverlapSphere(transform.position, Radius);
    var offset = positionLast - transform.position;
    if (offset.sqrMagnitude <= 0f) return cols;
    var hits = Physics.SphereCastAll(transform.position, Radius, offset.normalized, offset.magnitude);
    return hits.Select(h => h.collider).Concat(cols);
  }

  public bool GetCollision()
  {
    return collision;
  }
}
