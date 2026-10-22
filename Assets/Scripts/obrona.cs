using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obrona : MonoBehaviour
{
    public Transform graczczerw;
    public Transform gracznieb;
    public float interwal = 2f;
    private float timer;
    public GameObject czerwatak;
    public GameObject niebatak;
    bool nacisniete = false;
    bool pauza = false;
    private float timerAtaku = 0f;
    private float licznik;
    private float czas;
    public float cooldownN = 1f;
    private float ostatniN = -999f;
    // Start is called before the first frame update
    void Start()
    {
        czerwatak.SetActive(false);
        niebatak.SetActive(false);
        

    }
    private bool atakTrwa;
    // Update is called once per frame
    void Update()
    {
        czas += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.N) && Time.time >= ostatniN + cooldownN)
        {
            nacisniete = true;
            ostatniN = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space)) Atak();
        if (czas >= licznik) { Atak(); }

        if (atakTrwa)
        {
            timerAtaku += Time.deltaTime;
            if (timerAtaku >= 0.3f)
            {
                EndAtak();
            }
        }
    }
    private void Atak()
    {
        if (atakTrwa == true) return;
        nacisniete = false;
        atakTrwa = true;
        timerAtaku = 0f;
        czerwatak.SetActive(true);
    }
    private void EndAtak()
    {
        czas = 0f;
        atakTrwa = false;
        czerwatak.SetActive(false);
        licznik = Random.Range(1f, 2f);

        if (!nacisniete)
        {
            ZadajObrazenia(10, graczczerw);
        }
    }
    private void Animczerw()
    {
        czerwatak.SetActive (true); 
        
    }
    private void Animnieb()
    {
        niebatak.SetActive(true);

    }
    public void ZadajObrazenia(float hp,Transform gracz)
    {
        float scale = hp / 100;
        float czas = hp / 20;
        StartCoroutine(ScaleChange(czas,gracz,scale));
    }
    private IEnumerator ScaleChange(float czas,Transform gracz,float scale)
    {
        Debug.Log("Start");
        
        var startScale = gracz.localScale;
        var endScale = new Vector2(startScale.x - scale, 1);
        var elapsed = 0f;

        while (elapsed < czas)
        {
            var t = elapsed / czas;
            gracz.localScale = Vector2.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        gracz.localScale = endScale;
        Debug.Log("kon");
    }
}
