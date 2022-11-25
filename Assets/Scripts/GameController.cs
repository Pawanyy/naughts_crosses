using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int[] placements = new int[9];
    //0 - Unplaced
    //1 - Nought
    //2 - Cross

    [SerializeField] GameObject noughtPrefab;
    [SerializeField] GameObject crossPrefab;

    public GameObject currentPlayerPrefab;

    [SerializeField] int CurrentPlayer;

    private void Start()
    {
        currentPlayerPrefab = noughtPrefab;
        CurrentPlayer = 1;
        
        for(int i = 0; i < placements.Length; i++)
        {
            placements[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool clickedTile = false;
        GameObject _selectedTile = null;

        //Check if Pressing Mouse
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector2.zero);

            if(hit.collider != null)
            {
                //This Means We have Hit Something

                if (hit.collider.name.Contains("Tile"))
                {
                    clickedTile = true;
                    _selectedTile = hit.collider.gameObject;
                }
                
                Debug.Log("We Hit Obj => " + hit.collider.name);
            }
        }

        if (clickedTile)
        {

            GameObject _newPrefab = GameObject.Instantiate(currentPlayerPrefab);
            _newPrefab.transform.position = _selectedTile.transform.position;

            string name = _selectedTile.name.Replace("Tile", "");
            name = name.Trim();
            int index = int.Parse(name);

            placements[index-1] = CurrentPlayer;

            Destroy(_selectedTile);

            if (Checking(CurrentPlayer))
            {
                if(CurrentPlayer == 1)
                {

                }
                else
                {

                }

            }

            ChangeTurn();
        }
    }

    void ChangeTurn()
    {
        if(currentPlayerPrefab == noughtPrefab)
        {
            currentPlayerPrefab = crossPrefab;
            CurrentPlayer = 2;
        }
        else
        {
            currentPlayerPrefab = noughtPrefab;
            CurrentPlayer = 1;
        }
    }

    bool Checking(int PlayerCheck)
    {
        //Horizontal Same
        if (placements[0] == PlayerCheck && placements[1] == PlayerCheck && placements[2] == PlayerCheck)
        {
            return true;
        }

        if (placements[3] == PlayerCheck && placements[4] == PlayerCheck && placements[5] == PlayerCheck)
        {
            return true;
        }

        if (placements[6] == PlayerCheck && placements[7] == PlayerCheck && placements[8] == PlayerCheck)
        {
            return true;
        }

        //Vertical Same
        if (placements[0] == PlayerCheck && placements[3] == PlayerCheck && placements[6] == PlayerCheck)
        {
            return true;
        }

        if (placements[3] == PlayerCheck && placements[4] == PlayerCheck && placements[5] == PlayerCheck)
        {
            return true;
        }

        if (placements[6] == PlayerCheck && placements[7] == PlayerCheck && placements[8] == PlayerCheck)
        {
            return true;
        }

        //Diagonal Same
        if (placements[0] == PlayerCheck && placements[4] == PlayerCheck && placements[8] == PlayerCheck)
        {
            return true;
        }

        if (placements[2] == PlayerCheck && placements[4] == PlayerCheck && placements[6] == PlayerCheck)
        {
            return true;
        }

        return false;
    }
}
