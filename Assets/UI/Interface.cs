using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
  public GameObject StartButton;
  public GameObject QuitButton;
  public GameObject LookDown;
  public float OptionsDelay;
  public float SelectDelay;
  public float LookDownThreshold;

  private bool CursorActive;

  void Start()
  {
    CursorActive = false;
    StartCoroutine("ShowOptions");
  }

  IEnumerator ShowOptions()
  {
    yield return new WaitForSecondsRealtime(OptionsDelay);
    StartButton.SetActive(true);
    QuitButton.SetActive(true);
    CursorActive = true;
    yield return new WaitForSecondsRealtime(5);
    StartCoroutine("SelectStart");
  }

  IEnumerator SelectStart()
  {
    CursorActive = false;
    QuitButton.SetActive(false);
    yield return new WaitForSecondsRealtime(SelectDelay);
    StartButton.SetActive(false);
    LookDown.SetActive(true);

    var playerCollision = GameObject.FindObjectOfType<PlayerCollision>();
    while (true)
    {
      yield return null;
      var collision = playerCollision.GetCollision();
      var angle = Vector3.Angle(Camera.main.transform.forward, Vector3.down);
      if (!collision && angle < LookDownThreshold) break;
    }

    Time.timeScale = 1f;
  }

  IEnumerator SelectQuit()
  {
    CursorActive = false;
    StartButton.SetActive(false);
    yield return new WaitForSecondsRealtime(SelectDelay);
    Application.Quit();
  }
}
