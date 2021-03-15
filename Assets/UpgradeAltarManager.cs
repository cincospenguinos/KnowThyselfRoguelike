using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAltarManager : MonoBehaviour {
    public UpgradeAltar altar;
    public Sprite usedAltar;

    void Start() {
        altar.OnUsed += HandleUsed;
        altar.OnDeath += () => {
            Destroy(this.gameObject);
        };
        transform.position = new Vector3(altar.Coordinates.x, altar.Coordinates.y, 0);
    }

    void HandleUsed() {
        GetComponent<SpriteRenderer>().sprite = usedAltar;
        GetComponent<Animator>().enabled = false;
        GetComponentInChildren<ParticleSystem>()?.Stop();
        AnimUtils.ShowFloatingText("Upgraded first Rune!", transform.position);
    }
}
