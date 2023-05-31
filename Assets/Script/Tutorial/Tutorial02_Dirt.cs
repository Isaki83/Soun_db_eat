using UnityEngine;
using DG.Tweening;

public partial class TutorialUIController
{
    /*==============================================================

        チュートリアル０２のUI操作
     
    ==============================================================*/
    void Titorial02()
    {
        Debug.Log("Tutorial02 UI Controll");

        Sequence sequence = DOTween.Sequence();
        // 生成した"Dirt"を後で削除する用
        GameObject[] _Dirt = new GameObject[2];

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "Panel01"を大きくする
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step01
        // "Panel01"を大きくする
        sequence.Append(
            GO_Panel01_02.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnStart(
                () =>
                {
                    // "Panel01"をアクティブにする
                    GO_Panel01_02.SetActive(true);
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
            GO_Panel01_02.transform.DOScale(Vector3.one / 2.0f, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetDelay(DeleteTime)
            .SetLink(gameObject));
        // "Panel01"を移動する
        sequence.Join(
            GO_Panel01_02.transform.DOLocalMove(new Vector3(-206.0f, 40.0f, 0.0f), 0.5f)
            .SetRelative()
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject));
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "Panel02"を大きくする
        // 同時にチュートリアル用の"Dirt"生成
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step03
        // "Panel02"を大きくする
        sequence.Append(
            RT_Panel02_02.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnStart(
                () =>
                {
                    // "Panel02"をアクティブにする
                    GO_Panel02_02.SetActive(true);
                    // チュートリアル用の"Dirt"生成
                    _Dirt[0] = Instantiate(GO_Dirt_02, new Vector3(-3.2f, 0.1f, -10.0f), Quaternion.identity);
                    _Dirt[1] = Instantiate(GO_Dirt_02, new Vector3(-3.2f, 0.1f, 1.5f), Quaternion.identity);
                })
            .SetLink(gameObject)
            );
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "DeleteTime"秒後に
        // "チュートリアル０２"を透明にする
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step04
        // チュートリアル０２を透明にする
        sequence.Append(
            CG_Tutorial_02.DOFade(0.0f, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetDelay(DeleteTime)
            .OnComplete(
                () =>
                {
                    // チュートリアル用の"Dirt"削除
                    for (int i = 0; i < _Dirt.Length; i++) { Destroy(_Dirt[i]); }
                    // "Panel01"を非アクティブにする
                    GO_Panel01_02.SetActive(false);
                    // "Panel02"を非アクティブにする
                    GO_Panel02_02.SetActive(false);
                    sequence.Kill();
                })
            .SetLink(gameObject)
            );
        #endregion
    }
}
