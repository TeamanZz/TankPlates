using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plate : MonoBehaviour
{
    [SerializeField] private TextMeshPro valueText;

    [SerializeField] private int value;
    [SerializeField] private MeshRenderer meshRenderer;

    private void OnMouseDown()
    {
        TakeDamage(1);
    }

    public void SetNewValue(int newValue = 0)
    {
        value = newValue;

        UpdateView();
    }

    public void TakeDamage(int damageValue)
    {
        value -= damageValue;

        UpdateView();
    }

    private void UpdateView()
    {

        SetColorDependsOnValue();

        if (CheckOnZeroValue())
            return;

        UpdateValueView();
    }

    public void UpdateValueView()
    {
        valueText.text = value.ToString();
    }

    private bool CheckOnZeroValue()
    {
        if (value <= 0)
        {
            DisableText();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DisableText()
    {
        valueText.gameObject.SetActive(false);
    }

    private void SetColorDependsOnValue()
    {

        var newColor = ColorsHandler.Instance.GetNewPlateColor(value);
        var currentMaterial = meshRenderer.material;
        meshRenderer.material = new Material(currentMaterial);
        Debug.Log(newColor);
        meshRenderer.material.color = newColor;
    }
}