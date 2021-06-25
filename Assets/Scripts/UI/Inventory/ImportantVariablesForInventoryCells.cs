using UnityEngine;
using Zenject;

public class ImportantVariablesForInventoryCells : MonoBehaviour
{
    [SerializeField] Canvas m_canvas;
    RectTransform rectTransform;

    [Inject] public PlayerInventory m_playerInventory { get; set; }
    public Canvas Canvas { get => m_canvas; }
    public Vector2 XMaxAndMaxPointForDrop { get; set; }
    public Vector2 YMinAndMaxPointForDrop { get; set; }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        //XMaxAndMaxPointForDrop = new Vector2(320,
        //                   1100);

        //YMinAndMaxPointForDrop = new Vector2(100,
        //                   520);



        //  TODO: Сделать с процентами

        Rect canvas = Canvas.GetComponent<RectTransform>().rect;
        Rect inventoryCellsPanel = rectTransform.rect;

        // SerializeField для процента
        int percentOfScreenByX = 40;
        // Высчитываем от общей хуйни процент для X
        float XBorderForDrop = canvas.width * percentOfScreenByX / 100;

        Vector2 XMaxAndMaxPointForDrop2 = new Vector2(XBorderForDrop,
                          XBorderForDrop + inventoryCellsPanel.width); // Прибавляем длину хуйни где ячейки 

        // SerializeField для процента
        int percentOfScreenByY = 40;
        // Высчитываем от общей хуйни процент для Y
        float YBorderFopDrop = canvas.height * percentOfScreenByY / 100;

        Vector2 YMinAndMaxPointForDrop2 = new Vector2(YBorderFopDrop,
                           YBorderFopDrop + inventoryCellsPanel.height); // Прибавляем длину хуйни где ячейки


    }

}

