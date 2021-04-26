using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
  public GameObject StartButton;
  public GameObject QuitButton;
  public GameObject LookDown;
  public GameObject Cursor;
  public UnityEngine.UI.Image CursorProgress;
  public float OptionsDelay;
  public float SelectDelay;
  public float SelectClearDelay;
  public float LookDownThreshold;
  public float CursorDistance;
  public float CursorAngle;
  public float CursorMinScale;
  public float CursorScaleSpeed;

  private float SelectValue;
  private Vector3 CursorScaleBase;
  private float CursorScale;
  private int UILayerMask;

  void Start()
  {
    SelectValue = 0;
    CursorScaleBase = Cursor.transform.localScale;
    CursorScale = CursorMinScale;
    UILayerMask = LayerMask.GetMask("UI");
    StartCoroutine(ShowOptions());
  }

  void Update()
  {
    var offset = Camera.main.transform.TransformVector(Quaternion.Euler(CursorAngle, 0, 0) * Vector3.forward);
    Cursor.transform.position = Camera.main.transform.position + offset * CursorDistance;
    Cursor.transform.LookAt(Camera.main.transform.position);
    Cursor.transform.localScale = CursorScale * CursorScaleBase;

    var hit = false;
    if (Cursor.activeInHierarchy)
    {
      var hits = Physics.RaycastAll(Camera.main.transform.position, offset, float.PositiveInfinity, UILayerMask);
      if (hits.Length == 1)
      {
        if (SelectValue == 1f)
        {
          if (hits[0].collider.gameObject == StartButton)
          {
            StartCoroutine(SelectStart());
          }
          else if (hits[0].collider.gameObject == QuitButton)
          {
            StartCoroutine(SelectQuit());
          }
        }
        hit = true;
      }

      if (Input.GetKeyDown(KeyCode.F1))
      {
        Time.timeScale = 1f;
      }
    }
    var selectSpeed = (hit && CursorScale >= 1f) ? SelectDelay : SelectClearDelay;
    SelectValue = Mathf.MoveTowards(SelectValue, (hit && CursorScale >= 1f) ? 1f : 0f, Time.unscaledDeltaTime / selectSpeed);
    CursorProgress.fillAmount = SelectValue;
    var targetScale = hit ? 1f : CursorMinScale;
    CursorScale = Mathf.MoveTowards(CursorScale, targetScale, CursorScaleSpeed * Time.unscaledDeltaTime);
  }

  IEnumerator ShowOptions()
  {
    yield return new WaitForSecondsRealtime(OptionsDelay);
    StartButton.SetActive(true);
    QuitButton.SetActive(true);
    Cursor.SetActive(true);
  }

  IEnumerator SelectStart()
  {
    Cursor.SetActive(false);
    StartButton.SetActive(false);
    QuitButton.SetActive(false);
    LookDown.SetActive(true);

    yield return null;

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
    Cursor.SetActive(false);
    StartButton.SetActive(false);
    QuitButton.SetActive(false);
    yield return null;
    Application.Quit();
  }
}
