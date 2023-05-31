using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 最初のサイズ
    private Vector3 BaseScale = Vector3.one;
    
    public enum Access
    {
        Enter,
        Exit
    }

    // Start is called before the first frame update
    void Start()
    {
        // 最初のサイズ保存
        BaseScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // アニメーション再生
        PlayAnim(Access.Enter);
    }


    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // アニメーション再生
        PlayAnim(Access.Exit);
    }

    public void PlayAnim(Access access)
    {
        switch (access)
        {
            case Access.Enter:
                transform.DOScale(BaseScale * 1.5f, 0.25f)
                    .SetEase(Ease.OutBounce);
                break;
            case Access.Exit:
                transform.DOScale(BaseScale, 0.25f)
                    .SetEase(Ease.OutQuad);
                break;
        }
    }
}
