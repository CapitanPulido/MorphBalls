using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControladorVolumen : MonoBehaviour
{
    public AudioMixer mixer;         // Tu AudioMixer
    public AudioMixer mixerSFX;         // Tu AudioMixer
    public Slider sliderVolumen;     // El slider en el UI
    public Slider sliderSFX;     // El slider en el UI
    public string parametroVolumen = "Volumen";  // Nombre del parámetro en el mixer
    public string parametroVolumenSFX = "VolumenSFX";  // Nombre del parámetro en el mixer

    void Start()
    {
        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);
        sliderSFX.onValueChanged.AddListener(CambiarVolumenSFX);
    }

    void CambiarVolumen(float valor)
    {
        // El volumen del mixer debe estar en dB (-80 a 0), así que convertimos de [0,1] a dB
        float volumenEnDB = Mathf.Log10(Mathf.Clamp(valor, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(parametroVolumen, volumenEnDB);
    }

    void CambiarVolumenSFX
        (float valor)
    {
        // El volumen del mixer debe estar en dB (-80 a 0), así que convertimos de [0,1] a dB
        float volumenEnDB = Mathf.Log10(Mathf.Clamp(valor, 0.0001f, 1f)) * 20f;
        mixerSFX.SetFloat(parametroVolumenSFX, volumenEnDB);
    }
}
