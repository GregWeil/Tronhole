using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
  public Transform LookOrientation;
  public Color DownColor;
  public float DownDensity;
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
    var scale = Vector3.Angle(LookOrientation.forward, Vector3.down) / 180f;
    var color = Color.Lerp(DownColor, UpColor, scale);
    var density = Mathf.Lerp(DownDensity, UpDensity, scale);
    RenderSettings.fogColor = Color.Lerp(color, HurtColor, HurtScale);
    RenderSettings.fogDensity = Mathf.Lerp(density, HurtDensity, HurtScale);
    HurtScale -= HurtDecay * Time.deltaTime;
  }

  public void HurtPulse()
  {
    HurtScale = 1;
  }
}
