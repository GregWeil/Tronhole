using UnityEngine;

[System.Serializable]
public class ObstacleDefinition
{
  public float Weight;
  public GameObject Prefab;
  public Vector2 MaxTranslation;
  public float MinScale;
  public float MaxScale;
  public bool LimitRotation;
}
