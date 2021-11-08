using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSupportUI : MonoBehaviour
{
    public Image icon;
    public Text text;

    private Color iconColor;
    private Color textColor;
    private int sign = 1;

    private void Start()
    {
        iconColor = icon.color;
        textColor = text.color;
    }

    private void FixedUpdate()
    {
        if (iconColor.a <= 0 || iconColor.a >= 1)
        {
            sign *= -1;
        }
        iconColor.a += sign * Time.fixedDeltaTime;
        icon.color = iconColor;

        textColor.a += sign * Time.fixedDeltaTime;
        text.color = textColor;
    }
}
