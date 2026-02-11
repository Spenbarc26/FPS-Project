using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject hitUI;

    private void Awake()
    {
        instance = this;
    }

    public void InstantiateHitUI()
    {
        Instantiate(hitUI, transform);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
