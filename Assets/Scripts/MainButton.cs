using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainButton : MonoBehaviour
{
    public int cost;

    [SerializeField] private Button button;
    [SerializeField] private List<Image> imagesList = new List<Image>();

    private bool clickable = true;

    void FixedUpdate()
    {
        if (clickable == true && ProgressController.Instance.moneyCount < cost)
        {
            for (int i = 0; i < imagesList.Count; i++)
            {
                imagesList[i].color = Color.gray;
            }
            button.enabled = false;
            clickable = false;
        }

        if (clickable == false && ProgressController.Instance.moneyCount >= cost)
        {
            for (int i = 0; i < imagesList.Count; i++)
            {
                imagesList[i].color = Color.white;
            }
            button.enabled = true;
            clickable = true;
        }
    }
}