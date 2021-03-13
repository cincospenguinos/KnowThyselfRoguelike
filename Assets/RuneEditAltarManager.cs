using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneEditAltarManager : MonoBehaviour
{
    public RuneEditAltar altar;
    public Sprite usedAltar;

    void Start() {
        altar.OnUsed += HandleUsed;
        altar.OnDeath += () => Destroy(this.gameObject);
        transform.position = new Vector3(altar.Coordinates.x, altar.Coordinates.y, 0);
    }

    void HandleUsed() {
        GetComponent<SpriteRenderer>().sprite = usedAltar;
        // TODO: Where does this come from?
        // GetComponentInChildren<ParticleSystem>().Stop();
    }
}
