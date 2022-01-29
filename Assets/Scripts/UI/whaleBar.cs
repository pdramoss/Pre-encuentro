using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BarType {
    healthBar,
    distancebar
}
public class whaleBar : MonoBehaviour
{

    private Slider slider;
    public BarType type;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        switch(type){
            case BarType.healthBar:
            slider.maxValue = Whale.Max_Health;
            break;

            case BarType.distancebar:
            slider.maxValue = Whale.Max_dist;
            break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(type){
            case BarType.healthBar:
            slider.value = GameObject.Find("whale").GetComponent<Whale>().GetHealth();
            break;

            case BarType.distancebar:
            slider.value = GameObject.Find("whale").GetComponent<Whale>().GetDistance();
            break;
        }

        
    }
}
