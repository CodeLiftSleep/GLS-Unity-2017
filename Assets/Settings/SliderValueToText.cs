using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SliderValueToText : MonoBehaviour
    {
        public Slider sliderUI;
        private Text textSliderValue;
        // Use this for initialization
        void Start()
        {
            textSliderValue = GameObject.Find("TextSlider").GetComponent<Text>();
            showSliderValue();
        }

        public void showSliderValue()
        {
            string sliderMessage = "" + sliderUI.value;
            textSliderValue.text = sliderMessage;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}