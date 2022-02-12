using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public int AmmoInMag = 30;
    public static int maxAmmoCarried = 210;

    public string GetCurrentAmmo => isReloaded ? CurrentAmmo.ToString() : "Reloading";
    public string GetCurrentAmmoCarried => CurrentAmmoCarried.ToString();

    protected int CurrentAmmo;
    protected static int CurrentAmmoCarried;

    private bool isReloaded = true;
    //显示武器
    public void ShowWeapon(bool show)
    {
        WeaponRoot.SetActive(show);
        isWeaponActive = show;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmo = AmmoInMag;
        CurrentAmmoCarried = maxAmmoCarried;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //检测是否按下射击按钮
    public bool HandleShootInputs(bool inputHeld)
    {
        if (inputHeld && isReloaded)
        {
            return TryShoot();
        }
        return false;
    }

    public bool HandleReloadInputs(bool inputHeld)
    {
        if (inputHeld)
        {
            return TryReload();
        }
        return false;
    }
    //检测能否射击
    private bool TryShoot()
    {
        if (CurrentAmmo <= 0)
        {
            TryReload();
            return false;
        }
        if(lastShotTime + delayBetweenShots < Time.time)
        {
            CurrentAmmo -= 1;
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
            //ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponCamera.transform.position, weaponCamera.transform.rotation, weaponCamera.transform);
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

    private bool TryReload()
    {
        if (CurrentAmmo == AmmoInMag || CurrentAmmoCarried == 0) return false;
        var AmmoCount = AmmoInMag - CurrentAmmo;
        
        isReloaded = false;
        Invoke("Reload", 2);
        //Reload();
        return true;
    }

    private void Reload()
    {
        var AmmoCount = AmmoInMag - CurrentAmmo;
        if (AmmoCount > CurrentAmmoCarried)
        {
            CurrentAmmo = CurrentAmmo + CurrentAmmoCarried;
            CurrentAmmoCarried = 0;
        }
        else
        {
            CurrentAmmo = AmmoInMag;
            CurrentAmmoCarried -= AmmoCount;
        }
        isReloaded = true;
    }

    public static void AddAmmo(int count)
    {
        CurrentAmmoCarried += count;

        if(CurrentAmmoCarried >= maxAmmoCarried)
        {
            CurrentAmmoCarried = maxAmmoCarried;
        }
    }
}
