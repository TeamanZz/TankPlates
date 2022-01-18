using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    [SerializeField] private TextMeshPro valueText;

    [SerializeField] private int value;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject particles;

    public void SetEmptyValues()
    {
        value = 0;
        SetColorDependsOnValue();
        DisableText();
        this.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void SetNewValue(int newValue = 0)
    {
        value = newValue;

        UpdateView();
    }

    public void TakeDamage(int damageValue)
    {
        if (value <= 0)
            return;
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
            var newParticles = Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, transform.parent.parent);
            DisableText();
            this.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
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
        // var state = GetStateDependsOnValue();
        if (value <= 0)
        {
            transform.DOShakePosition(0.15f, 0.5f, 10, 90);
        }

        // currentState = state;
        var newColor = ColorsHandler.Instance.GetLerpedColor(value);
        var currentMaterial = meshRenderer.material;
        meshRenderer.material = new Material(currentMaterial);
        meshRenderer.material.color = newColor;
    }

    private int GetStateDependsOnValue()
    {
        int state = 0;
        if (value <= 0)
        {
            state = 0;
        }
        if (value >= 1 && value <= 6)
        {
            state = 1;
        }
        return state;
    }
}