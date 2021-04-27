using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
  public int Seed;
  public bool UseSeed;
  public float Height;
  public float MinDistance;
  public float MaxDistance;
  public float DifficultyScaling;

  public ObstacleDefinition[] Obstacles;
  public GameObject RootBehavior;

  public AnimationCurve RotateChance;
  public float RotateSpeedMin;
  public float RotateSpeedMax;
  public AnimationCurve RotationSpeedBias;
  public GameObject RotateBehavior;

  public AnimationCurve SlideChance;
  public AnimationCurve DoubleSlideChance;
  public float SlideSpeedMin;
  public float SlideSpeedMax;
  public AnimationCurve SlideSpeedBias;
  public GameObject SlideBehavior;

  private SpeedController Speed;
  private float Distance;

  void Start()
  {
    if (UseSeed) Random.InitState(Seed);
    Speed = GameObject.FindObjectOfType<SpeedController>();
    Distance = Mathf.Abs(Height / 2f);
  }

  void Update()
  {
    Distance += Speed.Speed * Time.deltaTime;
    if (Distance >= 0)
    {
      var difficulty = CurrentDifficulty();
      var definition = PickObstacle(difficulty);
      var root = Instantiate(RootBehavior, new Vector3(0, Height + Distance, 0), Quaternion.identity);
      root.transform.localRotation = Quaternion.Euler(0, Random.value * 360f, 0);
      if (Random.value <= RotateChance.Evaluate(difficulty))
      {
        root = Instantiate(RotateBehavior, root.transform);
        float value = Mathf.Pow(Random.value, 1f / RotationSpeedBias.Evaluate(difficulty));
        float direction = Random.value < 0.5 ? 1f : -1f;
        root.GetComponent<BasicRotation>().SpinSpeed = Mathf.Lerp(RotateSpeedMin, RotateSpeedMax, value) * direction;
      }
      bool singleSlide = Random.value < SlideChance.Evaluate(difficulty);
      bool doubleSlide = singleSlide && (Random.value < DoubleSlideChance.Evaluate(difficulty));
      bool slidingX = doubleSlide || (singleSlide && (Random.value < 0.5f));
      bool slidingZ = doubleSlide || (singleSlide && !slidingX);
      if (slidingX || slidingZ)
      {
        root = Instantiate(SlideBehavior, root.transform);
        var slide = root.GetComponent<SlideBehavior>();
        slide.TimeOffset = Random.insideUnitSphere * 1024;
        slide.SlideSpeed = new Vector3(
          Mathf.Lerp(SlideSpeedMin, SlideSpeedMax, Mathf.Pow(Random.value, 1f / SlideSpeedBias.Evaluate(difficulty))),
          Mathf.Lerp(SlideSpeedMin, SlideSpeedMax, Mathf.Pow(Random.value, 1f / SlideSpeedBias.Evaluate(difficulty))),
          Mathf.Lerp(SlideSpeedMin, SlideSpeedMax, Mathf.Pow(Random.value, 1f / SlideSpeedBias.Evaluate(difficulty))));
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

  private float CurrentDifficulty()
  {
    return 1f - 1f / (DifficultyScaling * Time.time + 1f);
  }

  private ObstacleDefinition PickObstacle(float difficulty)
  {
    float total = 0f;
    foreach (var def in Obstacles)
    {
      total += def.Weight.Evaluate(difficulty);
    }
    var value = Random.value * total;
    foreach (var def in Obstacles)
    {
      value -= def.Weight.Evaluate(difficulty);
      if (value <= 0)
      {
        return def;
      }
    }
    return Obstacles[0];
  }
}
