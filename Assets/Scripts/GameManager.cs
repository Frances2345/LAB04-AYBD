using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class TurnNode
{
    public Vector3 playerPosition;
    public float playerHealth;
    public int playerAttack;
    public float playerSpeed;

    public TurnNode Next;
    public TurnNode Prev;

    public TurnNode(Vector3 pos, float hp, int atk, float speed)
    {
        playerPosition = pos;
        playerHealth = hp;
        playerAttack = atk;
        playerSpeed = speed;
    }
}

public class GameManager : MonoBehaviour
{
    public PlayerController playerScript;
    public TextMeshProUGUI statsText;
    public CinemachineCamera[] cameras;

    private TurnNode head;
    private TurnNode last;
    private TurnNode pivot;

    private int currentCameraIndex = 0;
    private float timer = 7f;

    void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0)
        {
            CopyValues();
            NextTurn();
        }
    }

    void CopyValues()
    {
        Vector3 pos = playerScript.transform.position;
        float hp = playerScript.health;
        int atk = playerScript.attack;

        TurnNode newNode = new TurnNode(pos, hp, atk);

        if (pivot != null && pivot != last)
        {
            last = pivot;
            last.Next = null;
        }

        if (head == null)
        {
            head = newNode;
            last = newNode;
        }
        else
        {
            last.Next = newNode;
            newNode.Prev = last;
            last = newNode;
        }
        pivot = last;

        pivot = last;

        statsText.text = "Turno Guardado:\n" + "Pos: " + pos + "\n" + "Vida: " + hp + "\n" + "Ataque: " + atk;
    }

    public void NextTurn()
    {
        timer = 7f;

        if (cameras.Length > 0)
        {
            cameras[currentCameraIndex].Priority = 0;
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;
            cameras[currentCameraIndex].Priority = 10;
        }

        Debug.Log("Turno siguiente. Camara: " + currentCameraIndex);
    }

}
