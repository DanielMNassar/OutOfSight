using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverColorEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;
    public Color pressedColor = Color.black;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = normalColor;
    }

    public void OnMouseDown()
    {
        image.color = pressedColor;
    }

    public void OnMouseUp()
    {
        image.color = hoverColor;
    }
}
