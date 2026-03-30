using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FieldGenerator : MonoBehaviour
{
    
    [SerializeField] private int size;
    [SerializeField] private float border;
    [SerializeField] private GameObject cell;
    public GameObject[,] field;
    public List<CellData> cells = new List<CellData>();
    public static List<CellData> redcells = new List<CellData>();
    public static List<CellData> whitecells = new List<CellData>();


    void Start()
    {
        Vector2 cameraSize = new Vector2(GetComponent<RectTransform>().rect.width,GetComponent<RectTransform>().rect.height);
        field = new GameObject[size,size];
        for(int i = 0; i<size; i++)
            for(int j = 0; j<size; j++)
            {
                GameObject temp = Instantiate(cell,transform);
                temp.name = "Cell ["+i+","+j+"]";
                temp.GetComponent<RectTransform>().sizeDelta = new Vector2(cameraSize.x/size-border/2, cameraSize.y/size-border/2);
                temp.transform.localPosition = new Vector2((cameraSize.x/size)*i+border/4,(cameraSize.y/size)*j+border/4);
                field[i,j] = temp;
                CellData cell1 = new CellData(new Vector2(i,j),temp,temp.GetComponent<RawImage>().color);
                temp.GetComponent<ChangeColor>().cell = cell1;
                cells.Add(cell1);
            }

    }
    bool is_running=false;
    public void ControlProcess()
    {
        switch(is_running)
        {
            case true:
                CancelInvoke("FieldUpdate");
            break;
            case false:
        InvokeRepeating("FieldUpdate",0,.05f);
        break;
        }
        is_running = !is_running;
    }


    public static void ChangeCell(CellData cell)
    {
        if (cell.color == Color.red) {redcells.Add(cell); whitecells.Remove(cell);}
        if (cell.color == Color.white) {whitecells.Add(cell); redcells.Remove(cell);}
    }



    // Update is called once per frame
    void FieldUpdate()
    {
        List<CellData> new_field = new List<CellData>();
        List<CellData> reds = new List<CellData>(redcells);
        foreach(CellData r in reds)
        {
            for(int i = (int)r.cords.x-1;i<r.cords.x+2;i++)
                for(int j = (int)r.cords.y-1;j<r.cords.y+2;j++)
                {
                    try{
                    CellData target = field[i,j].GetComponent<ChangeColor>().cell;
                    for(int k = i-1;k<i+2;k++)
                    {
                        for(int l = j-1;l<j+2;l++)
                        {
                            try{
                                CellData temp = field[k,l].GetComponent<ChangeColor>().cell;
                            if(!(k==i && l==j)&&target.check==false)
                            {
                                if (temp.color == Color.red)
                                {
                                    target.counter+=1;
                                }
                            }}
                            catch{}
                        }
                    }
                    target.check = true;
                    }catch{}
                }
        }
        foreach(CellData c in cells)
        {
            if((c.counter>3 ||c.counter<2)&& c.color == Color.red)
            {
                c.switch_color();
            }
            if(c.counter==3 && c.color == Color.white)
            {
                c.switch_color();
            }
            c.check = false;
            c.counter = 0;
        }
    }
}
