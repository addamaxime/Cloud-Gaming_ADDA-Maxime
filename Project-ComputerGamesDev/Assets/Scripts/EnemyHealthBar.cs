using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Vector3 localScale;

    void Start(){
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = BossScript.instance.currentHealth;
        transform.localScale = localScale;
    }
}
