using System;
using UnityEngine;
    
public class CellData
{
    public Vector2 cords;
    public GameObject self;
    public Color color;
    public bool check = false;
    public int counter = 0;
    public Action switch_color;

    public CellData(Vector2 cords, GameObject self, Color color)
    {
        this.cords = cords;
        this.self = self;
        this.color = color;
    }
        
}