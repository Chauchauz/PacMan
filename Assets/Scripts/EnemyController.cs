using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Instantiate(enemies[i], new Vector3(11.25f + 1.5f * i, 0.25f, -14.0f), Quaternion.Euler(90.0f, 0.0f, 0.0f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
