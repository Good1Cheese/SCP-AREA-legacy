using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PlayerHealthCell : MonoBehaviour
{
    Image m_image;

    public Image Image { get => m_image; set => m_image = value; }

    void Awake()
    {
        m_image = GetComponent<Image>();
        PlayerHealthSystem.HealthCells.Add(this);
    }
}


