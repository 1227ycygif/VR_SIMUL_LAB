using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public enum BtnFunc 
{
    START,
    OPTION,
    QUIT
}

public class ButtonGrab : XRBaseInteractable
{
    // 사운드 메뉴를 위한 매니저 
    private SoundManager mixer;

    [Header("버튼의 기능을 선택해주세요")]
    [SerializeField]
    BtnFunc func;

    // 활성화시킬 메뉴창
    [Header("열고 싶은 Option Panel")]
    [SerializeField]
    GameObject pnlTarget;

    // Start is called before the first frame update
    void Start()
    {
        mixer = FindAnyObjectByType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        mixer.Click();

        FuncSelect(func);
    }

    void FuncSelect(BtnFunc func)
    {
        switch (func)
        {
            // Lobby
            case BtnFunc.START:
                GameStart();
                break;
            case BtnFunc.OPTION:
                MenuOption();
                break;
            case BtnFunc.QUIT:
                Quit();
                break;
        }
    }

    void GameStart()
    {
        // Target Panel 닫기 (로비용 패널)
        pnlTarget.SetActive(false);

        // 자기 버튼 그룹도 비활성화
        transform.parent.gameObject.SetActive(false);

        // 어차피 로비에서 게임으로 진입하는 시작버튼말고는 없으니까
        SceneManager.LoadScene("Laboratory Scene");
    }

    void MenuOption()
    {
        if (pnlTarget == null) return;

        // Target Panel 열기
        pnlTarget.SetActive(true);
        // 자신은 비활성화
        transform.parent.gameObject.SetActive(false);
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
