using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextRound : MonoBehaviour
{
    private Text text;
    private Color color;
    private int sign = 1;

    private void Start()
    {
        text = GetComponent<Text>();
        color = text.color;
    }

    private void FixedUpdate()
    {
        if(color.a <= 0 || color.a >= 1)
        {
            sign *= -1;
        }
        color.a += sign * Time.fixedDeltaTime;
        text.color = color;
    }
}
