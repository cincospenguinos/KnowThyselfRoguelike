using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {
    public Floor floor;

    // Start is called before the first frame update
    void Start() {
        transform.localScale = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update() {
        var targetScale = floor.isVisible ? 1 : 0;
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, 1), 0.2f);
    }
}
