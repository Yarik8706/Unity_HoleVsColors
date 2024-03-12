using System;
using UnityEngine;
using UnityEngine.UI;

public class FirstClickImageControl : MonoBehaviour
{
    [SerializeField] private GameObject clickImage;

    private void Update()
    {
        if(!clickImage.activeSelf) return;
        if (Input.GetMouseButtonDown(0) || Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            clickImage.SetActive(false);
        }
    }
}