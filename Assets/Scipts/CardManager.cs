using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [HideInInspector] public int pairAmount;
    public List<Sprite> spritesList = new List<Sprite>();
    public GameObject cardPrefab;
    [HideInInspector] public int width;
    [HideInInspector] public int height;
    [SerializeField] private List<GameObject> cardDeck = new List<GameObject>();

    private float offSet = 1.2f;
    private void Start()
    {
        GameManager.Instance.SetPairs(pairAmount);
        CreatePlayField();
    }

    private void CreatePlayField()
    {
        int startingPoint = 0;
        List<Sprite> tempSprites = new List<Sprite>();
        tempSprites.AddRange(spritesList);

        for (int i = 0; i < pairAmount; i++)
        {
            int randSpriteIndex = Random.Range(0, tempSprites.Count);

            for (int j = 0; j < 2; j++)
            {
                Vector3 position = Vector3.zero;
                GameObject newCard = Instantiate(cardPrefab, position, Quaternion.identity);
                newCard.GetComponent<Card>().SetCard(i, tempSprites[randSpriteIndex]);
                cardDeck.Add(newCard);
            }

            tempSprites.RemoveAt(randSpriteIndex);
        }

        for (int i = 0;i < cardDeck.Count; i++)
        {
            int randomIndex = Random.Range(0, cardDeck.Count);
            var temporary = cardDeck[i];
            cardDeck[i] = cardDeck[randomIndex];
            cardDeck[randomIndex] = temporary;
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 cardPostiton = transform.position + new Vector3(x * offSet, 0, z * offSet);
                cardDeck[startingPoint].transform.position = cardPostiton;
                startingPoint++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 cardPostiton = transform.position + new Vector3(x * offSet, 0, z * offSet);
                Gizmos.DrawWireCube(cardPostiton, new Vector3(1,0.1f,1));
            }
        }
    }
}
