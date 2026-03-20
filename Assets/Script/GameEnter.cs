using UnityEngine;

public class GameEnter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Battle.Instance.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Battle.Instance.Update();
    }
}
