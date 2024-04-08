using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NextMotion
{
    public List<List<int>> NextMotionOnBoard(List<List<int>> localBoardInt)
    {
        var boardInt = new List<List<int>> { new List<int> { 0,0,0,0 },
                                            new List<int> { 0,0,0,0 },
                                            new List<int> { 0,0,0,0 },
                                            new List<int> { 0,0,0,0 }};

        int multipule = 1;
        bool XMoving = false;

        int x = 0, y = 1;
        int oldX = 0, oldY = 0;

        int changeSide = 0;
        int i = 0;
        while (true)
        {
            boardInt[y][x] = localBoardInt[oldY][oldX];
            oldX = x;
            oldY = y;

            if ((XMoving && x % 3 == 0) || (!XMoving && y % 3 == 0))
            {
                XMoving = !XMoving;
                changeSide++;
                if (changeSide % 2 == 0)
                {
                    multipule = -multipule;
                }
            }

            if (XMoving) x += 1 * multipule;
            else y += 1 * multipule;

            if (x == 0 && y == 1 && changeSide > 0)
            {
                break;
            }
            
            //Debug.Log((boardInt[y][x] + "  Y: " + y + "  X: " + x + "   " + boardInt[oldY][oldX] + "  Y: " + oldY + "  X: " + oldX).ToString());

            if (x > 3) x = 3;
            if (x < 0) x = 0;
            if(y < 0) y = 0;
            if(y > 3) y = 3;
        }

        return boardInt;
    }
}
