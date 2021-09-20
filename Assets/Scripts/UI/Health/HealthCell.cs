using UnityEngine;
using UnityEngine.UI;

public class HealthCell : MonoBehaviour
{
    [SerializeField] protected Image m_image;
    [SerializeField] protected Slider m_slider;

    public Slider Slider { get => m_slider;}

    public bool IsFull { get; set; } = true;

    public void SetSprite(Sprite sprite)
    {
        m_image.sprite = sprite;
    }

    public virtual void Clear()
    {
        Slider.value = 0;
        IsFull = false;
    }

    public void Fill()
    {
        Slider.value = Slider.maxValue;
        IsFull = true;
    }
}
