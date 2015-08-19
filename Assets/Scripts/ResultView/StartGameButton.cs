using UnityEngine;
using System.Collections;

public class StartGameButton : MonoBehaviour
{
    public void StartGame ()
    {
        GameManager.Instance.GoToGameStart();
    }
}
