using UnityEngine;

public class BALABABA : MonoBehaviour
{
    #region 欄位區域
    [Header("移動速度")]
    [Range(1, 2000)]
    public int speed = 10;
    [Header("旋轉速度"), Tooltip("BALABABA的旋轉速度"), Range(1.5f, 200f)]
    public float turn = 20.5f;
    [Header("是否完成任務")]
    public bool mission;
    [Header("玩家名稱")]
    public string _name = "BALABABA";
    #endregion

    public Transform tran;
    public Rigidbody rig;
    public Animator ani;

    [Header("撿東西")]
    public Rigidbody rigCatch;

    private void Update()
    {
        Turn();
        Run();
        Catch();
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.name);

        if (other.name == "道具" && ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊觸發"))
        {
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }
    }

    #region 方法區域
    /// <summary>
    /// 跑步
    /// </summary>
    private void Run()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("攻擊觸發")) return;

        float v = Input.GetAxis("Vertical");
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);

        ani.SetBool("走路開關", v != 0);
    }

    /// <summary>
    /// 旋轉
    /// </summary>
    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
    }

    /// <summary>
    /// 撿東西
    /// </summary>
    private void Catch()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ani.SetTrigger("攻擊觸發");
        }
    }
    #endregion
}
