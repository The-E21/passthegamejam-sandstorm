using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance {get; private set;}

    [SerializeField] private string startElement;
    [SerializeField] private UIElement[] elements;

    private void Start() {
        Instance = this;
        SwapTo(startElement);
    }

    public void SwapTo(string name = null) {
        foreach(UIElement element in elements) {
            if(element.name == name){
                element.element.SetActive(true);
            } else {
                element.element.SetActive(false);
            }
        }
    }
}
