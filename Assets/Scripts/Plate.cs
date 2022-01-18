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

    public void SetPlateAsEmptyOnStart()
    {
        value = 0;
        SetDefaultColor();
        DisableText();
        this.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void SetNewNonZeroValue(int newValue = 1)
    {
        value = newValue;
        UpdateTextValue();
        SetColorDependsOnValue();
    }

    public void TakeDamage(int damageValue)
    {
        value -= damageValue;
        if (CheckOnZeroValue())
            KillPlate();
        else
        {
            UpdateTextValue();
            SetColorDependsOnValue();
        }
    }

    private void UpdateTextValue()
    {
        valueText.text = value.ToString();
    }

    private void KillPlate()
    {
        var newParticles = Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, transform.parent.parent);
        SetDefaultColor();
        ShakePlate();
        DisableText();
        transform.parent.GetComponent<PlateLine>().DecreasePlatesCount();
        this.enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        ProgressController.progressController.AddingMoney(ProgressController.progressController.incomeLvl);
    }

    private bool CheckOnZeroValue()
    {
        return (value <= 0);
    }

    private void DisableText()
    {
        valueText.gameObject.SetActive(false);
    }

    private void ShakePlate()
    {
        transform.DOShakePosition(0.15f, 0.5f, 10, 90);
    }

    private void SetColorDependsOnValue()
    {
        var newColor = ColorsHandler.Instance.GetLerpedColor(value);
        var currentMaterial = meshRenderer.material;
        meshRenderer.material = new Material(currentMaterial);
        meshRenderer.material.color = newColor;
    }

    private void SetDefaultColor()
    {
        var newColor = ColorsHandler.Instance.GetDefaultColor();
        var currentMaterial = meshRenderer.material;
        meshRenderer.material = new Material(currentMaterial);
        meshRenderer.material.color = newColor;
    }
}