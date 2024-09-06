using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Player> PlayerList = new List<Player>();
    private void Awake()
    {
        int player1 = PlayerPrefs.GetInt("Player");//ºÚÆåÍæ¼Ò
        int player2 = PlayerPrefs.GetInt("Player2");//°×ÆåÍæ¼Ò
       // PlayerPrefs.SetInt("Double", 1);
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (player1 == i)
            {
                PlayerList[i].chessColor = ChessType.Black;
            }else if (player2 == i)
            {
                PlayerList[i].chessColor = ChessType.White;
            }
            else
            {
                PlayerList[i].chessColor = ChessType.Watch;
            }

        }
    }
    public void SetPlayer(int i)
    {
        PlayerPrefs.SetInt("Player",i);
    }
    public void SetPlayer2(int i)
    {
        PlayerPrefs.SetInt("Player2", i);
    }
    public void PlayerGame()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayerNetGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ChangeChessColor()
    {
        for (int i = 0;i < PlayerList.Count;i++)
        {
            if (PlayerList[i].chessColor == ChessType.Black)
            {
                SetPlayer2(i);
            }
            else if (PlayerList[i].chessColor == ChessType.White)
            {
                    SetPlayer(i);
            } 
            else
            {
                PlayerList[i].chessColor = ChessType.Watch;    
            }
        }
        SceneManager.LoadScene(1);
    }
    public void Double()
    {
        PlayerPrefs.SetInt("Double", 10);

    }
    public void OnRturnBtn()
    {
        PlayerPrefs.SetInt("Double", 1);
        SceneManager.LoadScene(1);
    }
}
