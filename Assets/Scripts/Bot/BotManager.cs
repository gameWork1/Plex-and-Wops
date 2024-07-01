using UnityEngine;

public class BotManager : MonoBehaviour
{
    BoardGame boardGame => GetComponent<BoardGame>();
    private WinChecker _WinChecker => new WinChecker();
    [HideInInspector] public NextMotion MotionManager => new NextMotion();
    
    void SetPoint()
    {
        var boardInt = boardGame.GetCurrentBoard();
        //var board = boardGame.boardGameObjects;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(_WinChecker.CheckWin(1, boardGame.GetCurrentBoard()));
        }
    }


}


