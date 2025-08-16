using UnityEngine;
using TMPro;

public class TitleFlicker : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();

        InvokeRepeating(nameof(Flick), 0f, flickerSpeed);
    }

    void Flick()
    {
        float rand = Random.value;
        if (rand > 0.5f)
            text.alpha = 1f;
        else
            text.alpha = 0.3f; // dim flicker
    }
}
