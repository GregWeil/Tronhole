using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
  public UnityEngine.UI.Image Fill;
  public float TargetOpacity;
  public float Speed;
  public float Delay;

  void Update()
  {
    Delay -= Time.unscaledDeltaTime;
    if (Delay < 0f)
    {
      var color = Fill.color;
      color.a = Mathf.MoveTowards(color.a, TargetOpacity, Speed * Time.unscaledDeltaTime);
      Fill.color = color;
    }
  }
}
