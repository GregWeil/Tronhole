using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
  public Transform LookOrientation;
  public Color Color;
  public float Density;
  public Color UpColor;
  public float UpDensity;
  public Color HurtColor;
  public float HurtDensity;
  public float HurtDecay;

  private float HurtScale;

  void Start()
  {
    HurtScale = 0;
  }

  void Update()
  {
    var scale = Vector3.Angle(LookOrientation.forward, Vector3.up) / 90f;
    scale = Mathf.Clamp(1f - Mathf.Pow(scale, 2f), 0f, 1f);
    var color = Color.Lerp(Color, UpColor, scale);
    var density = Mathf.Lerp(Density, UpDensity, scale);
    RenderSettings.fogColor = Color.Lerp(color, HurtColor, HurtScale);
    RenderSettings.fogDensity = Mathf.Lerp(density, HurtDensity, HurtScale);
    HurtScale -= HurtDecay * Time.deltaTime;
  }

  public void HurtPulse()
  {
    HurtScale = 1;
  }
}
