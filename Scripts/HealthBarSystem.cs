using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarSystem : MonoBehaviour
{
    public Image fill;
    public Text numbers;

    public void SetBar(int value1, int value2)
    {
        fill.fillAmount = (float)value1 / (value2 * 1f);
        numbers.text = value1 + " / " + value2;
    }
}
