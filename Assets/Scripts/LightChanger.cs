using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class LightChanger : MonoBehaviour
{
    private Color defaultColorLight;
    private Color blueColor;
    private Color redLightColor;
    private Color redMaterialColor;
    private Color blueLightColor;
    private Color blueMaterialColor;

    public Light lanternLight1;
    public Light lanternLight2;
    public Light lanternLight3;
    public Light lanternLight4;
    public Light floatinglight;
    public Light smalllight;
    //public Light lightabove;

    public GameObject Lantern1;
    public GameObject Lantern2; 
    public GameObject Lantern3; 
    public GameObject Lantern4;


    private List<Light> lanternLights;

    public Material material;

    float duration = 8.0f;

    private bool isBlue = false;
    private bool changeColour;




    // Start is called before the first frame update
    void Start()
    {
        defaultColorLight = new Color(175f / 255f, 212f / 255f, 165f / 255f);

        redLightColor = new Color(255f / 255f, 111f / 255f, 0f / 255f);
        redMaterialColor = new Color(255f / 255f, 65f / 255f, 0f / 255f);

        blueLightColor = new Color(160 / 255f, 233f / 255f, 223f / 255f);
        blueMaterialColor = new Color(195 / 255f, 215f / 255f, 212f / 255f);

        /*lanternLights.Add(lanternLight1);
        lanternLights.Add(lanternLight2);
        lanternLights.Add(lanternLight3);
        lanternLights.Add(lanternLight4);*/


    }

    // Update is called once per frame
    void Update()
    {
        



      
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeColour = true;
            

        }

        if (changeColour)
        {
            setColorToBlue();

            if (!isBlue)
            {
                setLightToBlue();
            }

        }

    }

    //sets Material to blue
    public void setColorToBlue()
    {

        material.DOColor(Color.blue, 8);
       
    }

    //sets Light to blue
    public void setLightToBlue()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;

        lanternLight1.color = Color.Lerp(defaultColorLight, Color.blue, t);
        lanternLight2.color = Color.Lerp(defaultColorLight, Color.blue, t);
        lanternLight3.color = Color.Lerp(defaultColorLight, Color.blue, t);
        lanternLight4.color = Color.Lerp(defaultColorLight, Color.blue, t);
        smalllight.color = Color.Lerp(defaultColorLight, Color.blue, t);
        floatinglight.color = Color.Lerp(defaultColorLight, Color.blue, t);

        StartCoroutine(resetCoroutine());
    }

    //secret to only Lerp once
    IEnumerator resetCoroutine()
    {
        yield return new WaitForSeconds(duration);
        isBlue = true;
    }

}
