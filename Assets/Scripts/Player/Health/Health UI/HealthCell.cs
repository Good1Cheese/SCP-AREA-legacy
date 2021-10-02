using UnityEngine;
using UnityEngine.UI;

public class HealthCell : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] protected Slider m_slider;

    public Slider Slider { get => m_slider; }
    public bool IsFull { get; set; } = true;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public virtual void Clear()
    {
        IsFull = false;
        Slider.value = 0;
    }

    public virtual void Fill()
    {
        Slider.value = Slider.maxValue;
        IsFull = true;
    }
}
