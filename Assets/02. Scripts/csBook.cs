using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBook : MonoBehaviour
{
    [SerializeField]
    private GameObject info;
    private SpriteRenderer spInfo;

    // 정보 전환용 스프라이트
    public Sprite spInfo1;
    public Sprite spInfo2;

    private void Awake()
    {
        // 정보창 찾기
        if(!info) info = GameObject.Find("Info");
        // 정보창 이미지 컴포넌트 연결
        spInfo = info.GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        // 시작하면 일단 비활성화
        info.SetActive(false);
        // 브금 재생, 따로 적을 곳을 찾기 애매해서 이곳에 작성
        SoundManager.manager.BGMPlay(0, this.transform.position);
    }

    public void Next()
    {
        // 다음 정보 스프라이트 출력
        spInfo.sprite = spInfo2;
    }

    public void Close()
    {
        // 정보창 내용 초기화
        spInfo.sprite = spInfo1;
        // 정보창 비활성화
        info.SetActive(false);
    }
}
