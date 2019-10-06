using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class ParallaxBackground : MonoBehaviour
{
    public GameObject tile;
    public GameObject containter;
    public GameObject player;
    public int SlowDownFactor = 300;
    public float tileWidth = 5f;

    private List<GameObject> TilesArr = new List<GameObject> { };

//this is a method for shifting the tiles in any direction.
    public List<GameObject> arrangeTiles(List<GameObject> arrayToModify, int startingTile, bool columnMod,
        int iterationQty, int xMod, int yMod, int newArrayStartingIndex)
    {
        List<GameObject> arrayClone = new List<GameObject>(arrayToModify);
        //COLUMN
        if (columnMod)
        {
            for (int i = startingTile; i < startingTile + (iterationQty * 3); i += 3)
            {
                //modifying columns does not need an IF statement with respect to the starting index and the insert index because I can only move the column 2 spaces away max, 
                //but the script iterates by 3 units so the each next iteration retains the correct element in the 2nd and 3rd position upon completion of the loop.  VERY CONFUSING BUT IT WORKS!

                arrayClone[i].transform.position =
                    arrayClone[i].transform.position + new Vector3(xMod * tileWidth, yMod * tileWidth, 0);
                GameObject thisTile = arrayClone[i];
                arrayClone.RemoveAt(i);
                arrayClone.Insert((i - startingTile) + newArrayStartingIndex, thisTile);
            }

            return arrayClone;
            //ROW
        }
        else
        {
            for (int i = startingTile; i < (startingTile + iterationQty); i++)
            {
                if (startingTile > newArrayStartingIndex)
                {
                    arrayClone[i].transform.position =
                        arrayClone[i].transform.position + new Vector3(xMod * tileWidth, yMod * tileWidth, 0);
                    GameObject thisTile = arrayClone[i];
                    arrayClone.RemoveAt(i);
                    arrayClone.Insert((i - startingTile) + newArrayStartingIndex, thisTile);
                }
                else
                {
                    arrayClone[startingTile].transform.position =
                        arrayClone[startingTile].transform.position +
                        new Vector3(xMod * tileWidth, yMod * tileWidth, 0);
                    GameObject thisTile = arrayClone[startingTile];
                    arrayClone.RemoveAt(startingTile);
                    arrayClone.Add(thisTile);
                }
            }

            return arrayClone;
        }
    }


// Use this for initialization
    void Start()
    {
        float pX = player.transform.position.x;
        float pY = player.transform.position.y;
        for (int i = 0; i < 9; i++)
        {
            GameObject Tile = Instantiate(tile);
            Tile.name = "t" + i;
            Tile.transform.SetParent(containter.transform, false);
            Tile.transform.localScale = new Vector2(tileWidth, tileWidth);
            int rotation = Random.Range(0, 3);
            Tile.transform.Rotate(0, 0, rotation * 90);

            if (i <= 2)
            {
                Tile.transform.position = new Vector2((pX - 1 + i) * tileWidth, pY + tileWidth);
            }
            else if (i >= 3 && i <= 5)
            {
                Tile.transform.position = new Vector2((pX - 4 + i) * tileWidth, pY);
            }
            else
            {
                Tile.transform.position = new Vector2((pX - 7 + i) * tileWidth, pY - tileWidth);
            }

            TilesArr.Add(Tile);
        }
    }

// Update is called once per frame
    private int counter = 0;

    void Update()
    {
        if (counter == 0)
        {
            Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity / SlowDownFactor;
            Vector2 pTrans = (Vector2) player.transform.position;


            //we just grab player velocity and slowed it down. 
            //tile index starts at 0 on each Update() call
            int tileIndex = 0;
            foreach (var Tile in TilesArr)
            {
                //loop starts with complete=false 
                bool complete = false;


                Tile.transform.position = new Vector2(Tile.transform.position.x - playerVelocity.x,
                    Tile.transform.position.y - playerVelocity.y);
                Vector2 tTrans = Tile.transform.position;

                if (tileIndex != 4 && (Mathf.Abs(pTrans.x - tTrans.x) < (tileWidth / 2)) &&
                    (Mathf.Abs(pTrans.y - tTrans.y) < (tileWidth / 2)))
                {
                    List<GameObject> moveOne;

                    switch (tileIndex)
                    {
                        case 0:
                            moveOne = arrangeTiles(TilesArr, 6, false, 3, -1, 3, 0);
                            TilesArr = new List<GameObject>(arrangeTiles(moveOne, 5, true, 2, -3, 0, 3));
                            for (int i = 0; i < 9; i++)
                            {
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        case 1:
                            TilesArr = new List<GameObject>(arrangeTiles(TilesArr, 6, false, 3, 0, 3, 0));
                            for (int i = 0; i < 9; i++)
                            {
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        case 2:
                            moveOne = arrangeTiles(TilesArr, 6, false, 3, 1, 3, 0);
                            TilesArr = new List<GameObject>(arrangeTiles(moveOne, 3, true, 2, 3, 0, 5));
                            for (int i = 0; i < 9; i++)
                            {
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        case 3:
                            TilesArr = new List<GameObject>(arrangeTiles(TilesArr, 2, true, 3, -3, 0, 0));
                            for (int i = 0; i < 9; i++)
                            {
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        //there is no case 4 because that is center tile.
                        case 5:
                            TilesArr = new List<GameObject>(arrangeTiles(TilesArr, 0, true, 3, 3, 0, 2));
                            for (int i = 0; i < 9; i++)
                            {
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        //WHY IS THE BOTTOM ROW NOT WORKING??
                        case 6:
                            moveOne = arrangeTiles(TilesArr, 0, false, 3, -1, -3, 6);
                            TilesArr = new List<GameObject>(arrangeTiles(moveOne, 2, true, 2, -3, 0, 0));
                            for (int i = 0; i < 9; i++)
                            {
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        case 7:
                            TilesArr = new List<GameObject>(arrangeTiles(TilesArr, 3, false, 3, 0, -3, 6));

                            for (int i = 0; i < 9; i++)
                            {
                                Debug.Log(TilesArr[i].name);
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        case 8:
                            moveOne = arrangeTiles(TilesArr, 0, false, 3, 1, -3, 6);
                            TilesArr = new List<GameObject>(arrangeTiles(moveOne, 0, true, 2, 3, 0, 2));
                            for (int i = 0; i < 9; i++)
                            {
                                TilesArr[i].transform.position = new Vector2(TilesArr[i].transform.position.x,
                                    TilesArr[i].transform.position.y);
                            }

                            complete = true;
                            break;
                        default:
                            Debug.Log("were in the middle");
                            break;
                    }
                }

                tileIndex += 1;
                if (complete)
                {
                    break;
                }
            }

            //counter = 0;
        }

        //counter = 1;
    }
}