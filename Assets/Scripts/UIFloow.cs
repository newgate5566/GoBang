using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFloow : MonoBehaviour
{
    private void Update()
    {
        if(ChessBooard.Instance.chessStack.Count > 0)
        {
            transform.position=ChessBooard.Instance.chessStack.Peek().position;//ȡ��ջ��Ԫ�� ���λ��
        }
    }
    public void OnRelayBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnBtn()
    {
        SceneManager.LoadScene(0);
    }
}
