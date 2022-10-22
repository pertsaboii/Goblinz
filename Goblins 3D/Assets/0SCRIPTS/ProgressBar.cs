using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{

    private Slider slider;

    public float fillSpeed = 0.1f;
    private float targetProgress = 0;
    [SerializeField] private TMP_Text resourceAmount;

    private void Awake() 
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        IncrementProgress(10);
    }

    // Update is called once per frame
    void Update()
    {


        resourceAmount.text = slider.value.ToString("0");

        if (slider.value < targetProgress)
            slider.value +=fillSpeed * Time.deltaTime;
    }

    // Add Progress to the bar

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }
}
