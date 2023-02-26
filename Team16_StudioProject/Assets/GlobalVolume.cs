using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlobalVolume : MonoBehaviour
{
    public static GlobalVolume globalVolume;
    private PostProcessVolume postProcessingVolume;
    private Vignette vignette;
    private bool off = false;
    private bool isChase = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (globalVolume == null)
        {
            globalVolume = this;
            postProcessingVolume = GetComponent<PostProcessVolume>();
            postProcessingVolume.profile.TryGetSettings(out vignette);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChase)
        {
            OffVignette();
        }
    }
    public void SetVignetteIntensity()
    {
        if (vignette.intensity.value < 0.6 && !off)
        {
            vignette.intensity.value += Time.deltaTime;
        }
        else
        {
            off = true;
        }

        if (vignette.intensity.value > 0 && off)
        {
            vignette.intensity.value -= Time.deltaTime;
        }
        else
        {
            off = false;
        }
    }
    public void OffVignette()
    {
        if (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= Time.deltaTime;
        }
        else
        {
            off = false;
        }
    }
    public void SetChase(bool chase)
    {
        isChase = chase;
    }
    public bool GetChase()
    {
        return isChase;
    }
}
