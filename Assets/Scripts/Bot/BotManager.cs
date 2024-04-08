using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    BoardGame boardGame => GetComponent<BoardGame>();
    WinChecker WinChecker => new WinChecker();
    [HideInInspector] public NextMotion MotionManager => new NextMotion();
    
    void SetPoint()
    {
        var boardInt = boardGame.GetIntBoard();
        var board = boardGame.board;
        
    }

    
}


