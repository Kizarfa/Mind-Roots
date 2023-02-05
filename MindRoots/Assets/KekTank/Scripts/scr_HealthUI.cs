using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_HealthUI : MonoBehaviour
{
    public void UpdateHealth(float currenthp,  float maxhp)
    {
        //860 - 1050
        RectTransform rect = GetComponent<RectTransform>();
        float bottom = Mathf.Lerp(1050, 860, currenthp / maxhp);
        //Debug.Log(currenthp / maxhp);

        rect.offsetMin = new Vector2(rect.offsetMin.x, bottom);
    }
}
