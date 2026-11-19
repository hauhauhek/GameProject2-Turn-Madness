using System.Collections;
using UnityEngine;

public enum HitTarget
{
    Player1,
    Player2,
    Both
}

[System.Serializable]
public class Hit
{
    public float delayBefore = 0.5f;          // czas oczekiwania przed uderzeniem (s)
    public float damage = 10f;                // obra¿enia tego uderzenia
    public HitTarget target = HitTarget.Both; // kogo uderza
}

public class EnemyAttack : MonoBehaviour
{
    [Header("Obrona")]
    [SerializeField] KeyCode klawisz1 = KeyCode.N;
    [SerializeField] KeyCode klawisz2 = KeyCode.M;
    [SerializeField] float cooldownKlawisza = 1f;
    [SerializeField] AudioSource damage_taken;
    [SerializeField] AudioSource parried;
    [SerializeField] AudioSource indicator1;
    [SerializeField] AudioSource dindicator2;

    [Header("Gracze")]
    [SerializeField] obrona skryptGracza; // klasa ze skryptu, który zawiera ZadajObrazenia
    [SerializeField] Transform gracz1;    // pasek zdrowia gracza 1
    [SerializeField] Transform gracz2;    // pasek zdrowia gracza 2
    [SerializeField] GameObject squareczerw;    // pasek zdrowia gracza 2
    [Header("Atak")]
    [SerializeField] Hit[] hits;
    [Header("Animacje")]
    [SerializeField] Animator redattack1Animator;
    [SerializeField] Animator blueattack1Animator;
    public bool IsAttacking { get; private set; }

    bool nacisniete1, nacisniete2;
    float ostatni1 = -999f, ostatni2 = -999f;

    void Update()
    {
        if (Input.GetKeyDown(klawisz1) && Time.time >= ostatni1 + cooldownKlawisza)
        {
            nacisniete1 = true;
            ostatni1 = Time.time;
        }
        if (Input.GetKeyDown(klawisz2) && Time.time >= ostatni2 + cooldownKlawisza)
        {
            nacisniete2 = true;
            ostatni2 = Time.time;
        }
    }
    private void Start()
    {
        IsAttacking = false;
    }

    [ContextMenu("Test Attack")]
    public void StartAttack()
    {
        Debug.Log("start");
        if (IsAttacking) return;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        IsAttacking = true;

        foreach (Hit hit in hits)
        {

            nacisniete1 = false;
            nacisniete2 = false;
            yield return new WaitForSeconds(hit.delayBefore - 0.8333f);
            if(hit.target == HitTarget.Player1)
            {
                redattack1Animator.SetBool("Atak1Start", true);
            }
            if (hit.target == HitTarget.Player2)
            {
                blueattack1Animator.SetBool("Atak1Start", true);
            }



            yield return new WaitForSeconds(0.7333f);
            nacisniete1 = false;
            nacisniete2 = false;
            
            yield return new WaitForSeconds(0.1f);
            ApplyHit(hit);
            if (hit.target == HitTarget.Player1)
            {
                redattack1Animator.SetBool("Atak1Start", false);
            }
            if (hit.target == HitTarget.Player2)
            {
                blueattack1Animator.SetBool("Atak1Start", false);
            }

        }

        IsAttacking = false;
    }

    void ApplyHit(Hit hit)
    {
        if (skryptGracza == null) return;

        if ((hit.target == HitTarget.Player1 || hit.target == HitTarget.Both) && gracz1 != null && !nacisniete1)
        {
            skryptGracza.ZadajObrazenia(hit.damage, gracz1);
            damage_taken.Play();
        }
        else if ((hit.target == HitTarget.Player1 || hit.target == HitTarget.Both) && gracz1 != null && nacisniete1) parried.Play();


            if ((hit.target == HitTarget.Player2 || hit.target == HitTarget.Both) && gracz2 != null && !nacisniete2)
        {
            skryptGracza.ZadajObrazenia(hit.damage, gracz2);
            damage_taken.Play();
        }
        else if ((hit.target == HitTarget.Player2 || hit.target == HitTarget.Both) && gracz2 != null && nacisniete2) parried.Play();


    }
}