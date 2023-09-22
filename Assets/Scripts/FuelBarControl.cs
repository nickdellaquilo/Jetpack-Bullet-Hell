using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarControl : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateFuel(float curr, float max){ //controls the slider based on current fuel
        slider.value = curr/max;
    }
}
