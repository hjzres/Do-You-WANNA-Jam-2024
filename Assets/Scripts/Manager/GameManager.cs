using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Camera Cam1, Cam2;
    public Text State;
    [SerializeField] private float rate;

    private bool _inside = true;
    private Transform _playerOneStart, _playerTwoStart;

    protected override void Awake()
    {
        base.Awake();

        // Ensure State is assigned before using it
        if (State == null)
        {
            State = GameObject.Find("InOrOut")?.GetComponent<Text>();
        }

        if (Cam1 != null) Cam1.enabled = true;
        if (Cam2 != null) Cam2.enabled = false;

        if (State != null) InvokeRepeating(nameof(FlashText), rate, rate);
    }

    public void SwitchCamera()
    {
        if (Cam1 != null && Cam2 != null)
        {
            Cam1.enabled = !Cam1.enabled;
            Cam2.enabled = !Cam2.enabled;
            _inside = !_inside;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
            if (State != null)
            {
                State.text = _inside ? "Inside" : "Out";
            }
        }
    }

    private void FlashText()
    {
        if (State != null)
        {
            State.gameObject.SetActive(!State.gameObject.activeSelf);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
