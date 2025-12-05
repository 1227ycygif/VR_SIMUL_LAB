using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public class BeakerCombine : MonoBehaviour
{
    // 메인 비커 안에 들어가는 액체 오브젝트 참조 
    [SerializeField] Transform liquid; 
    // 액체 오브젝트의 material 저장 
    Material matLiquid; 

    // 가열용 파티클 
    [SerializeField] ParticleSystem[] particles; 

    // 현재 입력 절차 입력란 
    private List<int> process = new List<int>(); 
    // 정답 순서 저장 
    private List<int> answer = new List<int> { 0, 1, 2, 3 }; 
    // 0: 파란색, 1: 분홍색, 2: 가열하기, 3: 노란색 

    // 혼합 중인지 
    bool isCombining = false;

    // 절차 확인 변수 
    int procedureCount = 0; 
    int procedureIdx = 0; 

    // 비커 액체 높이를 Lerp시키기 위한 값 
    Vector3 targetHeight; 

    private void Awake() 
    {
        // material 찾아서 할당
        matLiquid = liquid.gameObject.GetComponent<MeshRenderer>().material;    
    } 

    private void Start()
    {
        // 초기 높이 설정 
        targetHeight = liquid.transform.localScale; 
    } 

    private void OnTriggerEnter(Collider col) 
    {
        Debug.Log("충돌 감지 시작"); 
        if (!col.gameObject.TryGetComponent<Procedure>(out var input)) 
            return; // 컴포넌트 없으면 리턴 

        int proc = input.process; 
        Debug.Log($"진행 과정 감지 : process = {proc}"); 

        // 파티클 재생 처리 
        if (proc == 2)  // 가열 과정 감지 
        {
            foreach (var particle in particles) 
                particle.Play(); 
        }
        if (!isCombining) 
        {
            isCombining = true; 
            col.GetComponent<Procedure>().Pouring(); 
            SetProcess(proc); 
        } 
    } 

    // 파티클 처리 
    private void OnTriggerExit(Collider col) 
    {
        Debug.Log("충돌 감지 종료"); 
        if (!col.gameObject.TryGetComponent<Procedure>(out var input)) 
            return; // 컴포넌트 없으면 리턴 

        int proc = input.process; 
        Debug.Log($"과정 감지 완료 : process = {proc}"); 

        // 파티클 재생 처리 
        if (proc == 2)       // 가열 과정 감지 
            foreach (var particle in particles) 
                particle.Stop(); 
    }

    private void Update() 
    {
        // 비커 액체 높이를 Lerp 
        liquid.localScale = Vector3.Lerp(liquid.localScale, targetHeight, Time.deltaTime * 2f);
    }

    public void SetProcess(int proc)
    {
        Debug.Log($"절차 추가 : {proc}번째 단계");
        process.Add(proc);
        Check();
    }
    // 입력 순서 체크 
    void Check()
    {
        if (process[procedureIdx] == answer[procedureIdx])
        {
            Debug.Log("올바른 순서입니다.");
            CombineProcess();

            // 비커 가열할 때는 오래 기다리게 30초 설정
            if (procedureIdx == 2)
            {
                StartCoroutine(WaitCounter(30));
            }
            else if (procedureIdx == 0 || procedureIdx == 1 || procedureIdx == 3)
            {
                StartCoroutine(WaitCounter(3));
            }
            //StartCoroutine(WaitCounter((procedureIdx == 2) ? 6f : 3f));
            procedureIdx++;
            Debug.Log($"단계 완료, 다음 단계 인덱스 : {procedureIdx}");
        }
        else
        {
            // 게임오버 씬으로 이동
            SceneManager.LoadScene("GameOver");
        }
    }

    // 절차 진행 시 처리될 함수
    void CombineProcess() 
    {
        // 현재 절차에 맞는 액체 색상 처리 
        SetLiquid(); 
        // 액체 색상 변경 후 절차 진행 횟수 증가 
        procedureCount++; 
    } 

    // 비커 높이 조절, material 색상을 처리하는 함수, 단계별로 
    void SetLiquid() 
    {
        // 인덱스가 올라갈수록 절차마다 색상 변경 진행, 인덱스를 기준으로 처리 
        switch (procedureCount) 
        {
            case 0:     // 파란 시약 
                Debug.Log("1단계 처리 : 파란색 추가"); 
                SoundManager.manager.SFXPlay(0,this.transform.position); 
                targetHeight = new Vector3(1f, 0.25f, 1f); 
                matLiquid.color = new Color(0, 0, 255, 255); 
                break; 
            case 1:     // 분홍 시약 
                Debug.Log("2단계 처리 : 분홍색 추가"); 
                SoundManager.manager.SFXPlay(0,this.transform.position); 
                targetHeight = new Vector3(1f, 0.45f, 1f); 
                matLiquid.color = new Color(120, 0, 180, 255); 
                break; 
            case 2:     // 가열 후 변화
                Debug.Log("3단계 처리 : 가열");
                targetHeight = new Vector3(1f, 0.35f, 1f);
                matLiquid.color = new Color(80, 0, 80, 255);
                break;
            case 3:     // 노란 시약, 클리어
                Debug.Log("4단계 처리 : 노란색 추가");
                SoundManager.manager.SFXPlay(0,this.transform.position);
                targetHeight = new Vector3(1f, 0.5f, 1f);
                matLiquid.color = new Color(80, 50, 70, 255);
                break;
        }
    }

    // 대기 시간만큼 기다리는 코루틴 
    IEnumerator WaitCounter(float second) 
    {
        yield return new WaitForSeconds(second); 

        // 대기 시간 후 혼합 완료, 다음 입력 가능 
        isCombining = false; 

        yield break; 
    }
}
