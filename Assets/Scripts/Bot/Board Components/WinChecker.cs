using System.Collections.Generic;

public class WinChecker
{
    int CheckWin(int botId, List<List<int>> localBoardInt)
    {
        int winPlayer = 0;
        int ver = winVertical(botId, localBoardInt);
        int hor = winHorizontal(botId, localBoardInt);
        int diaganelPlus = winDiaganel(botId, localBoardInt, 1);
        int diaganelMinus = winDiaganel(botId, localBoardInt, -1);

        bool player = false;
        bool bot = false;

        if (ver > 0) bot = true;
        else player = true;
        if (hor > 0) bot = true;
        else player = true;
        if (diaganelPlus > 0) bot = true;
        else player = true;
        if (diaganelMinus > 0) bot = true;
        else player = true;

        if (player && !bot) winPlayer = -botId;
        if (!player && bot) winPlayer = botId;
        if (!player && !bot) winPlayer = -2;

        return winPlayer;
    }

    int winVertical(int botId, List<List<int>> localBoardInt)
    {
        int winPlayer = 0;
        bool player = false;
        bool bot = false;
        for (int x = 0; localBoardInt[0].Count > x; x++)
        {
            if (!player) player = false;
            if (!bot) bot = false;
            for (int y = 0; localBoardInt.Count > y; y++)
            {
                if (localBoardInt[y][x] == 0)
                {
                    winPlayer = -2;
                    break;
                }
                else
                {
                    if ((localBoardInt[y][x] != botId && bot) || (localBoardInt[y][x] != -botId && player))
                    {
                        winPlayer = -2;
                        break;
                    }
                }
            }
        }
        if (winPlayer != -2)
        {
            if (player && !bot)
                winPlayer = -botId;
            else if (!player && bot)
                winPlayer = botId;
        }
        return winPlayer;
    }

    int winHorizontal(int botId, List<List<int>> localBoardInt)
    {
        int winPlayer = 0;
        bool player = false;
        bool bot = false;
        for (int x = 0; localBoardInt[0].Count > x; x++)
        {
            if (!player) player = false;
            if (!bot) bot = false;
            for (int y = 0; localBoardInt.Count > y; y++)
            {
                if (localBoardInt[x][y] == 0)
                {
                    winPlayer = -2;
                    break;
                }
                else
                {
                    if ((localBoardInt[x][y] != botId && bot) || (localBoardInt[x][y] != -botId && player))
                    {
                        winPlayer = -2;
                        break;
                    }
                }
            }
        }
        if (winPlayer != -2)
        {
            if (player && !bot)
                winPlayer = -botId;
            else if (!player && bot)
                winPlayer = botId;
        }
        return winPlayer;
    }

    int winDiaganel(int botId, List<List<int>> localBoardInt, int typeDiagnel)
    {
        int winPlayer = 0;
        bool player = false;
        bool bot = false;
        if (typeDiagnel > 0)
        {
            for (int x = 0, y = 0; localBoardInt[0].Count > x && localBoardInt[0].Count > y; x++, y++)
            {
                if (!player) player = false;
                if (!bot) bot = false;

                if (localBoardInt[x][y] == 0)
                {
                    winPlayer = -2;
                    break;
                }
                else
                {
                    if ((localBoardInt[x][y] != botId && bot) || (localBoardInt[x][y] != -botId && player))
                    {
                        winPlayer = -2;
                        break;
                    }
                }
            }
        }
        else
        {
            for (int x = localBoardInt[0].Count - 1, y = localBoardInt[0].Count - 1; x >= 0 && y >= 0; x--, y--)
            {
                if (!player) player = false;
                if (!bot) bot = false;

                if (localBoardInt[x][y] == 0)
                {
                    winPlayer = -2;
                    break;
                }
                else
                {
                    if ((localBoardInt[x][y] != botId && bot) || (localBoardInt[x][y] != -botId && player))
                    {
                        winPlayer = -2;
                        break;
                    }
                }
            }
        }

        if (winPlayer != -2)
        {
            if (player && !bot)
                winPlayer = -botId;
            else if (!player && bot)
                winPlayer = botId;
        }
        return winPlayer;
    }
}