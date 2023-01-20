using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject ARInitImg;

    [SerializeField]
    private GameObject touchImg;

    [SerializeField]
    private GameObject panel;

    private GameObject[] panels;

    public bool touchIsDisplay = false;
    void Start()
    {
        panels = new GameObject[] { ARInitImg, touchImg };
        alloff();
        displayInitAR();
    }

    public void HidePanel()
    {
        panel.SetActive(false);
    }

    public void HideOverlay()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void displayTouch()
    {
        if (!touchIsDisplay)
        {
            alloff();
            touchImg.SetActive(true);
            text.SetText("Tekan layar jika posisi titik sudah benar!");

            StartCoroutine("autoClose");
            touchIsDisplay = true;
        }
    }

    private void alloff()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void displayInitAR()
    {
        if (!touchIsDisplay)
        {
            alloff();
            ARInitImg.SetActive(true);
            text.SetText("Gerakan kamera untuk memulai AR");
        }
    }


}
