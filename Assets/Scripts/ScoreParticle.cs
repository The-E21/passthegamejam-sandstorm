using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TextMeshPro))]
public class ScoreParticle : MonoBehaviour
{
    private TextMeshPro text;
    [HideInInspector] public string score;

    [SerializeField] private float liveTime;
    [SerializeField] private AnimationCurve alphaOverTime;
    [SerializeField] private Color[] colors;
    [SerializeField] private float flashTime;
    [SerializeField] private Vector2 velocity;

    private float timer;
    private int counter;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = score;
        timer = 0;
        counter = 0;
        text.color = nextColor;
        StartCoroutine("flash");
    }

    private void Update() {
        if(timer >= liveTime)
            Destroy(gameObject);

        transform.position += (Vector3)velocity * Time.deltaTime;
        timer += Time.deltaTime;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alphaOverTime.Evaluate(timer));   
    }

    private IEnumerator flash() {
        yield return new WaitForSeconds(flashTime);
        text.color = nextColor;
        StartCoroutine("flash");
    }

    private Color nextColor { 
        get {
            if(counter >= colors.Length)
                counter = 0;
            Color rtn = colors[counter];
            counter ++;

            return new Color(rtn.r, rtn.g, rtn.b, text.color.a);
        }
    }
}
