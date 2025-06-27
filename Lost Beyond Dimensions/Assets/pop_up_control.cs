using UnityEngine; // Required for MonoBehaviour, GameObject, etc.

public class pop_up_control : MonoBehaviour
{
    public GameObject portal;
    public Animator portalAnimator;
    public Animator spaceshipAnimator;

    void Start()
    {
        portal.SetActive(false);
        Invoke("ShowPortal", 3.55f);
    }

    void ShowPortal()
    {
        portal.SetActive(true);
        portalAnimator.Play("pop_up");

        // Start sucking the spaceship after portal is open
        Invoke("SuckInShip", 1.0f); // Delay lets pop_up finish
    }

    void SuckInShip()
    {
        spaceshipAnimator.Play("suck_in");
        // You can add sound later here
        Invoke("ReversePortal", 0.93f); // closer to the end of suck-in
    }
    void ReversePortal()
    {
        portalAnimator.SetTrigger("Reverse");
    }

}
