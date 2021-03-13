using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {
  public TMPro.TMP_Text text;
  // Start is called before the first frame update
  void Start() {
    StartCoroutine(DoIntro());
  }

  IEnumerator DoIntro() {
    yield return AnimUtils.Animate(1f, t => {
      t = t * t * (3.0f - 2.0f * t);
      var s = 100 - 99 * t;
      text.rectTransform.localScale = new Vector3(s, s, 1);
    });
    yield return new WaitForSeconds(2);
    yield return AnimUtils.Animate(2, t => {
      var s = 1 - t;
      text.rectTransform.localScale = new Vector3(s, s, 1);
      this.GetComponent<Image>().color = new Color(0, 0, 0, s);
    });
    Destroy(this.gameObject);
  }
}
