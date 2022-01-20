using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    public int value;
    public bool isReflectionPlate;

    [SerializeField] private TextMeshPro valueText;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject particles;

    public void SetPlateAsEmptyOnStart()
    {
        value = 0;
        SetDefaultColor();
        DisableText();
    }

    public void SetNewNonZeroValue(int newValue = 1)
    {
        var num = Random.Range(0, 80);
        value = newValue;

        if (value > 6 && num == 0)
        {
            isReflectionPlate = true;
            GetComponent<BoxCollider>().isTrigger = false;
            var currentMaterial = meshRenderer.material;
            meshRenderer.material = new Material(currentMaterial);
            meshRenderer.material.color = Color.black;

            valueText.gameObject.SetActive(false);

            return;
        }

        UpdateTextValue();
        SetColorDependsOnValue();
    }

    public void TakeDamage(int damageValue)
    {
        if (isReflectionPlate)
            return;

        if (value <= 0)
            return;

        value -= damageValue;
        if (CheckOnZeroValue())
            KillPlate();
        else
        {
            UpdateTextValue();
            SetColorDependsOnValue();
        }
    }

    public void RestoreValue(int damageValue)
    {
        if (isReflectionPlate)
            return;
        ShakePlate();
        value = damageValue;

        UpdateTextValue();
        valueText.gameObject.SetActive(true);
        SetColorDependsOnValue();
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
        ProgressController.Instance.IncreaseCurrency();
        transform.parent.GetComponent<PlateLine>().DecreasePlatesCount();
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

        valueText.color = ColorsHandler.Instance.GetTextColor(value);
    }

    private void SetDefaultColor()
    {
        var newColor = ColorsHandler.Instance.GetDefaultColor();
        var currentMaterial = meshRenderer.material;
        meshRenderer.material = new Material(currentMaterial);
        meshRenderer.material.color = newColor;
    }
}