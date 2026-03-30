using UnityEngine;
using UnityEngine.UI;
using static FieldGenerator;

public class ChangeColor : MonoBehaviour
{
    void Start()
    {
        cell.switch_color = SwitchColor;
    }
    public CellData cell;
    public int counter;

    public void SwitchColor()
    {
        RawImage im = cell.self.GetComponent<RawImage>();
        if (im.color == Color.red)
        {
            im.color = Color.white;
        }
        else im.color = Color.red;
        cell.color = im.color;
        ChangeCell(cell);

    }
    void FixedUpdate()
    {
        counter = cell.counter;
    }
}
