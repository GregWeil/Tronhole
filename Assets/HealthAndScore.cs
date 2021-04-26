using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndScore : MonoBehaviour
{
  public int Health;
  public AudioSource CheckpointSound;
  public UnityEngine.UI.Text HealthDisplay;
  public UnityEngine.UI.Text ScoreDisplay;
  public UnityEngine.UI.Text BestDisplay;
  public float ScoreDelay;
  public float ScoreInterval;

  private int Best;
  private int Score;
  private float ScoreTimer;

  void Start()
  {
    Best = PlayerPrefs.GetInt("BestScore", 0);
    BestDisplay.text = Best > 0 ? Best.ToString() : "---";
    ChangeHealth(0);
    ChangeScore(0);
    ScoreTimer = ScoreDelay;
  }

  void Update()
  {
    ScoreTimer -= Time.deltaTime;
    if (ScoreTimer < 0)
    {
      ChangeScore(+1);
      ScoreTimer += ScoreInterval;
    }
  }

  public void OnHurt()
  {
    ChangeHealth(-1);
  }

  public void OnCheckpoint()
  {
    ChangeHealth(+1);
    CheckpointSound.Play();
  }

  private void ChangeHealth(int amount)
  {
    Health += amount;
    if (Health < 0)
    {
      Time.timeScale = 0f;
    }
    HealthDisplay.text = Health >= 0 ? Health.ToString() : "---";
  }

  private void ChangeScore(int amount)
  {
    Score += amount;
    ScoreDisplay.text = Score.ToString();
    if (Score > Best)
    {
      PlayerPrefs.SetInt("BestScore", Score);
    }
  }
}
