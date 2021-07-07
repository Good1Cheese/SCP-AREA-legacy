using UnityEngine;
using UnityEngine.UI;

public class HealthCell : MonoBehaviour
{
    [SerializeField] protected Image m_image;
    [SerializeField] protected Slider m_slider;

    public Slider Slider { get => m_slider;}

    public void SetSprite(Sprite sprite)
    {
        m_image.sprite = sprite;
    }

    public virtual void MakeCellEmpty()
    {
        Slider.value = 0;
    }

    public void MakeCellFull()
    {
        Slider.value = Slider.maxValue;
    }
}
