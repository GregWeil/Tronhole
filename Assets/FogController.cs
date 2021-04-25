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
  public float UpTime;
  public Color HurtColor;
  public float HurtDensity;
  public float HurtDecay;

  private float HurtScale;
  private float UpScale;

  void Start()
  {
    HurtScale = 0;
    UpScale = 0;
  }

  void Update()
  {
    var scale = Vector3.Angle(LookOrientation.forward, Vector3.up) / 90f;
    scale = Mathf.Clamp(1f - Mathf.Pow(scale, 2f), 0f, 1f) * UpScale;
    var color = Color.Lerp(Color, UpColor, scale);
    var density = Mathf.Lerp(Density, UpDensity, scale);
    RenderSettings.fogColor = Color.Lerp(color, HurtColor, HurtScale);
    RenderSettings.fogDensity = Mathf.Lerp(density, HurtDensity, HurtScale);
    Camera.main.backgroundColor = RenderSettings.fogColor;
    HurtScale -= HurtDecay * Time.deltaTime;
    UpScale = Mathf.Min(1f, UpScale + Time.deltaTime / UpTime);
  }

  public void HurtPulse()
  {
    HurtScale = 1;
  }
}
