using UnityEngine;

public class PlayerIntangibility : MonoBehaviour
{
    public bool isIntangible {get; private set;}
    [SerializeField] private LayerMask ignoreLayers;

    private void Start() {
        isIntangible = false;
    }

    public void makeIntangible(){
        if(isIntangible)
            return;
        
        isIntangible = true;
        for(int i = 0; i < 32; i ++){
            if(ignoreLayers == (ignoreLayers | (1 << i))) {
                Physics2D.IgnoreLayerCollision(gameObject.layer, i);
            }
        }
    }

    public void makeTangible(){
        if(!isIntangible)
            return;
        
        isIntangible = false;
        for(int i = 0; i < 32; i ++){
            if(ignoreLayers == (ignoreLayers | (1 << i))) {
                Physics2D.IgnoreLayerCollision(gameObject.layer, i, false);
            }
        }
    }
}
