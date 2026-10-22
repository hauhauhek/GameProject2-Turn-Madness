using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject atak;
    public Transform healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void zadajDamage()
    {
        Debug.Log("dziala");
        StartCoroutine(ScaleChange(1f));
    }
    private IEnumerator ScaleChange(float czas)
    {
        Debug.Log("Start");
        float scale = 0.1f;
        var startScale = healthBar.localScale;
        var endScale = new Vector2(startScale.x - 0.1f,1);
        var elapsed = 0f;
        
        while (elapsed < czas)
        {
            var t = elapsed / czas;
            healthBar.localScale = Vector2.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        healthBar.localScale = endScale;
        Debug.Log("kon");
    }
}
