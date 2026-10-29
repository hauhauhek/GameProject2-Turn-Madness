using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obrona : MonoBehaviour
{
    public Transform graczczerw;
    public Transform gracznieb;
    
    public GameObject czerwatak;
    public GameObject niebatak;
    
    
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
        
    }

    Dictionary<Transform, float> cel = new Dictionary<Transform, float>();
    Dictionary<Transform, Coroutine> animacje = new Dictionary<Transform, Coroutine>();

    public void ZadajObrazenia(float hp, Transform gracz)
    {
        if (!cel.ContainsKey(gracz))
            cel[gracz] = gracz.localScale.x;

        cel[gracz] = Mathf.Max(0f, cel[gracz] - hp / 100f);

        if (!animacje.ContainsKey(gracz) || animacje[gracz] == null)
            animacje[gracz] = StartCoroutine(Doganiaj(gracz));
    }

    IEnumerator Doganiaj(Transform gracz)
    {
        float predkosc = 0.5f; // jednostki skali na sekundê
        while (!Mathf.Approximately(gracz.localScale.x, cel[gracz]))
        {
            Vector3 s = gracz.localScale;
            s.x = Mathf.MoveTowards(s.x, cel[gracz], predkosc * Time.deltaTime);
            gracz.localScale = s;
            yield return null;
        }
        animacje[gracz] = null;
    }
}
