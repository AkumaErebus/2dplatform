using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 movespeed = new Vector3(0, 50, 0);
    public float timetoFade = 1f;
     
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    private float timeElapsed = 0f;
    private Color startColor;
    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro= GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    } 
    
    private void Update()
    {
        textTransform.position += movespeed * Time.deltaTime;

        timeElapsed+= Time.deltaTime;
        if(timeElapsed < timetoFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timetoFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else 
        {
            Destroy(gameObject); 
        }
        
    }
}
