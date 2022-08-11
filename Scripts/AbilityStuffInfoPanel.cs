using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AbilityStuffInfoPanel : MonoBehaviour
{
    public Text thename;
    public Text details;
    public GameObject icon;
    public Text cooldown;
    public void SetUp(string myname, string mydetails, string mycooldown, GameObject myicon)
    {
        thename.text = myname;
        details.text = mydetails;
       
        cooldown.text = mycooldown;

        foreach (Transform child in icon.transform)
        {
            Destroy(child.gameObject);
        }

        GameObject instance = Instantiate(myicon);
        instance.transform.SetParent(icon.transform);
    }
    public void Update()
    {
        transform.position = Input.mousePosition + new Vector3(140, -77, 0);
    }
}
