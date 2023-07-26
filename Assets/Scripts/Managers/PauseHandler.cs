using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public static PauseHandler Instance {get; private set;}

    [SerializeField] private KeyCode pauseKey;
    [SerializeField] private string pauseMenu;
    private float playTimescale;
    public bool paused {get; private set;}

    void Start()
    {
        Instance = this;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(pauseKey)) {
            if(paused)
                Play();
            else
                Pause();
        }
    }

    public void Pause() {
        if(paused)
            return;
        
        playTimescale = Time.timeScale;
        UIHandler.Instance.SwapTo(pauseMenu);
        Time.timeScale = 0;
        paused = true;
    }

    public void Play() {
        if(!paused)
            return;
        Time.timeScale = playTimescale;
        UIHandler.Instance.SwapTo();
        paused = false;
    }
}
