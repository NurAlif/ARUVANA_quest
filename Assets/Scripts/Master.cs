using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Master : MonoBehaviour
{

    public ARCursor aRCursor;

    public AudioSource audioData;
    private List<Material> mats;
    private Material matObj;

    public void PlayQuest()
    {
        if (aRCursor.spawned)
        {
            GameObject model = aRCursor.pictogramObj;
            SkinnedMeshRenderer renderer = model.GetComponentInChildren<SkinnedMeshRenderer>();
            ParticleSystem particle = model.GetComponentInChildren<ParticleSystem>();
            matObj = renderer.material;
            StartCoroutine("blink");
            particle.Emit(40);
        }
        audioData.Play();
    }

    IEnumerator blink()
    {
        Color lowColor = Color.white;
        Color highColor = Color.grey;
        lowColor.a = 0.5f;
        highColor.a = 1f;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.7f);
            matObj.color = lowColor;
            yield return new WaitForSeconds(0.7f);
            matObj.color = highColor;
        }
        yield return null;
    }
    

    // Update is called once per frame
    void Update()
    {
    }
}
