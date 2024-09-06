using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIleveTwo : AIleveOne
{
    protected override void Start()
    {
  
        toScore.Add("aa___",100); //眠二
        toScore.Add("a_a__",100);
        toScore.Add("___aa",100);
        toScore.Add("__a_a",100);
        toScore.Add("a__a_",100);
        toScore.Add("_a__a",100);
        toScore.Add("a___a",100);

        toScore.Add("__aa__", 500);//活二
        toScore.Add("_a_a_", 500);
        toScore.Add("_a__a_", 500);
        toScore.Add("_aa__",500);
        toScore.Add("__aa_",500);

        toScore.Add("a_a_a", 1000);//眠三
        toScore.Add("aa__a", 1000);
        toScore.Add("_aa_a", 1000);
        toScore.Add("a_aa_", 1000);
        toScore.Add("_a_aa", 1000);
        toScore.Add("aa_a_", 1000);
        toScore.Add("aaa__", 1000);

        toScore.Add("_aa_a_", 9000);//跳活三
        toScore.Add("_a_aa_", 9000);
       
        
        toScore.Add("_aaa_", 10000);//活三

        toScore.Add("a_aaa", 15000);//冲四
        toScore.Add("aaa_a", 15000);
        toScore.Add("_aaaa", 15000);
        toScore.Add("aaaa_", 15000);
        toScore.Add("aa_aa", 15000);

        toScore.Add("_aaaa_", 15000);//活四

        toScore.Add("aaaaa", float.MaxValue);//连五
        if (chessColor != ChessType.Watch)
            Debug.Log(chessColor + "AITWo");

    }
    public override void CheckOneLine(int[] pos, int[] offset, int chess)
    {
        bool lfirst =true, lStop=false,rStop =false;//lfist=ture 左扫棋,flase右扫棋，lStop，rStop向左右扫棋判断是否为白子，为白子停止，
        int AllNum = 1;//最多扫棋数，
        string str = "a";
        int ri = offset[0], rj = offset[1];//右边遍历
        int li = -offset[0], lj = -offset[1];
        while (AllNum < 7 && (!lStop || !rStop))//最多扫棋数为7 当lStop,rStop==ture 停止扫棋
        {
            if(lfirst)//左边扫棋
            {
                if((pos[0] + li >= 0 && pos[0] + li < 15) && pos[1] + lj >= 0 &&
                    pos[1] + lj < 15 &&!lStop)//左边安全校验
                {
                    if (ChessBooard.Instance.grid[pos[0] + li, pos[1] + lj] == chess)
                    {
                        AllNum++;
                        str="a"+str;
                    }
                    else if (ChessBooard.Instance.grid[pos[0] + li, pos[1] + lj] == 0)
                    {
                        AllNum++;
                        str="_"+str;
                      if(!rStop)  lfirst = false;//如果右边没有停止 就向右边扫
                    }
                    else
                    {
                        lStop=true;
                        if (!rStop) lfirst = false;
                    }
                    li -= offset[0]; lj -= offset[1];

                }
                else
                {
                    lStop = true;
                    if (!rStop) lfirst = false;
                }
            }
           else
            {
                if ((pos[0] + ri >= 0 && pos[0] + ri < 15) && pos[1] + rj >= 0 &&
                   pos[1] + rj < 15 && !lfirst && !rStop)
                {//右边安全校验
                    if (ChessBooard.Instance.grid[pos[0] + ri, pos[1] + rj] == chess)
                    {
                        AllNum++;
                        str += "a";
                    }
                    else if (ChessBooard.Instance.grid[pos[0] + ri, pos[1] + rj] == 0)
                    {
                        AllNum++;
                        str += "_";
                        if (!lStop) lfirst = true;//向右扫棋 如果lStop没有停止 lfirst变成true
                    }
                    else
                    {
                        rStop = true;
                        if (!lStop) lfirst = true;
                    }
                    ri += offset[0]; rj += offset[1];
                }
                else
                {
                    rStop = true;
                    if (!lStop) lfirst = true;
                }
           }
            
        }
        string cmpStr = "";//比较
        foreach(var keyInfo in toScore)
        {
            if (str.Contains(keyInfo.Key))
            {
                if (cmpStr != "")
                {
                    if (toScore[keyInfo.Key] > toScore[cmpStr])//比较最大值最小值
                    {
                        cmpStr = keyInfo.Key;
                    }
                }
                else
                {
                    cmpStr = keyInfo.Key;//当前字符是空记录一次
                }
            }
        }
        if (cmpStr != "")
        {
            score[pos[0], pos[1]] += toScore[cmpStr];
        }
    }
    protected override void ChangeBtnColor()
    {

    }
}
