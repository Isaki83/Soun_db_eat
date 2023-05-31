using UnityEngine;
using DG.Tweening;

public partial class TutorialUIController
{
    /*==============================================================

        チュートリアル０４のUI操作
     
    ==============================================================*/
    void Titorial04()
    {
        Debug.Log("Tutorial04 UI Controll");

        Sequence sequence = DOTween.Sequence();

        // 始まったときのピッチを保存しておく
        float prevAudioPitch = _PlayerMove.AudioPitch;

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "Panel01"を大きくして
        // 同時に曲を止めてプレイヤーが進まないようにする
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step01
        // "Panel01"を大きくする
        sequence.Append(
            GO_Panel01_04.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnStart(
                () =>
                {
                    // "Panel01"をアクティブにする
                    GO_Panel01_04.SetActive(true);
                    // 曲を止める
                    _PlayerMove.AudioPause = true;
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
            GO_Panel01_04.transform.DOScale(Vector3.one / 2.0f, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetDelay(DeleteTime)
            .SetLink(gameObject));
        // "Panel01"を移動する
        sequence.Join(
            GO_Panel01_04.transform.DOLocalMove(new Vector3(-199.0f, 40.0f, 0.0f), 0.5f)
            .SetRelative()
            .SetEase(Ease.OutQuad)
            .SetLink(gameObject));
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "Panel02"を大きくする
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step03
        // "Panel02"を大きくする
        sequence.Append(
            RT_Panel02_04.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnStart(
                () =>
                {
                    // "Panel02"をアクティブにする
                    GO_Panel02_04.SetActive(true);
                })
            .SetLink(gameObject)
            );
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // "DeleteTime"秒後に
        // "チュートリアル０４"を透明にする
        // 同時に曲を逆再生してプレイヤーを少し後ろに戻す
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        #region Step04
        // チュートリアル０４を透明にする
        sequence.Append(
            CG_Tutorial_04.DOFade(0.0f, 0.5f)
            .SetEase(Ease.OutQuad)
            .SetDelay(DeleteTime)
            .OnStart(
                () =>
                {
                    // 曲を逆再生
                    _PlayerMove.AudioPitch = -3.0f;
                    // 曲再開
                    _PlayerMove.AudioPause = false;
                })
            .OnComplete(
                () =>
                {
                    // "Panel01"を非アクティブにする
                    GO_Panel01_04.SetActive(false);
                    // "Panel02"を非アクティブにする
                    GO_Panel02_04.SetActive(false);
                    // 曲のピッチを元に戻す
                    _PlayerMove.AudioPitch = prevAudioPitch;
                    sequence.Kill();
                })
            .SetLink(gameObject)
            );
        #endregion
    }
}
