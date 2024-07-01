using System.Collections.Generic;
using UnityEngine;

public class BoardGame : MonoBehaviour
{
    [SerializeField] private List<Row> boardGameObjects = new List<Row>();
    [HideInInspector] public List<List<int>> board = new List<List<int>>();

    private List<List<int>> GetIntBoard()
    {
        List<List<int>> intBoard = new List<List<int>> { new List<int> { 0,0,0,0 },
                                                         new List<int> { 0,0,0,0 },
                                                         new List<int> { 0,0,0,0 },
                                                         new List<int> { 0,0,0,0 }
                                                        };

        for (int y = 0; y < boardGameObjects.Count; y++)
        {
            for (int x = 0; x < boardGameObjects[y].platforms.Count; x++)
            {
                intBoard[y][x] = boardGameObjects[y].platforms[x].GetComponent<TypePlatform>().typePlatform;
            }
        }

        return intBoard;
    }

    public List<List<int>> GetCurrentBoard()
    {
        board = GetComponent<BotManager>().MotionManager.NextMotionOnBoard(GetIntBoard());
        return board;
    }
}

[System.Serializable]
public struct Row
{
    public List<GameObject> platforms;
}