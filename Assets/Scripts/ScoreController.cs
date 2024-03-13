using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    //[SerializeField] ParticleSystem[] AdditionEffectParticles;
    //[SerializeField] ParticleSystem[] RestartEffectParticles;
    private Animator anim;

    public int score { get; private set; }
    public TextMeshProUGUI text;

    public void Init()
    {
        SetScore(Environment.savingSystem.lastScore);
        anim = text.GetComponent<Animator>();
    }

    public void PlayAdditionEffect()
    {
        /*foreach (ParticleSystem particleSystem in AdditionEffectParticles)
        {
            ParticleSystem particles = Instantiate(particleSystem, transform);
            particles.Play();
            Destroy(particles, 3);
        }*/
        anim = text.GetComponent<Animator>();
        anim.SetTrigger("Add");
    }
    public void PlayRestartEffect()
    {
        /*foreach (ParticleSystem particleSystem in RestartEffectParticles)
        {
            ParticleSystem particles = Instantiate(particleSystem, transform);
            particles.Play();
            Destroy(particles, 3);
        }*/
    }

    public void SetScore(int n)
    {
        score = n;
        text.text = score.ToString();

        //if (score > 0) // Do not play on awake
        //    PlayAdditionEffect();
    }
    public void IncreaseScore(int n) => SetScore(score + n);
}