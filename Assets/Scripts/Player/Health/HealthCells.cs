using System.Collections.Generic;
using System.Linq;

public class HealthCells
{
    public int LastCellIndex { get; set; }

    public List<HealthCell> Cells { get; set; } = new List<HealthCell>();

    public HealthCell GetFirstFilledCell()
    {
        return Cells.LastOrDefault(cell => cell.IsFull);
    }

    public HealthCell GetNextCell()
    {
        return Cells[GetCurrentCellIndex() + 1];
    }

    public int GetCurrentCellIndex()
    {
        return Cells.IndexOf(GetFirstFilledCell());
    }

    public bool IsCurrentCellLast(int cellOffset = 0)
    {
        return GetCurrentCellIndex() + cellOffset == LastCellIndex;
    }

    public void AddCells(HealthCell[] healthCells)
    {
        LastCellIndex = healthCells.Length - 1;

        Cells.AddRange(healthCells);
    }

    public HealthCell this[int index]
    {
        get { return Cells[index]; }
        set { Cells[index] = value; }
    }

}
