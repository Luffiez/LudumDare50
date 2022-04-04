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


    public void UpdateStatText()
    {
        StatTracker statTracker;
        statTracker = FindObjectOfType<StatTracker>();
        Kills.text = "Kills:" + statTracker.Kills; ;
        DamageDealt.text = "Damage Dealt:" + statTracker.DamageGiven;
        DamageTaken.text = "Damage Taken:" + statTracker.DamageTaken;
        DamageHealed.text = "Health Recovered:" + statTracker.DamageHealed;
        int minutes = (int)statTracker.TimeSurvived / 60;
        int seconds = (int)statTracker.TimeSurvived -minutes*60;
        Time.text = "Time:" + minutes + ":" + seconds;
        wave.text = "Wave:" + statTracker.Wave;
        ammoPickedUpp.text="Ammo Collected:" + statTracker.AmmoCollected;
    }

    private void OnEnable()
    {
        UpdateStatText();
    }
}
