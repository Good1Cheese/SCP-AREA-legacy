using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerHealthCell : MonoBehaviour
{
    Image m_image;

    void Awake()
    {
        m_image = GetComponent<Image>();
        PlayerHealthSystem.HealthCells.Add(this);
    }
    public void SetSprite(Sprite sprite)
    {
        m_image.sprite = sprite;
    }
}


