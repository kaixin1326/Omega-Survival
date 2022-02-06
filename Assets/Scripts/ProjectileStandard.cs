using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStandard : MonoBehaviour
{
    //在游戏场景存活时间
    public float maxLifeTime = 5f;
    //速度
    public float speed = 300f;

    public Transform root;
    //子弹尖端的位置
    public Transform tip;
    //用于碰撞检测球体半径
    public float collider_radius = 0.01f;

    public LayerMask hittableLayers = -1;

    private ProjectileBase _projectileBase;

    private Vector3 _velocity;

    //跟踪上次射击起始位置
    private Vector3 _lastRootPostion;

    private void OnEnable()
    {
        _projectileBase = GetComponent<ProjectileBase>();
        _projectileBase.onShoot += OnShoot;
        Destroy(gameObject, maxLifeTime);
    }
    //正在射击的时候设置速度
    private void OnShoot()
    {
        _lastRootPostion = root.position;
        _velocity += transform.forward * speed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //移动
        transform.position += _velocity * Time.deltaTime;
        //方向
        transform.forward = _velocity.normalized;

        //碰撞检测
        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = Mathf.Infinity;

        bool foundHit = false;

        Vector3 displacementSinceLastFrame = tip.position - _lastRootPostion;
        //SphereCastAll 和 sphereCast 的区别 是返回值是个数组 所有和这个射线交互的对象都返回到这个数组里
        RaycastHit[] hits = Physics.SphereCastAll(_lastRootPostion,
            collider_radius,
            displacementSinceLastFrame.normalized,
            displacementSinceLastFrame.magnitude,
            hittableLayers,
            QueryTriggerInteraction.Collide
        );

        foreach (RaycastHit hit in hits)
        {
            //有效撞击
            if (isHitValid(hit) && hit.distance < closestHit.distance)
            {
                closestHit = hit;
                foundHit = true;
            }
        }

        if (foundHit)
        {
             //无效射击处理
             if(closestHit.distance <= 0)
            {
                //无效果
                closestHit.point = root.position;
                closestHit.normal = -transform.forward;
            }

            OnHit();
        }
    }

    private bool isHitValid(RaycastHit hit)
    {
        if (hit.collider.isTrigger)
        {
            return false;
        }
        return true;
    }
    //用来处理碰撞后的音效和特效
    private void OnHit()
    {
        print(message:"Hit");
        Destroy(gameObject);
    }
}