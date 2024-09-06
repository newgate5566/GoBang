using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinMaxNode 
{
    public int chess;//�������ɫ
    public int[] pos;//�õ��λ�� x,y
    public List<MinMaxNode> child;//��  �����ӽڵ�
    public float value;
}

public class AIleveThree :Player
{
    Dictionary<string, float> toScore = new Dictionary<string, float>();
    protected override void ChangeBtnColor()
    {

    }
    protected override void Start()
    {

        toScore.Add("aa___", 100); //�߶�
        toScore.Add("a_a__", 100);
        toScore.Add("___aa", 100);
        toScore.Add("__a_a", 100);
        toScore.Add("a__a_", 100);
        toScore.Add("_a__a", 100);
        toScore.Add("a___a", 100);

        toScore.Add("__aa__", 500);//���
        toScore.Add("_a_a_", 500);
        toScore.Add("_a__a_", 500);
        toScore.Add("_aa__", 500);
        toScore.Add("__aa_", 500);

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
            Debug.Log(chessColor + "AIThree");

    }
    public  float CheckOneLine(int [,] grid,int[] pos, int[] offset, int chess)
    {
        float score = 0;
        bool lfirst = true, lStop = false, rStop = false;//lfist=ture ��ɨ��,flase��ɨ�壬lStop��rStop������ɨ���ж��Ƿ�Ϊ���ӣ�Ϊ����ֹͣ��
        int AllNum = 1;//���ɨ������
        string str = "a";
        int ri = offset[0], rj = offset[1];//�ұ߱���
        int li = -offset[0], lj = -offset[1];
        while (AllNum < 7 && (!lStop || !rStop))//���ɨ����Ϊ7 ��lStop,rStop==ture ֹͣɨ��
        {
            if (lfirst)//���ɨ��
            {
                if ((pos[0] + li >= 0 && pos[0] + li < 15) && pos[1] + lj >= 0 &&
                    pos[1] + lj < 15 && !lStop)//��߰�ȫУ��
                {
                    if (grid[pos[0] + li, pos[1] + lj] == chess)
                    {
                        AllNum++;
                        str = "a" + str;
                    }
                    else if (grid[pos[0] + li, pos[1] + lj] == 0)
                    {
                        AllNum++;
                        str = "_" + str;
                        if (!rStop) lfirst = false;//����ұ�û��ֹͣ �����ұ�ɨ
                    }
                    else
                    {
                        lStop = true;
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
                    if (grid[pos[0] + ri, pos[1] + rj] == chess)
                    {
                        AllNum++;
                        str += "a";
                    }
                    else if (grid[pos[0] + ri, pos[1] + rj] == 0)
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
        foreach (var keyInfo in toScore)
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
            score += toScore[cmpStr];
        }
        return score;
    }
    public float GetScore(int[,]grid,int[] pos)
    {

        float score = 0;
       score+= CheckOneLine(grid,pos, new int[2] { 1, 0 }, 1);
        score += CheckOneLine(grid, pos, new int[2] { 1, 1 }, 1);
        score += CheckOneLine(grid, pos, new int[2] { 1, -1 }, 1);
        score += CheckOneLine(grid, pos, new int[2] { 0, 1 }, 1);

        score += CheckOneLine(grid, pos, new int[2] { 1, 0 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { 1, 1 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { 1, -1 }, 2);
        score += CheckOneLine(grid, pos, new int[2] { 0, 1 }, 2);
        return score;
    }
    public override void PlayerChess()
    {
        if (ChessBooard.Instance.chessStack.Count == 0)
        {
            if (ChessBooard.Instance.PlayerChess(new int[2] { 7, 7 }))
                ChessBooard.Instance.timer = 0;
            return;
        }
        MinMaxNode node = null;
        foreach(var item in GetList(ChessBooard.Instance.grid,(int)chessColor,true))
        {
            CreateTree(item, (int[,])ChessBooard.Instance.grid.Clone(),3,false);
            float  a=float.MinValue; float b=float.MaxValue;
            item.value += AlphaBeta(item,3,false,a,b);
            if(node != null)//��ѡ�������
            {
                if(node.value <item.value)
                {
                    node = item;
                }
            }
            else
            {
                node = item;
            }
        }
        ChessBooard.Instance.PlayerChess(node.pos);
    }
    //���ؽڵ� ����С
    List<MinMaxNode> GetList(int[,]grid,int chess,bool mySelf)//����ĸ�ʽ����ǰ�������ɫ��ʲô����ǰ�Ƿ��Լ�������
    {
        List<MinMaxNode> Nodelist=new List<MinMaxNode>();
        MinMaxNode node;//�ڵ�
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                int [] pos=new int[2] {i,j};
                   
                if (grid[pos[0], pos[1]] != 0) continue;//�жϵ�ǰ�Ƿ����������
                node=new MinMaxNode();
                node.pos = pos;
                node.chess = chess;
                if(mySelf)//�����
                {
                    node.value = GetScore(grid,pos);
                }
                else
                {
                    node.value = -GetScore(grid, pos);
                }

                if (Nodelist.Count < 4)
                {
                    Nodelist.Add(node);
                }
                else
                {
                    foreach(var item in Nodelist)
                    {
                        if (mySelf)//��С��
                        {
                            if(node.value > item.value)
                            {
                                Nodelist.Remove(item);
                                Nodelist.Add(node);
                                break;
                            }
                        }
                        else
                        {
                            if (node.value < item.value)
                            {
                                Nodelist.Remove(item);
                                Nodelist.Add(node);
                                break;
                            }
                        }
                    }
                }

            }
        }
        return Nodelist;
    }
     
    public void CreateTree(MinMaxNode node,int[,]grid ,int deep,bool mySelf)//������
    {
        grid[node.pos[0], node.pos[1]] = node.chess;
     node.child= GetList(grid,node.chess,!mySelf);//���ݵ�ǰ�Լ��Ƿ����巵��
        if (deep == 0||node.value==float.MaxValue)
        {
            return;
        }
        foreach( var item in node.child)
        {
            CreateTree(item, (int[,])grid.Clone(),deep-1,!mySelf);
        }
    }
    public float AlphaBeta(MinMaxNode node,int deep,bool mySelf,float alpha,float beta)
    {
        if (deep == 0 || node.value == float.MaxValue || node.value == float.MaxValue)
        {
            return node.value;
        }
            if (mySelf)
        {
            foreach( var child in node.child)
            {
                alpha = Mathf.Max(alpha, AlphaBeta(child,deep-1,!mySelf,alpha,beta));
                                     
                if (alpha >= beta)//alpha��֦
                {
                    return alpha;
                }
              

            }
            return alpha;
        }
        else
        {
          
            foreach (var child in node.child)
            {
                beta = Mathf.Min(beta, AlphaBeta(child, deep - 1, !mySelf, alpha, beta));
                if (alpha >= beta)//beta��֧
                {
                    return beta;
                }
              

            }
            return beta;
        }
    }

}
