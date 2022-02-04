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
    // Start is called before the first frame update

    public void ShowWeapon(bool show)
    {
        WeaponRoot.SetActive(show);
        isWeaponActive = show;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
