using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_scroll : MonoBehaviour
{
    public float Scroll_speed = 0.1f;
    float offset;

    // Update is called once per frame
    void Update()
    {
        offset = (Time.time * Scroll_speed);
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex" , new Vector2(offset, 0));
    }
}
