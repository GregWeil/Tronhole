using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthAndScore : MonoBehaviour
{
  public int Health;
  public AudioSource CheckpointSound;
  public UnityEngine.UI.Text HealthDisplay;
  public UnityEngine.UI.Text ScoreDisplay;
  public UnityEngine.UI.Text BestDisplay;
  public UnityEngine.UI.Text LastDisplay;
  public float ScoreDelay;
  public float ScoreInterval;
  public AudioSource DeathSound;
  public float RespawnDelay;
  public float FadeDelay;

  private int Best;
  private int Score;
  private float ScoreTimer;

  void Start()
  {
    Best = PlayerPrefs.GetInt("BestScore", 0);
    BestDisplay.text = Best > 0 ? Best.ToString() : "---";
    var Last = PlayerPrefs.GetInt("LastScore", 0);
    LastDisplay.text = Last > 0 ? Last.ToString() : "---";
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
      DeathSound.Play();
      Time.timeScale = 0f;
      StartCoroutine(Death());
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
    if (Score > 0)
    {
      PlayerPrefs.SetInt("LastScore", Score);
      LastDisplay.transform.parent.gameObject.SetActive(false);
      ScoreDisplay.transform.parent.gameObject.SetActive(true);

    }
  }
  private IEnumerator Death()
  {
    yield return new WaitForSecondsRealtime(FadeDelay);
    var fade = GameObject.FindObjectOfType<FadeToBlack>();
    if (fade != null)
    {
      fade.TargetOpacity = 1f;
      fade.Speed = 0.5f;
    }
    yield return new WaitForSecondsRealtime(RespawnDelay - FadeDelay);
    SceneManager.LoadScene("SampleScene");
  }
}

