using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MDP : MonoBehaviour
{
    public enum Action {up, down, right, left};

    public int penalty = -10;

    [Range(0,1)]
    public float probability;
    [Range(0, 1)]
    public float discountFactor;

    Action preferableAction = Action.up;

    public int rows, columns = 10;

    public int goodReward = 100;
    public int badReward = -100;

    public GameObject cellPrefab;
    public Transform layout;

    private CellObj[,] table;

    [SerializeField]
    int iteration = 0;

    public float epsilon = 0.00001f;

    void Start()
    {
        table = new CellObj[rows, columns];

        for(int i = 0; i<rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject newCell = Instantiate(cellPrefab, layout);
                newCell.name = "cell " + i + " " + j;
                table[i, j] = newCell.GetComponent<CellObj>();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            iteration = 0;
            while (iteration < 10000)
            {
                float delta = 0;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {

                        if(table[i,j].cell.cellType != CellObj.CellType.neutral)
                        {
                            continue;
                        }

                        float currentValue = table[i, j].cell.value;
                        float newValue = 0;

                        Dictionary<Action,CellObj> possibleActions = new Dictionary<Action,CellObj>();

                        //Define possible actons
                        foreach (Action action in Enum.GetValues(typeof(Action)))
                        {
                            if (action == Action.right)
                            {
                                try
                                {
                                    if(table[i, j + 1].cell.cellType != CellObj.CellType.wall)
                                    {
                                        possibleActions.Add(action, table[i, j + 1]);
                                    } 
                                    else { possibleActions.Add(action, table[i, j]); }
                                }
                                catch { possibleActions.Add(action, table[i, j]); }
                                
                            }
                            else if (action == Action.left)
                            {
                                try
                                {
                                    if (table[i, j - 1].cell.cellType != CellObj.CellType.wall)
                                    {
                                        possibleActions.Add(action, table[i, j - 1]);
                                    }
                                    else { possibleActions.Add(action, table[i, j]); }
                                }
                                catch { possibleActions.Add(action, table[i, j]); }
                            }
                            else if (action == Action.down)
                            {
                                try
                                {
                                    if (table[i + 1, j].cell.cellType != CellObj.CellType.wall)
                                    {
                                        possibleActions.Add(action, table[i + 1, j]);
                                    }
                                    else { possibleActions.Add(action, table[i, j]); }
                                }
                                catch { possibleActions.Add(action, table[i, j]); }
                            }
                            else if (action == Action.up)
                            {
                                try
                                {
                                    if (table[i - 1, j].cell.cellType != CellObj.CellType.wall)
                                    {
                                        possibleActions.Add(action, table[i - 1, j]);
                                    }
                                    else { possibleActions.Add(action, table[i, j]); }
                                }
                                catch { possibleActions.Add(action, table[i, j]); }
                            }
                        }

                        float maxVal = 0;
                        foreach(CellObj act in possibleActions.Values) 
                        {
                            if (act.cell.value > maxVal)
                            {
                                maxVal = act.cell.value;
                                preferableAction = possibleActions.FirstOrDefault(x => x.Value == act).Key;
                                table[i, j].cell.bestAction = preferableAction;
                            }
                        }
                        


                        float probabilitiesFortheOtherDirections = 0;
                        
                        probabilitiesFortheOtherDirections = (1 - probability) / (possibleActions.Count - 1);
                        float v = 0;

                        CellObj u, d, l, r;

                        //Bellman equaton
                        if (possibleActions.TryGetValue(Action.up, out u))
                        {
                            if (preferableAction == Action.up) { v += u.cell.value * probability; }
                            else { v += u.cell.value * probabilitiesFortheOtherDirections; }
                        }

                        if (possibleActions.TryGetValue(Action.down, out d))
                        {
                            if (preferableAction == Action.down) { v += d.cell.value * probability; }
                            else { v += d.cell.value * probabilitiesFortheOtherDirections; }
                        }

                        if (possibleActions.TryGetValue(Action.right, out r))
                        {
                            if (preferableAction == Action.right) { v += r.cell.value * probability; }
                            else { v += r.cell.value * probabilitiesFortheOtherDirections; }
                        }

                        if (possibleActions.TryGetValue(Action.left, out l))
                        {
                            if (preferableAction == Action.left) { v += l.cell.value * probability; }
                            else { v += l.cell.value * probabilitiesFortheOtherDirections; }
                        }

                        v = table[i,j].cell.reward + (discountFactor * v);

                        newValue = v;
                        table[i, j].cell.value = v;

                        delta = Mathf.Max(delta, Mathf.Abs(currentValue - v));
                    }
                }

                if(delta < epsilon)
                {
                    Debug.Log(iteration);
                    break;
                }

                iteration += 1;
           }

            
        }
    }

   
}

public class Cell
{
    public float value;
    public CellObj.CellType cellType;
    public MDP.Action bestAction;
    public int reward;
}


