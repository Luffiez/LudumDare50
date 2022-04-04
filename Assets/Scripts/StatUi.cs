using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatUi : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Kills;
    [SerializeField]
    TextMeshProUGUI DamageDealt;
    [SerializeField]
    TextMeshProUGUI DamageTaken;
    [SerializeField]
    TextMeshProUGUI DamageHealed;
    [SerializeField]
    TextMeshProUGUI Time;
    [SerializeField]
    TextMeshProUGUI wave;
    [SerializeField]
    TextMeshProUGUI ammoPickedUpp;
    StatTracker StatTracker;

    private void Start()
    {
        StatTracker = FindObjectOfType<StatTracker>();
    }


    public void UpdateStatText()
    {
        Kills.text = "Kills:" + StatTracker.Kills; ;
        DamageDealt.text = "Damage Dealt:" + StatTracker.DamageGiven;
        DamageTaken.text = "Damage Taken:" + StatTracker.DamageTaken;
        DamageHealed.text = "Health Recovered:" + StatTracker.DamageHealed;
        int minutes = (int)StatTracker.TimeSurvived / 60;
        int seconds = (int)StatTracker.TimeSurvived -minutes*60;
        Time.text = "Time:" + minutes + ":" + seconds;
        wave.text = "Wave:" + StatTracker.Wave;
        ammoPickedUpp.text="Ammo Collected:" + StatTracker.AmmoCollected;
    }
}
