using UnityEngine;
using DG.Tweening;


public partial class TutorialUIController
{
    /*==============================================================

        チュートリアル０１のUI操作
     
    ==============================================================*/
    void Titorial01()
    {
        Debug.Log("Tutorial01 UI Controll");

        Sequence sequence = DOTween.Sequence();

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "Panel01"を大きくする
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step01
        // "Panel01"を大きくする
        sequence.Append(
            GO_Panel01_01.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnStart(
                () =>
                {
                    // "Panel01"をアクティブにする
                    GO_Panel01_01.SetActive(true);
                })
            .SetLink(gameObject)
            );
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "DeleteTime"秒後に
        // "Panel01"を小さくしながら移動する
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step02
        // "Panel01"を小さくする
        sequence.Append(
            GO_Panel01_01.transform.DOScale(Vector3.one / 2.0f, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetDelay(DeleteTime)
            .SetLink(gameObject));
        // "Panel01"を移動する
        sequence.Join(
            GO_Panel01_01.transform.DOLocalMove(new Vector3(-117.0f, 40.0f, 0.0f), 0.5f)
            .SetRelative()
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject));
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "Panel02"を大きくする
        // 同時に"Arrow"を点滅させる
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step03
        // "Panel02"を大きくする
        sequence.Append(
            RT_Panel02_01.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnStart(
                () => 
                {
                    // "Panel02"をアクティブにする
                    GO_Panel02_01.SetActive(true);

                    // "Arrow"を点滅させる
                    CG_Arrow_01.DOFade(1, 0.5f)
                    .SetEase(Ease.OutQuad)
                    .SetLoops(-1, LoopType.Yoyo)
                    .OnStart(
                        () =>
                        {
                            // "Arrow"をアクティブにする
                            GO_Arrow_01.SetActive(true);
                        }
                    );
                })
            .SetLink(gameObject)
            );
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "DeleteTime"秒後に
        // "チュートリアル０１"を透明にする
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step04
        // チュートリアル０１を透明にする
        sequence.Append(
            CG_Tutorial_01.DOFade(0.0f, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetDelay(DeleteTime)
            .OnComplete(
                () =>
                {
                    // "Arrow"を非アクティブにする
                    GO_Arrow_01.SetActive(false);
                    // "Panel01"を非アクティブにする
                    GO_Panel01_01.SetActive(false);
                    // "Panel02"を非アクティブにする
                    GO_Panel02_01.SetActive(false);
                    sequence.Kill();
                })
            .SetLink(gameObject)
            );
        #endregion
    }
}
