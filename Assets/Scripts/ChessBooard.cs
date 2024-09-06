using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBooard : MonoBehaviour
{
    // Start is called before the first frame update
    static ChessBooard instance;
    public ChessType turn = ChessType.Black;//��������
    public int[,] grid;
    public GameObject[] prefabs;//�洢�ڰ���
    public float timer = 0;//�ȴ�ʱ��
    public bool gameStart = false;//�жϵ�ǰ�Ƿ��������
    Transform parent;
    public Stack<Transform> chessStack = new Stack<Transform>();
    public Text Win;
    public static ChessBooard Instance
    {
        get
        { return instance;

        } 
    }
    private void Awake()
    {if(Instance == null)
        {
            instance = this;
        }
       
    }
    private void Start()
    {
        parent = GameObject.Find("Parent").transform;
        grid = new int[15, 15];//��ʼ������
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public bool PlayerChess(int[] pos)
    {
        if (!gameStart) return false;//ʤ���Ͳ�������
        pos[0] = Mathf.Clamp(pos[0],0,14);
        pos[1] = Mathf.Clamp(pos[1], 0, 14);
        if (grid[pos[0], pos[1]] != 0) return false;//�жϵ�ǰ���Ƿ��¹���
        if(turn==ChessType.Black)
        {
          GameObject gO=  Instantiate(prefabs[0], new Vector3(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(gO.transform);
            gO.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 1;//���ӵ�ö��Ϊ1
            if (CheckWin(pos))//�ж�ʤ��
            {
                GameEnd();
            }
            turn = ChessType.White;
        }
        else if(turn==ChessType.White) 
        {
            GameObject gO = Instantiate(prefabs[1], new Vector3(pos[0] - 7, pos[1] - 7), Quaternion.identity);
            chessStack.Push(gO.transform);
            gO.transform.SetParent(parent);
            grid[pos[0], pos[1]] = 2;//���ӵ�ö��Ϊ2
            if (CheckWin(pos))
            {
                GameEnd();
            }
            turn = ChessType.Black;
        }
        return true;
    }
    void GameEnd()
    {
        Win.transform.parent.parent.gameObject.SetActive(true);
        switch (turn)
        {
            case ChessType.Black:
                Win.text = "����ʤ";
                break; 
            case ChessType.White:
                Win.text = "����ʤ";
                break;
            case ChessType.Watch: 
                break;

        }
      
        gameStart = false;
        Debug.Log(turn+"ʤ��");
    }
    public bool CheckWin(int[] pos)
    {
        if (CheckOneLine(pos, new int[2] { 1, 0 })) return true;//����
        if (CheckOneLine(pos, new int[2] { 0, 1 })) return true;//����
        if (CheckOneLine(pos, new int[2] { 1, 1 })) return true;//��б
        if (CheckOneLine(pos, new int[2] { 1, -1 })) return true;//��б

        return false;
    }
    private void FixedUpdate()
    {
        timer += Time.deltaTime;
    }
    public bool CheckOneLine(int[] pos,int[] offset)
    {
        int linkNum = 1;
        //�����ұ߼���Ƿ���Լ�һ��������
        for(int i = offset[0],j=offset[1];(pos[0]+i >=0&&pos[0]+i<15)&& (pos[1] + j >= 0 && pos[1] + j < 15); i += offset[0], j += offset[1])
        {
            if (grid[pos[0]+i, pos[1] + j] == (int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        } //��߼���Ƿ���Լ�һ��������
        for (int i = -offset[0], j = -offset[1]; (pos[0] + i >= 0 && pos[0] + i < 15) && (pos[1] + j >= 0 && pos[1] + j < 15); i -= offset[0], j -= offset[1])
        {
            if (grid[pos[0] + i, pos[1] + j] == (int)turn)
            {
                linkNum++;
            }
            else
            {
                break;
            }
        }

        if (linkNum > 4)
        {
            return true;
        }
        return false;
    }

    public void RetractChess()//���巽��
    {
        if(chessStack.Count>1)
        {
            Transform pos = chessStack.Pop();//��ջ
            grid[(int)(pos.position.x + 7), (int)(pos.position.y + 7)] = 0;
            Destroy(pos.gameObject);
            pos = chessStack.Pop();//��ջ
            grid[(int)(pos.position.x + 7), (int)(pos.position.y + 7)] = 0;
            Destroy(pos.gameObject);
        }
    }

}
public enum ChessType
{
    Watch,
    Black,
    White,
}