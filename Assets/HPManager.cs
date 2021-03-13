using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour {
    public Image fullImage;
    public TMPro.TMP_Text text;
    void Start() {
        Update();
    }

    void Update() {
        var player = Grid.instance.Player;
        text.text = $"{player.HitPoints}/{player.MaxHitPoints}";
        var fillAmount = (float)(player.HitPoints) / player.MaxHitPoints;
        fullImage.fillAmount = fillAmount;
    }
}
