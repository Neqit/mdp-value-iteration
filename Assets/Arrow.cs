using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public FlexibleGrid grid;

    CellObj cell;

    RectTransform rect;

    void Start()
    {
        cell = transform.parent.gameObject.GetComponent<CellObj>();
        rect = GetComponent<RectTransform>();
    }


    void Update()
    {
        rect.sizeDelta = new Vector2(grid.cellSize.y / 2, grid.cellSize.x / 2);

        switch (cell.cell.bestAction)
        {
            case MDP.Action.down:
                rect.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case MDP.Action.right:
                rect.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case MDP.Action.up:
                rect.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case MDP.Action.left:
                rect.rotation = Quaternion.Euler(0, 0, 270);
                break;
        }
        
    }
}
