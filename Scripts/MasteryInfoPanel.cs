using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasteryInfoPanel : MonoBehaviour
{
    public Text name1;
    public Text description;
    public void SetStart(string myname, string mydescription)
    {
        name1.text = myname;
        description.text = mydescription;
    }
    public void Update()
    {
        transform.position = Input.mousePosition + new Vector3(130, -77, 0);
    }
}
