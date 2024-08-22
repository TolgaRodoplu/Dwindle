using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Intro : MonoBehaviour
{
    public bool isOutro;
    public string[] credits;
    public TextMeshProUGUI textBox = null;
    public float frequency = 0.2f;
    public float startWait = 1f;
    public float endWait = 1f;
    public float endOfLineWait = 0.2f;

    void Start()
    {
        StartCoroutine(PlayOntro());
    }

    IEnumerator PlayOntro()
    {
        yield return new WaitForSeconds(startWait);
        foreach (string item in credits)
        {
            int cnt = -1;
            foreach (char c in item)
            {
                textBox.text += c;
                FindObjectOfType<AudioManeger>().Play("Keyboard" + Random.Range(1, 3).ToString());
                cnt++;
                yield return new WaitForSeconds(frequency);
            }

            yield return new WaitForSeconds(endOfLineWait);
            FindObjectOfType<AudioManeger>().Play("Keyboard" + Random.Range(1, 3).ToString());
            textBox.text = null;
        }
        yield return new WaitForSeconds(endWait);

        if(isOutro)
            EventSystem.instance.GoToMenu();
        else
            EventSystem.instance.StartPlay();

        Destroy(gameObject);
    }
}
