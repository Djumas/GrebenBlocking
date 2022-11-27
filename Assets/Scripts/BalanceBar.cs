using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceBar : MonoBehaviour
{
    public Slider balanceBar;
    public HealthManager healthManager;
    public float stunTresh;
    public Image FillInstance;
    public Color normalColor;
    public Color stunColor;
    // Start is called before the first frame update
    void Start()
    {
        stunTresh = healthManager.stunTresh;
    }

    // Update is called once per frame
    void Update()
    {
        balanceBar.value = healthManager.currentBalance / healthManager.baseBalance;
        if (healthManager.currentBalance <= stunTresh)
        {
            FillInstance.color = stunColor;
        }
        else FillInstance.color = normalColor;
    }
}
