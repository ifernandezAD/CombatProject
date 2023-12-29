using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audition : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float timeToForget = 1f;

    [Header("Debug")]
    [SerializeField] List<HeardAudible> debugHeardAudibles;

    [System.Serializable]
    class HeardAudible
    {
        public AudibleBase audible;
        public Senseable senseable;
        public float hearingTime;
    }

    List<HeardAudible> heardAudibles = new List<HeardAudible>();

    private void Awake()
    {
        debugHeardAudibles = heardAudibles;
    }

    private void Update()
    {
        heardAudibles.RemoveAll(x => (Time.time - x.hearingTime) > timeToForget);
    }

    public void NotifyAudibleInRange(AudibleBase audible, Senseable senseable)
    {
        HeardAudible heardAudible = heardAudibles.Find(x => x.audible == audible);
        if (heardAudible == null)
        {
            heardAudible = new HeardAudible();
            heardAudibles.Add(heardAudible);
        }
        heardAudible.audible = audible;
        heardAudible.senseable = senseable;
        heardAudible.hearingTime = Time.time;
    }
}
