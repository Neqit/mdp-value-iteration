using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGrid : LayoutGroup
{

    public int rows, columns;

    public Vector2 cellSize;
    public Vector2 spacing;
    public override void CalculateLayoutInputVertical()
    {
        float sqrt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrt);
        columns = Mathf.CeilToInt(sqrt);

        float parrentWidht = rectTransform.rect.width;
        float parrentHeight = rectTransform.rect.height;

        float cellWidth = parrentWidht / (float)columns - ((spacing.x/(float)columns)*2);
        float cellHeight = parrentHeight / (float)rows - ((spacing.y/(float)rows)*2);

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        int columnCount, rowCount = 0;

        for(int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount);
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount);

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }

    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
