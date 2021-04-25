using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
  public int Seed;
  public float Height;
  public float MinDistance;
  public float MaxDistance;

  public ObstacleDefinition[] Obstacles;
  public GameObject RootBehavior;

  private SpeedController Speed;
  private float Distance;

  void Start()
  {
    Random.InitState(Seed);
    Speed = GameObject.FindObjectOfType<SpeedController>();
    Distance = Mathf.Abs(Height / 2f);
  }

  void Update()
  {
    Distance += Speed.Speed * Time.deltaTime;
    if (Distance >= 0)
    {
      var definition = Obstacles[Random.Range(0, Obstacles.Length)];
      var root = Instantiate(RootBehavior, new Vector3(0, Height + Distance, 0), Quaternion.identity);
      root.transform.localRotation = Quaternion.Euler(0, Random.value * 360f, 0);
      var solid = Instantiate(definition.Prefab, root.transform);
      solid.transform.localPosition = new Vector3(Random.Range(-1, 1) * definition.MaxTranslation.x, 0, Random.Range(-1, 1) * definition.MaxTranslation.y);
      solid.transform.localScale = new Vector3(1, 1, 1) * Random.Range(definition.MinScale, definition.MaxScale);
      Distance -= Random.Range(MinDistance, MaxDistance);
    }
  }
}
