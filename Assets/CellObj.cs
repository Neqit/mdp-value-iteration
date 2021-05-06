using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CellObj : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    public Cell cell = new Cell();
    public MDP mdp;

    public bool arrow = false;
    
    public enum CellType {neutral, positive, negative, wall};

    void Start()
    {
        mdp = GameObject.Find("MDP").GetComponent<MDP>();

        cell.value = 0;
        cell.cellType = CellType.neutral;
        cell.reward = mdp.penalty;
        cell.bestAction = MDP.Action.up;

        this.transform.GetChild(0).GetComponent<TMP_Text>().text = cell.value.ToString();
        StartCoroutine("UPD");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if(cell.cellType == CellType.neutral)
            {
                if (!arrow)
                {
                    transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;
                    arrow = !arrow;
                }
                else
                {
                    transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;
                    arrow = !arrow;
                }
            }
        }
    }


    IEnumerator UPD()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            this.transform.GetChild(0).GetComponent<TMP_Text>().text = cell.value.ToString();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("DOWN");
        if (Input.GetMouseButtonDown(0))
        {
            ChangeCellState();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChangeCellToWall();
        }

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            ChangeCellState();
        }

        if (Input.GetMouseButton(1))
        {
            ChangeCellToWall();
        }

    }

    private void ChangeCellState()
    {
        if (cell.cellType == CellType.neutral)
        {
            this.GetComponent<Image>().color = Color.red;
            cell.cellType = CellType.negative;
            cell.value = mdp.badReward;
            cell.reward = mdp.penalty;
        }
        else if (cell.cellType == CellType.positive)
        {
            this.GetComponent<Image>().color = Color.white;
            cell.cellType = CellType.neutral;
            cell.value = 0;
            cell.reward = mdp.penalty;
        }
        else if (cell.cellType == CellType.negative)
        {
            this.GetComponent<Image>().color = Color.green;
            cell.cellType = CellType.positive;
            cell.value = mdp.goodReward;
            cell.reward = mdp.penalty;
        }

        this.transform.GetChild(0).GetComponent<TMP_Text>().text = cell.value.ToString();
    }

    private void ChangeCellToWall()
    {
        if (cell.cellType == CellType.neutral)
        {
            this.GetComponent<Image>().color = Color.gray;
            cell.cellType = CellType.wall;
        }
        else if (cell.cellType == CellType.wall)
        {
            this.GetComponent<Image>().color = Color.white;
            cell.cellType = CellType.neutral;
        }

        this.transform.GetChild(0).GetComponent<TMP_Text>().text = cell.value.ToString();
    }

}
