using UnityEngine; // Required for MonoBehaviour, GameObject, etc.

public class pop_up_control1 : MonoBehaviour
{
    public GameObject portal;
    public Animator portalAnimator;
    public Animator spaceshipAnimator;

    void Start()
    {
        portal.SetActive(false);
        Invoke("ShowPortal", 6.55f);
    }

    void ShowPortal()
    {
        portal.SetActive(true);
        portalAnimator.Play("pop_up 1");

        // Start sucking the spaceship after portal is open
        Invoke("SuckInShip", 1.5f); // Delay lets pop_up finish
    }

    void SuckInShip()
    {
        spaceshipAnimator.Play("suck_in 1");
        // You can add sound later here
        Invoke("ReversePortal", 1.30f); // closer to the end of suck-in
    }
    void ReversePortal()
    {
        portalAnimator.SetTrigger("Reverse");
    }

}
