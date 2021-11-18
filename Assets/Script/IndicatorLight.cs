using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorLight : MonoBehaviour
{
    public float inertia = 1f;
    private Light _light;
    float indicatorTimer;
    void OnEnable()
    {
        _light = GetComponent<Light>();
        _light.enabled = true;
    }
    private void OnDisable()
    {
        _light.intensity = 0;
    }
    // Update is called once per frame
    void Update()
    {
        indicatorTimer += Time.deltaTime;
        if (indicatorTimer >= .5f)
        {
            Lighting(0);
          
        }
        else
        
            Lighting(1);
            
        if (indicatorTimer >= 1f)
            indicatorTimer = 0f;
    }
    ///<summary>
    ///Define the lighting setting accoridng to the time to make blink effect
    ///</summary>
    private void Lighting(float input)
    {
        _light.intensity = Mathf.Lerp(_light.intensity, input, Time.deltaTime * inertia * 20f);
    }

}
