using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class csMenuAction : XRBaseInteractable
{
    public GameObject parent;
    private IMenuTarget menuTarget;

    public enum Action
    {
        다음,
        나가기
    }

    [Space(10)]
    [Header("대응 동작")]
    [SerializeField]
    private Action actionType;
    private string actionName;

    protected override void OnEnable()
    {
        base.OnEnable();

        // 이 오브젝트의 Collider를 XR 상호작용 Colliders 리스트에 보장 등록
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            // XRBaseInteractable이 제공하는 colliders 리스트를 사용
            if (colliders == null)
                Debug.LogError("콜라이더 할당 안되고 있음.");

            if (!colliders.Contains(col))
                colliders.Add(col);
        }
    }

    private void Awake()
    {
        menuTarget = parent.GetComponent<IMenuTarget>();
    }

    private void Start()
    {
        switch (actionType)
        {
            case Action.다음:
                actionName = "Next";
                break;
            case Action.나가기:
                actionName = "Exit";
                break;
        }

        if (menuTarget == null)
        {
            Debug.LogError("부모 오브젝트 못찾았어.");
            return;
        }
        else
        {
            Debug.Log($"이 오브젝트의 부모: {menuTarget.TargetName} / 수행 동작: {actionName}");
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (menuTarget != null)
        {
            Debug.Log("자식 오버라이드 함수 진입");
            menuTarget.OnMenuAction(actionName);
        }
    }
}
