using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtAnimationEnd : MonoBehaviour {
  public GameObject who;
  public void AnimationEnded() {
    Destroy(who);
  }
}
