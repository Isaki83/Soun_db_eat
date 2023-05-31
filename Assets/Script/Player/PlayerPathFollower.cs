/*
 * Asset "Bézier Path Creator"
 * Base "PathFollower.cs"
 * https://assetstore.unity.com/packages/tools/utilities/b-zier-path-creator-136082
*/
using UnityEngine;
using PathCreation;

public class PlayerPathFollower : MonoBehaviour
{
    // 作成したスプライン
    public PathCreator pathCreator;
    // 停止やループ等のモード
    public EndOfPathInstruction endOfPathInstruction;
    // スプラインの位置
    float distanceTravelled;

    // `PlayerMove.cs`スクリプト呼び出し
    PlayerMove playerMove;


    /*==============================================================

        開始

    ==============================================================*/
    void Start()
    {
        // コンポーネント取得
        playerMove = GetComponent<PlayerMove>();

        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }


    /*==============================================================

        更新
     
    ==============================================================*/
    void Update()
    {
        // スプラインが存在しなかったら終わり
        if (pathCreator == null) { return; }

        // ゴールした後はスプラインから剥がす
        if (playerMove._State == PlayerMove.State.End) { return; }
        
        // スプラインの位置指定
        distanceTravelled = playerMove.distanceTravelled;
        // 位置をスプラインの張り付いた状態で横移動できるようにする
        Vector3 position = new Vector3( pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).x + playerMove.SideMove.x,
                                        transform.position.y,
                                        pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction).z + playerMove.SideMove.z);
        // 位置・角度反映
        transform.position = position;
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}
