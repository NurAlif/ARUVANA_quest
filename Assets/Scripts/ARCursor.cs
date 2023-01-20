using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARCursor : MonoBehaviour
{
    public GameObject child;
    public GameObject objectToPlace;
    public ARRaycastManager raycastManager;
    public Overlay overlay;

    public bool useCursor = true;

    [HideInInspector]
    public bool spawned = false;

    [HideInInspector]
    public GameObject pictogramObj;

    [SerializeField]
    private float scaleFactor = 0.02f;
    [SerializeField]
    private float rotateFactor = 0.02f;

    void Start()
    {
        child.SetActive(useCursor);

    }

    private Vector2 startPos1;
    private float startDeltaPinch = 0f;
    private Vector3 scaleInit;

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            if (useCursor)
            {
                UpdateCursor();
            }

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && overlay.touchIsDisplay)
                || Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 vector3 = transform.rotation.eulerAngles;
                Quaternion rot = Quaternion.Euler(0f, vector3.y, 0f);
                overlay.HideOverlay();
                if (useCursor)
                {
                    pictogramObj = Instantiate(objectToPlace, transform.position + Vector3.up * 0.1f, rot);
                    pictogramObj.transform.localScale = Vector3.one * 0.1f;
                }

                child.SetActive(false);
                spawned = true;
            }
        }
        else
        {
            if(Input.touchCount == 1) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) startPos1 = Input.GetTouch(0).position;
                else if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 delta = Input.GetTouch(0).position - startPos1;
                    pictogramObj.transform.Rotate(new Vector3(0, rotateFactor * delta.x, 0));
                }
            }
            if (Input.touchCount >= 2)
            {
                Vector2 touchPos1 = Input.GetTouch(0).position;
                Vector2 touchPos2 = Input.GetTouch(1).position;
                if (Input.GetTouch(1).phase == TouchPhase.Began) {
                    startDeltaPinch = Vector2.Distance(touchPos1, touchPos2);
                    scaleInit = pictogramObj.transform.localScale;
                }
                else {
                    float newDistPinch = Vector2.Distance(touchPos1, touchPos2);
                    float pinch = (newDistPinch - startDeltaPinch) * scaleFactor;

                    pictogramObj.transform.localScale = scaleInit + (Vector3.one * pinch);
                }
            }
        }



    }

    private void UpdateCursor()
    {
        Vector2 screenpos = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenpos, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        if (hits.Count > 0)
        {
            overlay.displayTouch();
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
