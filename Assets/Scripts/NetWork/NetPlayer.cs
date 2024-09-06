using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
public class NetPlayer: MonoBehaviour
{
    public ChessType chessColor = ChessType.Black;
    //Button button;
    public bool isDoubleMode = false;
    protected virtual void Start()
    {
        //button = GameObject.Find("RetractBtn").GetComponent<Button>();
        //if (PlayerPrefs.GetInt("Double") == 10)
        //{
        //    isDoubleMode = true;
        //}
    }
    protected virtual void FixedUpdate()
    {
        //if (chessColor == ChessBooard.Instance.turn && ChessBooard.Instance.timer > 0.5f)
        //    PlayerChess();
        //if (!isDoubleMode)
        //{
        //    ChangeBtnColor();
        //}

    }
    public virtual void PlayerChess()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//屏幕坐标转世界坐标
            
            if (ChessBooard.Instance.PlayerChess(new int[2] { (int)(pos.x + 7.5f), (int)(pos.y + 7.5f) })) ;
            ChessBooard.Instance.timer = 0;
        }
    }
    //protected virtual void ChangeBtnColor()
    //{
    //    if (chessColor == ChessType.Watch)
    //    {
    //        return;
    //    }
    //    if (ChessBooard.Instance.turn == chessColor)
    //    {
    //        button.interactable = true;
    //    }
    //    else
    //    {
    //        button.interactable = false;
    //    }

    //}

}
