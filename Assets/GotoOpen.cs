using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GotoOpen : XRBaseInteractable
{
    Collider col;
    protected override void OnEnable()
    {
        base.OnEnable();

        // �� ������Ʈ�� Collider�� XR ��ȣ�ۿ� Colliders ����Ʈ�� ���� ���
        
        if (col != null)
        {
            col = GetComponent<Collider>();
            // XRBaseInteractable�� �����ϴ� colliders ����Ʈ�� ���
            if (colliders == null)
                Debug.LogError("�ݶ��̴� �Ҵ� �ȵǰ� ����.");

            if (!colliders.Contains(col))
                colliders.Add(col);
        }
    }

    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // ���¾����� �̵�
        SceneManager.LoadScene(0);
    }
}
