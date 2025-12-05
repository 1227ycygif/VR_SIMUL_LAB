using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSelectBook : MonoBehaviour, IMenuTarget
{
    public string TargetName => "Book";

    public void OnMenuAction(string actionName)
    {
        Debug.Log("인터페이스 메서드 실행: " + actionName);

        switch (actionName)
        {
            case "Next":
                this.gameObject.GetComponent<csBook>().Next();
                Debug.Log("다음");
                break;
            case "Exit":
                this.gameObject.GetComponent<csBook>().Close();
                Debug.Log("종료");
                break;
        }
    }
}
