﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button playerDeckButton;

    public DropZone playerField;
    public DropZone enemyField;

    public Draggable draggable;

    bool matchStart;
    bool drawPhase;
    public bool summoningPhase;
    bool spellPhase;
    public bool combatPhase;
    bool endTurn;
    bool playerTurn;
    bool firstTurn; 
    bool coinFlip;
    bool coinFlipped;
    bool cardDrawn;
    public bool monsterSummoned;
    public bool endCombat;

    public string[] deckContents = new string[] {"Chicken", "Valiant Knight", "Brutus the Strong", "The Very Ancient Wizard", "Guardian of The Abyss"};

    //public CardContainer[] cards;

    //public CardContainer[] cards;
    public GameObject[] cardPrefabs;

    public List<string> playerDeck;
    public List<string> enemyDeck;

    float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        matchStart = true;
        firstTurn = true;

        waitTime = Time.time + 1;

        playerDeck = BuildDeck();
        Shuffle(playerDeck);

        enemyDeck = BuildDeck();
        Shuffle(enemyDeck);

        playerDeckButton.enabled = false;

        FirstDraw();

        CoinFlip();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (drawPhase)
        {
            if(playerTurn && !cardDrawn)
            {
                playerDeckButton.enabled = true;
            }
            else if(!playerTurn && !cardDrawn)
            {     
                if(Time.time > waitTime)
                {
                    EnemyDraw();
                } 
            }
        }

        if (summoningPhase)
        {
            if (playerTurn && !monsterSummoned)
            {
                playerField.enabled = true;
            }
            else if(!playerTurn && !monsterSummoned)
            {
                enemyField.enabled = true;

                //enemy summons
            }
            else if(monsterSummoned)
            {
                playerField.enabled = false;
                enemyField.enabled = false;
                summoningPhase = false;
                spellPhase = true;
            }
        }

        if (spellPhase)
        {
            //skipping through phase as it is not being used.

            spellPhase = false;
            combatPhase = true;
        }

        if (combatPhase)
        {
            draggable.inCombat = true;

            if(endCombat)
            {
                combatPhase = false;
                endTurn = true;
                draggable.inCombat = false;
            }
        }

        if (endTurn)
        {
            if(playerTurn)
            {
                playerTurn = false;
                endTurn = false;
                drawPhase = true;
            }
            else if(!playerTurn)
            {
                playerTurn = true;
                endTurn = false;
                drawPhase = true;
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    public List<string> BuildDeck()
    {
        List<string> newDeck = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            newDeck.Add(deckContents[0]);
            newDeck.Add(deckContents[1]);
            newDeck.Add(deckContents[2]);
            newDeck.Add(deckContents[3]);
            newDeck.Add(deckContents[4]);

            Debug.Log("Card Added");
        }

        Debug.Log("Deck Built");

        Debug.Log(newDeck);

        

        return newDeck;
    }

    public void FirstDraw()
    {
            int deckSize = playerDeck.Count - 1;
            Debug.Log(deckSize);
            
            for (int i = deckSize; i > (deckSize - 3);)
            {
                if(playerDeck[i] == cardPrefabs[0].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[0], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[0].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                }
                else if(playerDeck[i] == cardPrefabs[1].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[1], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[1].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    }
                else if(playerDeck[i] == cardPrefabs[2].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[2], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[2].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                }
                else if(playerDeck[i] == cardPrefabs[3].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[3], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[3].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                }
                else if(playerDeck[i] == cardPrefabs[4].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[4], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[4].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                }

                playerDeck.RemoveAt(i);

                i--;
            }

            deckSize = enemyDeck.Count -1;
            Debug.Log(deckSize);

            for (int i = deckSize; i > (deckSize - 3);)
            {

                if(enemyDeck[i] == cardPrefabs[0].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[0], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
                    newCard.name = cardPrefabs[0].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.enemyCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    newCard.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if(enemyDeck[i] == cardPrefabs[1].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[1], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
                    newCard.name = cardPrefabs[1].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.enemyCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    newCard.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if(enemyDeck[i] == cardPrefabs[2].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[2], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
                    newCard.name = cardPrefabs[2].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.enemyCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                    newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    newCard.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if(enemyDeck[i] == cardPrefabs[3].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[3], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
                    newCard.name = cardPrefabs[3].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.enemyCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                  newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    newCard.transform.GetChild(5).gameObject.SetActive(true);
                }
                else if(enemyDeck[i] == cardPrefabs[4].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[4], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
                    newCard.name = cardPrefabs[4].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.enemyCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                   newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    newCard.transform.GetChild(5).gameObject.SetActive(true);
                }
                enemyDeck.RemoveAt(i);

                i--;
            }

            matchStart = false;
            drawPhase = true;

            Debug.Log("Enemy Deck Size" + enemyDeck.Count);
            Debug.Log("Player Deck Size" + playerDeck.Count);
    }

    public void Draw()
    {
        
        if(playerTurn)
        {
            int i = playerDeck.Count - 1;
            Debug.Log(i);

            if(playerDeck[i] == cardPrefabs[0].name && i > -1)
                {
                    GameObject newCard = Instantiate(cardPrefabs[0], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[0].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                   newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    playerDeck.RemoveAt(i);
                }
                else if(playerDeck[i] == cardPrefabs[1].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[1], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[1].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                  newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    playerDeck.RemoveAt(i);
                }
                else if(playerDeck[i] == cardPrefabs[2].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[2], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[2].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                  newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    playerDeck.RemoveAt(i);
                }
                else if(playerDeck[i] == cardPrefabs[3].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[3], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[3].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                  newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    playerDeck.RemoveAt(i);
                }
                else if(playerDeck[i] == cardPrefabs[4].name)
                {
                    GameObject newCard = Instantiate(cardPrefabs[4], transform.position, Quaternion.identity, GameObject.Find("/Canvas/PlayerHand").transform);
                    newCard.name = cardPrefabs[4].name;
                    CardContents newCardContents = newCard.GetComponent<CardContents>();
                    newCardContents.playerCard = true;
                    newCardContents.nameText.text = newCardContents.cardContainer.cardName;
                 newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
                    playerDeck.RemoveAt(i);
                }
        }
        else
        {
            Debug.Log("Enemy Turn");
        }

        cardDrawn = true;
        playerDeckButton.enabled = false;

        drawPhase = false;
        summoningPhase = true;

        Debug.Log("Enemy Deck Size" + enemyDeck.Count);
        Debug.Log("Player Deck Size" + playerDeck.Count);
    }

    void EnemyDraw()
    {

        int i = enemyDeck.Count -1;

        if(enemyDeck[i] == cardPrefabs[0].name && i > -1)
        {
            GameObject newCard = Instantiate(cardPrefabs[0], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
            newCard.name = cardPrefabs[0].name;
            CardContents newCardContents = newCard.GetComponent<CardContents>();
            newCardContents.enemyCard = true;
            newCardContents.nameText.text = newCardContents.cardContainer.cardName;
           newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
            newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
            enemyDeck.RemoveAt(i);
            newCard.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if(playerDeck[i] == cardPrefabs[1].name)
        {
            GameObject newCard = Instantiate(cardPrefabs[1], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
            newCard.name = cardPrefabs[1].name;
            CardContents newCardContents = newCard.GetComponent<CardContents>();
            newCardContents.enemyCard = true;
            newCardContents.nameText.text = newCardContents.cardContainer.cardName;
          newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
            newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
            enemyDeck.RemoveAt(i);
            newCard.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if(enemyDeck[i] == cardPrefabs[2].name)
        {
            GameObject newCard = Instantiate(cardPrefabs[2], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
            newCard.name = cardPrefabs[2].name;
            CardContents newCardContents = newCard.GetComponent<CardContents>();
            newCardContents.playerCard = true;
            newCardContents.nameText.text = newCardContents.cardContainer.cardName;
          newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
            newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
            enemyDeck.RemoveAt(i);
            newCard.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if(enemyDeck[i] == cardPrefabs[3].name)
        {
            GameObject newCard = Instantiate(cardPrefabs[3], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
            newCard.name = cardPrefabs[3].name;
            CardContents newCardContents = newCard.GetComponent<CardContents>();
            newCardContents.playerCard = true;
            newCardContents.nameText.text = newCardContents.cardContainer.cardName;
         newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
            newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
            enemyDeck.RemoveAt(i);
            newCard.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if(enemyDeck[i] == cardPrefabs[4].name)
        {
            GameObject newCard = Instantiate(cardPrefabs[4], transform.position, Quaternion.identity, GameObject.Find("/Canvas/EnemyHand").transform);
            newCard.name = cardPrefabs[4].name;
            CardContents newCardContents = newCard.GetComponent<CardContents>();
            newCardContents.playerCard = true;
            newCardContents.nameText.text = newCardContents.cardContainer.cardName;
          newCardContents.attackValue = newCardContents.cardContainer.attack;
                    newCardContents.healthValue = newCardContents.cardContainer.health;
                    newCardContents.attackValueArtwork = newCardContents.numericValues[newCardContents.attackValue];
                    newCardContents.healthValueArtwork = newCardContents.numericValues[newCardContents.healthValue];
                    newCard.transform.GetChild(0).GetComponent<Image>().sprite = newCardContents.cardTemplate;
                    newCard.transform.GetChild(1).GetComponent<Image>().sprite = newCardContents.cardArtwork;
                    newCard.transform.GetChild(3).GetComponent<Image>().sprite = newCardContents.attackValueArtwork;
                    newCard.transform.GetChild(4).GetComponent<Image>().sprite = newCardContents.healthValueArtwork;
                    newCard.transform.GetChild(5).GetComponent<Image>().sprite = newCardContents.cardBack;
            enemyDeck.RemoveAt(i);
            newCard.transform.GetChild(5).gameObject.SetActive(true);
        }
        cardDrawn = true;

        drawPhase = false;
        summoningPhase = true;
    }

    void CoinFlip()
    {
        coinFlip = (Random.value > 0.5f);
        if (coinFlip)
        {
            //play Heads coin flip animation
            playerTurn = true;
            Debug.Log("Player goes first");
        }
        else
        {
            //play Tails coin flip animation
            Debug.Log("Enemy goes first");
        }
        coinFlipped = true;
        

        Debug.Log("Coin Flipped");
    }
}
