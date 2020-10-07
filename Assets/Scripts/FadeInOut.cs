using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 1, 1, 0f);

        for(float f = 0; f <= 1f; f += Time.deltaTime / 2f)
        {
            renderer.color = new Color(1, 1, 1, f);
            yield return null;
        }
        renderer.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(10);
        for (float f = 1; f >= 0f; f -= Time.deltaTime / 2f)
        {
            renderer.color = new Color(1, 1, 1, f);
            yield return null;
        }
        renderer.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Title");
    }
}
