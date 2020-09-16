using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] spriteArray;
    //TextAsset levelMap = Resources.Load<TextAsset>("PacMan Level Map");
    int[,] levelMap =
     {
     // -> j
     // |
     // v
     // i
         {1,2,2,2,2,2,2,2,2,2,2,2,2,7},     // 0: Empty
         {2,5,5,5,5,5,5,5,5,5,5,5,5,4},     // 1: Outside Corner
         {2,5,3,4,4,3,5,3,4,4,4,3,5,4},     // 2: Outside Wall
         {2,6,4,0,0,4,5,4,0,0,0,4,5,4},     // 3: Inside Corner
         {2,5,3,4,4,3,5,3,4,4,4,3,5,3},     // 4: Inside Wall
         {2,5,5,5,5,5,5,5,5,5,5,5,5,5},     // 5: Standard Pellet
         {2,5,3,4,4,3,5,3,3,5,3,4,4,4},     // 6: Power Pellet
         {2,5,3,4,4,3,5,4,4,5,3,4,4,3},     // 7: T-Junction
         {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
         {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
         {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
         {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
         {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
         {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
         {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
     };
    private Transform[,] mapGrid;

    void Start()
    {
        mapGrid = new Transform[levelMap.GetLength(0), levelMap.GetLength(1)];
        LoadLevel();
        CorrectRotations();
    }

    public void LoadLevel() //Instantiates all sprites in correct x,z coordinates *16x16 pixels are scaled by 6.25 (100/16) to fit the grid in prefabs already
    {
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                Transform newSprite = (Transform)Instantiate(spriteArray[levelMap[i, j]], new Vector3(j, 0, -i), Quaternion.Euler(new Vector3(90, 0, 0))); //Rotation 90°x to face camera
                mapGrid[i, j] = newSprite;
            }
        }
    }

    public void CorrectRotations() //Rotates all walls and corner pieces correctly based on surrounding walls
    {
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                switch (levelMap[i, j])
                {
                    case 1:
                        if ((i > 0 && j < levelMap.GetLength(1) - 1) && (levelMap[i - 1, j] == 2 && levelMap[i, j + 1] == 2))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 90.0f);
                        }
                        else if ((i > 0 && j > 0) && (levelMap[i - 1, j] == 2 && levelMap[i, j - 1] == 2))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 180.0f);
                        }
                        else if ((i < levelMap.GetLength(0) - 1 && j > 0) && (levelMap[i + 1, j] == 2 && levelMap[i, j - 1] == 2))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 270.0f);
                        }
                        break;
                    case 2:
                        if ((i > 0 && i < levelMap.GetLength(0) - 1) && (levelMap[i + 1, j] == 2 || levelMap[i - 1, j] == 2))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 90.0f);
                        }
                        break;
                    case 3:
                        

                        if ((i > 0 && j < levelMap.GetLength(1) - 1) && ((levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4) && (levelMap[i, j + 1] == 3 || levelMap[i, j + 1] == 4)))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 90.0f);
                        }
                        else if ((i > 0 && j > 0) && ((levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4) && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4)))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 180.0f);
                        }
                        else if ((i < levelMap.GetLength(0) - 1 && j > 0) && ((levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4) && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4)))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 270.0f);
                        }
                        break;
                    case 4:
                        if ((i > 0 && i < levelMap.GetLength(0) - 1) && (levelMap[i + 1, j] == 3 || levelMap[i - 1, j] == 3 || levelMap[i + 1, j] == 7 || levelMap[i - 1, j] == 7))
                        {
                            mapGrid[i, j].transform.Rotate(0.0f, 0.0f, 90.0f);
                        }
                        break;
                    case 7:
                        break;
                }
            }
        }
    }
}
