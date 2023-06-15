using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer frontCard;
    [SerializeField] private ParticleSystem confettiVFX;

    private int cardId;
    private const string FLIPCARD = "FlippedOpen";

    public void SetCard(int id, Sprite sprite)
    {
        cardId = id;
        frontCard.sprite = sprite;
    }

    public void FlippedOpen(bool flipped)
    {
        animator.SetBool(FLIPCARD,flipped);
    }

    public int GetCardId()
    { 
        return cardId; 
    }

    public void PlayConfetti()
    {
        confettiVFX.Play();
    }
}
