using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Enemy Enemy;

    // Start is called before the first frame update
    void Start() {
        Enemy.OnDeath += () => {
            Destroy(this.gameObject);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
