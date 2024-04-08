using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGame : MonoBehaviour
{
    public List<Row> board = new List<Row>();

    public List<List<int>> GetIntBoard()
    {
        List<List<int>> intBoard = new List<List<int>> { new List<int> { 0,0,0,0 },
                                                        new List<int> { 0,0,0,0 },
                                                        new List<int> { 0,0,0,0 },
                                                        new List<int> { 0,0,0,0 }};

        for (int y = 0; y < board.Count; y++)
        {
            for (int x = 0; x < board[y].platforms.Count; x++)
            {
                intBoard[y][x] = board[y].platforms[x].GetComponent<TypePlatform>().typePlatform;
            }
        }

        return intBoard;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            var board = GetComponent<BotManager>().MotionManager.NextMotionOnBoard(GetIntBoard());

            for(int y = 0; y < board.Count; y++)
            {
                for (int x = 0; x < board.Count; x++)
                {
                    Debug.Log("X: " + x + " " + "Y: " + y + "  " + "Value: " + board[y][x]);
                }
            }
        }
    }
}

[System.Serializable]
public struct Row
{
    public List<GameObject> platforms;
}