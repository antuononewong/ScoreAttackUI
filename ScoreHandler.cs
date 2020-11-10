using System;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // Dependancies
    public TMP_Text finalScore;
    public TMP_Text scoreMultiplier;

    // Components
    private TMP_Text _scoreText;

    // Point system
    private float _score;
    private float _passengerPoints = 100000f;
    private float _pedestrianPoints = 5000f;

    private float _multiplier = 1f;
    private float _maxMultiplierTimer = 1f;
    private float _multiplierTimer;

    void Awake()
    {
        _scoreText = gameObject.GetComponent<TMP_Text>();

        DeliveryPointController.OnPassengerDropOff += OnPassengerDroppedOff;
        PedestrianController.OnPedestrianDeath += OnPedestrianDying;
    }

    private void Update()
    {
        _multiplierTimer -= Time.deltaTime;

        if (_multiplierTimer <= 0)
        {
            _multiplier = 1f;
            scoreMultiplier.text = "1x";
        }
    }

    private void AddScore(float score)
    {
        _multiplierTimer = _maxMultiplierTimer;
        _score += score * _multiplier;
        _multiplier += 1f;

        string scoreText = "Score: " + _score.ToString("#");
        _scoreText.text = scoreText;
        finalScore.text = scoreText;
        scoreMultiplier.text = _multiplier.ToString("#") + "x";
    }

    private void OnPassengerDroppedOff(bool havePassenger)
    {
        AddScore(_passengerPoints);
    }

    private void OnPedestrianDying()
    {
        AddScore(_pedestrianPoints);
    }
}
