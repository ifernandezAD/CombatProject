using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Audition : Sense
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
        heardAudibles.Sort(
            (x, y) =>
        (Vector3.Distance(transform.position, x.senseable.transform.position) <
        Vector3.Distance(transform.position, y.senseable.transform.position)) ? 1 : 0
        );
    }

    public void NotifyAudibleInRange(AudibleBase audible, Senseable senseable)
    {
        if (senseable != GetMySenseable() &&
             (AllegianceManager.GetAllegianceRelationship(senseable.allegiance, GetMySenseable().allegiance) ==
                AllegianceDefinition.Relationship.Enemies)
            )
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

    public override Senseable GetSenseable()
    {
        return heardAudibles.Count == 0 ? null : heardAudibles[0].senseable;
    }
}
