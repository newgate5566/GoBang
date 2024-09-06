using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AIleveTwo : AIleveOne
{
    protected override void Start()
    {
  
        toScore.Add("aa___",100); //�߶�
        toScore.Add("a_a__",100);
        toScore.Add("___aa",100);
        toScore.Add("__a_a",100);
        toScore.Add("a__a_",100);
        toScore.Add("_a__a",100);
        toScore.Add("a___a",100);

        toScore.Add("__aa__", 500);//���
        toScore.Add("_a_a_", 500);
        toScore.Add("_a__a_", 500);
        toScore.Add("_aa__",500);
        toScore.Add("__aa_",500);

        toScore.Add("a_a_a", 1000);//����
        toScore.Add("aa__a", 1000);
        toScore.Add("_aa_a", 1000);
        toScore.Add("a_aa_", 1000);
        toScore.Add("_a_aa", 1000);
        toScore.Add("aa_a_", 1000);
        toScore.Add("aaa__", 1000);

        toScore.Add("_aa_a_", 9000);//������
        toScore.Add("_a_aa_", 9000);
       
        
        toScore.Add("_aaa_", 10000);//����

        toScore.Add("a_aaa", 15000);//����
        toScore.Add("aaa_a", 15000);
        toScore.Add("_aaaa", 15000);
        toScore.Add("aaaa_", 15000);
        toScore.Add("aa_aa", 15000);

        toScore.Add("_aaaa_", 15000);//����

        toScore.Add("aaaaa", float.MaxValue);//����
        if (chessColor != ChessType.Watch)
            Debug.Log(chessColor + "AITWo");

    }
    public override void CheckOneLine(int[] pos, int[] offset, int chess)
    {
        bool lfirst =true, lStop=false,rStop =false;//lfist=ture ��ɨ��,flase��ɨ�壬lStop��rStop������ɨ���ж��Ƿ�Ϊ���ӣ�Ϊ����ֹͣ��
        int AllNum = 1;//���ɨ������
        string str = "a";
        int ri = offset[0], rj = offset[1];//�ұ߱���
        int li = -offset[0], lj = -offset[1];
        while (AllNum < 7 && (!lStop || !rStop))//���ɨ����Ϊ7 ��lStop,rStop==ture ֹͣɨ��
        {
            if(lfirst)//���ɨ��
            {
                if((pos[0] + li >= 0 && pos[0] + li < 15) && pos[1] + lj >= 0 &&
                    pos[1] + lj < 15 &&!lStop)//��߰�ȫУ��
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
                      if(!rStop)  lfirst = false;//����ұ�û��ֹͣ �����ұ�ɨ
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
                {//�ұ߰�ȫУ��
                    if (ChessBooard.Instance.grid[pos[0] + ri, pos[1] + rj] == chess)
                    {
                        AllNum++;
                        str += "a";
                    }
                    else if (ChessBooard.Instance.grid[pos[0] + ri, pos[1] + rj] == 0)
                    {
                        AllNum++;
                        str += "_";
                        if (!lStop) lfirst = true;//����ɨ�� ���lStopû��ֹͣ lfirst���true
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
        string cmpStr = "";//�Ƚ�
        foreach(var keyInfo in toScore)
        {
            if (str.Contains(keyInfo.Key))
            {
                if (cmpStr != "")
                {
                    if (toScore[keyInfo.Key] > toScore[cmpStr])//�Ƚ����ֵ��Сֵ
                    {
                        cmpStr = keyInfo.Key;
                    }
                }
                else
                {
                    cmpStr = keyInfo.Key;//��ǰ�ַ��ǿռ�¼һ��
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
