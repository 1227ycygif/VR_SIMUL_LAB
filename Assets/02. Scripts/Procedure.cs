using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedure : MonoBehaviour
{
    [Header("액체 오브젝트 (필요 시 연결)")]
    [SerializeField]
    private Transform liquid;
    // 액체 오브젝트의 material 저장
    Material matLiquid;

    // 진행 과정 번호를 저장 (외부에서 설정)
    public int process;

    // 액체 높이를 Lerp시키기 위한 값
    Vector3 targetHeight;

    private void Awake()
    {
        // 머티리얼 찾아서 할당
        matLiquid = liquid.gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        // 초기 높이 설정
        targetHeight = liquid.transform.localScale;
    }

    private void Update()
    {
        //if (liquid.localScale.y - targetHeight.y > 0.005f)
        //    // 비커 액체의 높이를 타겟 높이만큼 Lerp
        //    liquid.localScale = Vector3.Lerp(liquid.localScale, targetHeight, Time.deltaTime * 2f);
        //else
        //{
        //    liquid.localScale = new Vector3(1f, 0.01f, 1f);
        //    matLiquid.color = new Color(0, 0, 0, 0);
        //}
        liquid.localScale = Vector3.Lerp(liquid.localScale, targetHeight, Time.deltaTime * 2f);
    }

    // 비커에 액체를 부을 때 호출, 외부 호출용
    public void Pouring()
    {
        Debug.Log("시약 액체 붓기 호출");
        targetHeight = new Vector3(1f, 0.01f, 1f);
    }
}
