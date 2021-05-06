using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    FlexibleGrid grid;

    CellObj cell;

    RectTransform rect;

    void Start()
    {
        cell = transform.parent.gameObject.GetComponent<CellObj>();
        grid = GameObject.Find("layout").GetComponent<FlexibleGrid>();
        rect = GetComponent<RectTransform>();
    }


    void Update()
    {
        rect.sizeDelta = new Vector2(grid.cellSize.y / 2, grid.cellSize.x / 2);

        if(cell.cell.bestAction == MDP.Action.down)
        {
            rect.rotation = Quaternion.Euler(0, 0, 0);
        } else if (cell.cell.bestAction == MDP.Action.right)
        {
            rect.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (cell.cell.bestAction == MDP.Action.up)
        {
            rect.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (cell.cell.bestAction == MDP.Action.left)
        {
            rect.rotation = Quaternion.Euler(0, 0, 270);
        }
    }
}
