using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNotesType : MonoBehaviour
{
    public TypeList NotesType = TypeList.Middle;    // ロングノーツの始点、中点、終点
    public int NotesNum;                            // 判定の数


    // ノーツのタイプ
    public enum TypeList
    {
        Start, Middle, End
    }


    /*==============================================================

        ノーツの数を返す
     
    ==============================================================*/
    public int GetNotesNum()
    {
        return NotesNum;
    }
}
