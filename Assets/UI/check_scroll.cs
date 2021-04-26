using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_scroll : MonoBehaviour
{
    public float Scroll_speed = 0.001f;
    float offset = 0f;
    //Renderer rend;
    //public Material mat;
    private void Start()
    {
        //rend = GetComponent<Renderer>();
    }
    // Update is called once per frame
    void Update()
    {
        offset += Scroll_speed;
        //mat.SetTextureOffset("_BaseColorMap" , new Vector2(offset, 0));
        transform.Rotate(0f, 0f, Scroll_speed);
    }
}
