using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndScore : MonoBehaviour
{
  public int Health;
  public AudioSource CheckpointSound;

  public void OnHurt()
  {
    Health -= 1;
    if (Health <= 0)
    {
      Time.timeScale = 0f;
    }
  }

  public void OnCheckpoint()
  {
    Health += 1;
    CheckpointSound.Play();
  }
}
