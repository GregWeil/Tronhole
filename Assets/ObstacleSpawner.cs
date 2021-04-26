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

  public float RotateChance;
  public float RotateSpeedMin;
  public float RotateSpeedMax;
  public GameObject RotateBehavior;

  public float SlideChance;
  public float SlideSpeedMin;
  public float SlideSpeedMax;
  public GameObject SlideBehavior;

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
      var definition = PickObstacle();
      var root = Instantiate(RootBehavior, new Vector3(0, Height + Distance, 0), Quaternion.identity);
      root.transform.localRotation = Quaternion.Euler(0, Random.value * 360f, 0);
      if (Random.value <= RotateChance)
      {
        root = Instantiate(RotateBehavior, root.transform);
        root.GetComponent<BasicRotation>().SpinSpeed = Random.Range(RotateSpeedMin, RotateSpeedMax) * (Random.value < 0.5 ? 1f : -1f);
      }
      bool slidingX = Random.value < SlideChance;
      bool slidingZ = Random.value < SlideChance;
      if (slidingX || slidingZ)
      {
        root = Instantiate(SlideBehavior, root.transform);
        var slide = root.GetComponent<SlideBehavior>();
        slide.TimeOffset = Random.insideUnitSphere * 1024;
        slide.SlideSpeed = new Vector3(
          Random.Range(SlideSpeedMin, SlideSpeedMax),
          Random.Range(SlideSpeedMin, SlideSpeedMax),
          Random.Range(SlideSpeedMin, SlideSpeedMax));
        slide.SlideRange = new Vector3(slidingX ? definition.MaxTranslation.x : 0, 0, slidingZ ? definition.MaxTranslation.y : 0);
      }
      var solid = Instantiate(definition.Prefab, root.transform);
      solid.transform.localRotation = definition.LimitRotation ? Quaternion.Euler(0, Random.value * 360f, 0) : Random.rotationUniform;
      solid.transform.localPosition = new Vector3(
        slidingX ? 0 : Random.Range(-1, 1) * definition.MaxTranslation.x,
        0,
        slidingZ ? 0 : Random.Range(-1, 1) * definition.MaxTranslation.y);
      solid.transform.localScale = new Vector3(1, 1, 1) * Random.Range(definition.MinScale, definition.MaxScale);
      Distance -= Random.Range(MinDistance, MaxDistance);
    }
  }

  private ObstacleDefinition PickObstacle()
  {
    float total = 0f;
    foreach (var def in Obstacles)
    {
      total += def.Weight;
    }
    var value = Random.value * total;
    foreach (var def in Obstacles)
    {
      value -= def.Weight;
      if (value <= 0)
      {
        return def;
      }
    }
    return Obstacles[0];
  }
}
