using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public int distanceToItem;
    public Text pickupNotice;

    //inventory
    [SerializeField] private UI_inventory uiInventory;
    private static Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (pickupNotice.enabled == true)
        {
            pickupNotice.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collect();
    }

    void Collect()
    {
        //from middle of screen
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanceToItem))
        {
            if (hit.collider.tag == "CollectableObj")
            {
                pickupNotice.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //put in inventory
                    Debug.Log(hit.transform.gameObject.GetComponent<itemDetail>().type);
                    inventory.AddItem(hit.transform.gameObject.GetComponent<itemDetail>().item);
                    Destroy(hit.transform.gameObject);
                    //WeaponController.AddAmmo(30);
                }
            }
            else
            {
                pickupNotice.enabled = false;
            }
        }

    }
}
