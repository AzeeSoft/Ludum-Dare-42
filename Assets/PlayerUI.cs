using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Slider weight;

    private void SetWeightUI(float value)
    {
        float percent = value / 100;

        weight.value = percent;
    }

    private void Update()
    {
        SetWeightUI(ProductManager.Instance.GetTotalProductWeight());
    }


}
