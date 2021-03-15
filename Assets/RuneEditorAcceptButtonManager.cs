using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneEditorAcceptButtonManager : MonoBehaviour
{
    public GameObject OverlayAndButton;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    public void DoneEditingRunes() {
        Debug.Log("hey this should be run now");
        Grid.instance.Player.EditingRunes = false;
        PlayerManager.inputEnabled = true;
    }
}
