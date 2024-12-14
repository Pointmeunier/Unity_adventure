using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("檢測參數")]
    //手動設置碰撞判斷點
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRaduis;
    public LayerMask groundLayer;
    [Header("狀態")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;

    public void Awake()
    {
        coll = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            leftOffset = new Vector2((-coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x)/2,coll.bounds.size.y / 2);
          
        }
        
    }
    public void Update()
    {
        Check();
    }

    public void Check()
    {
        //檢測是否在地面
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRaduis, groundLayer);

        //檢測左右邊的牆壁
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRaduis, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRaduis, groundLayer);


    }

    //調整判斷偏移
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRaduis);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRaduis);
    }
}


