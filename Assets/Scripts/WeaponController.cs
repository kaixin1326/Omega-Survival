using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject WeaponRoot;
    //是否有武器
    public bool isWeaponActive { get; private set; }
    //拥有枪的人
    public GameObject Owner { get; set; }
    //武器实际3d模型
    public GameObject SourcePrefab { get; set; }
    //枪口火焰的位置
    public Transform weaponMuzzle;
    //枪口火焰prefab
    public GameObject muzzleFlashPrefab;
    //
    public Vector3 muzzleWorldVelocity{ get; private set; } 
    //两次射击间隔
    public float delayBetweenShots = 0.1f;
    //上一次射击时间
    private float lastShotTime = Mathf.NegativeInfinity;
    //
    public ProjectileBase projectilePrefab;
    //显示武器
    public void ShowWeapon(bool show)
    {
        WeaponRoot.SetActive(show);
        isWeaponActive = show;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //检测是否按下射击按钮
    public bool HandleShootInputs(bool inputHeld)
    {
        if (inputHeld)
        {
            return TryShoot();
        }
        return false;
    }

    //检测能否射击
    private bool TryShoot()
    {
        if(lastShotTime + delayBetweenShots < Time.time)
        {
            HandleShoot();
            return true;
        }
        return false;
    }

    //射击
    private void HandleShoot()
    {
        //射出弹道
        if(projectilePrefab != null)
        {
            //方向位置和火焰方向一致
            Vector3 shotDirection = weaponMuzzle.forward;
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, weaponMuzzle.rotation, weaponMuzzle.transform);
            newProjectile.Shoot(controller:this);
        }

        //射击火焰特效
        if(muzzleFlashPrefab != null)
        {
            //创建实例
            GameObject muzzleFlashInstance = Instantiate(muzzleFlashPrefab,weaponMuzzle.position,weaponMuzzle.rotation,weaponMuzzle.transform);
            //消除实例
            Destroy(muzzleFlashInstance, 2);
        }
        lastShotTime = Time.time;
    }
}
