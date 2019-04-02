using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    Bloom bloomLayer = null;
    AmbientOcclusion ambientOcclusionLayer = null;
    ColorGrading colorGradingLayer = null;
    PostProcessVolume volume=null;
    private bool isFlashBang = false;
    private float flashBangCounter = 0.0f;
    [SerializeField]
    private float flashBangFactor = 6.0f;
    [SerializeField]
    private float flashBangMax = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        // somewhere during initializing
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloomLayer);
        volume.profile.TryGetSettings(out ambientOcclusionLayer);
        volume.profile.TryGetSettings(out colorGradingLayer);

        //Set default values
        BloomSetup();
        ColorGradientSetup();
    }

    // Update is called once per frame
    void Update()
    {
        FlashBangEffectUpdate();
    }

    private void BloomSetup()
    {
        if(bloomLayer==null)
        {
            return;
        }

        //bloomLayer.intensity.value = 0;
        bloomLayer.enabled.value = true;
        bloomLayer.intensity.value = 0;
        bloomLayer.threshold.value = 1;
        bloomLayer.softKnee.value = 0.5f;
        bloomLayer.clamp.value = 65472;
        bloomLayer.diffusion.value = 7;
        bloomLayer.anamorphicRatio.value = 0;
        //bloomLayer.color.value = Color.cyan;
        bloomLayer.fastMode.value = false;
    }


    private void ColorGradientSetup()
    {
        if (colorGradingLayer == null)
        {
            return;
        }

        //bloomLayer.intensity.value = 0;
        colorGradingLayer.enabled.value = true;
        colorGradingLayer.temperature.value = 0.0f;
        colorGradingLayer.tint.value = 0.0f;
        colorGradingLayer.postExposure.overrideState = true;
        colorGradingLayer.postExposure.value = 0.0f;
        colorGradingLayer.hueShift.value = 0.0f;
        colorGradingLayer.saturation.value = 0.0f;
        colorGradingLayer.contrast.value = 0.0f;

        //colorGradingLayer.colorFilter.value = Color.black;
    }

    public void SetColorTemperature(float Valor)
    {
        if(colorGradingLayer!=null)
        {
            colorGradingLayer.temperature.value = Valor;
        }
    }

    public void SetTint(float Valor)
    {
        if (colorGradingLayer != null)
        {
            colorGradingLayer.tint.value = Valor;
        }
    }

    public void SetExposure(float Valor)
    {
        if (colorGradingLayer != null)
        {
            colorGradingLayer.postExposure.value = Valor;
        }
    }

    public void FlashBangEffectUpdate()
    {
        if(isFlashBang && colorGradingLayer!=null)
        {
                flashBangCounter += Time.deltaTime * flashBangFactor;
                colorGradingLayer.postExposure.value = flashBangCounter;

            if (flashBangCounter > flashBangMax && flashBangFactor > 0.0f)
            {
                flashBangFactor = flashBangFactor * -1.0f;
            }
        }
    }

    public void FlashBangEffect()
    {
        isFlashBang = true;
        StartCoroutine(FlashBangEffectRoutine());
    }
    
    private IEnumerator FlashBangEffectRoutine()
    {
        yield return new WaitForSeconds(3);
        isFlashBang = false;
        flashBangCounter = 0.0f;
        colorGradingLayer.postExposure.value = flashBangCounter;
        if (flashBangFactor < 0.0f)
        {
            flashBangFactor = flashBangFactor * -1.0f;
        }
    }
}
