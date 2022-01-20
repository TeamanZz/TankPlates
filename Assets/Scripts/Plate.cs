using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    public int value;
    public int reflectionPlateChance;
    public int explosionPlateChance;
    public bool isExplosivePlate;
    public bool isReflectionPlate;
    public bool wasExploded;
    public bool wasKilled;

    [SerializeField] private TextMeshPro valueText;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject explodeImage;

    public void SetPlateAsEmptyOnStart()
    {
        value = 0;
        SetDefaultColor();
        DisableText();
    }

    public void RemoveReflectBehaivor()
    {
        if (isReflectionPlate)
        {
            isReflectionPlate = false;
            GetComponent<BoxCollider>().isTrigger = true;
            SetPlateAsEmptyOnStart();
            var newParticles = Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, transform.parent.parent);
        }
    }

    public void SetNewNonZeroValue(int newValue = 1)
    {
        var num = Random.Range(0, reflectionPlateChance);
        value = newValue;

        if (value >= 6 && num == 0)
        {
            isReflectionPlate = true;
            GetComponent<BoxCollider>().isTrigger = false;
            var currentMaterial = meshRenderer.material;
            meshRenderer.material = new Material(currentMaterial);
            meshRenderer.material.color = Color.black;
            transform.parent.GetComponent<PlateLine>().DecreasePlatesCount();
            valueText.gameObject.SetActive(false);

            return;
        }

        UpdateTextValue();
        SetColorDependsOnValue();

        var num2 = Random.Range(0, explosionPlateChance);
        value = newValue;

        if (value >= 12 && num2 == 0)
        {
            isExplosivePlate = true;
            explodeImage.SetActive(true);
            DisableText();
        }
    }

    public void TakeDamage(int damageValue)
    {
        if (isReflectionPlate)
            return;

        if (value <= 0)
            return;

        value -= damageValue;
        if (CheckOnZeroValue())
        {
            if (isExplosivePlate)
                ExplodePlate();
            else
                KillPlate();
        }
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
        wasKilled = false;
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
        if (wasKilled)
            return;
        value = 0;
        wasKilled = true;
        GetComponent<BoxCollider>().isTrigger = true;
        var newParticles = Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, transform.parent.parent);
        SetDefaultColor();
        ShakePlate();
        DisableText();
        ProgressController.Instance.IncreaseCurrency();
        if (!isReflectionPlate)
            transform.parent.GetComponent<PlateLine>().DecreasePlatesCount();
        isReflectionPlate = false;
    }

    private void ExplodePlate()
    {
        if (wasExploded)
            return;
        wasExploded = true;
        value = 0;
        var newParticles = Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity, transform.parent.parent);
        SetDefaultColor();
        ShakePlate();
        DisableText();
        ProgressController.Instance.IncreaseCurrency();
        if (!isReflectionPlate)
            transform.parent.GetComponent<PlateLine>().DecreasePlatesCount();
        explodeImage.SetActive(false);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
        foreach (var hitCollider in hitColliders)
        {
            Plate plate;
            if (hitCollider.TryGetComponent<Plate>(out plate))
            {

                if (!plate.isExplosivePlate)
                    plate.KillPlate();
                else
                {
                    if (!plate.wasExploded)
                        plate.ExplodePlate();
                }
            }
        }
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