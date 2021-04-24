using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
  public int Seed;
  public float Height;
  public float MinDistance;
  public float MaxDistance;

  public GameObject[] Solids;
  public GameObject RootBehavior;

  private SpeedController Speed;
  private float Distance;

  void Start()
  {
    Random.InitState(Seed);
    Speed = GameObject.FindObjectOfType<SpeedController>();
    Distance = 0f;
  }

  void Update()
  {
    Distance += Speed.Speed * Time.deltaTime;
    if (Distance >= 0)
    {
      var root = Instantiate(RootBehavior, new Vector3(0, Height + Distance, 0), Quaternion.identity);
      var solid = Instantiate(Solids[Random.Range(0, Solids.Length)], root.transform);
      solid.transform.localPosition = Random.insideUnitSphere;
      solid.transform.localRotation = Quaternion.Euler(0, Random.value * 360f, 0);
      Distance -= Random.Range(MinDistance, MaxDistance);
    }
  }
}
