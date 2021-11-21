using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game 
{
    [XLua.Hotfix]
    public class ChangeTextColor : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Text>().color = Color.red;
        }
    }
}


