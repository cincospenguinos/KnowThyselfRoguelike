using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {
  public new Camera camera;
  public float moveScale = 0.2f;
  public MeshRenderer meshRenderer;

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {
    var offset = new Vector2(Time.time / 10 % 1, 0);
    if (meshRenderer != null) {
      meshRenderer.material.SetTextureOffset("_MainTex", offset);
    }
    transform.position = new Vector3(
      camera.transform.position.x * moveScale, 
      camera.transform.position.y * moveScale, 
      transform.position.z
    );
  }
}
