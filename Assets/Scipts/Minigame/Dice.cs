using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    [Header("Dice")]
    public GameObject[] _Dice;
    [Space]

    [Header("DiceRotation")]
    //Quaternion[] _DiceRotation;
    //public int[] _DiceNumber;
    //Change the 0 Values that allow rotation (that does not change the number on top)
    //to be randomized instead of always 0 and therefore straight
    Quaternion Q_Dice_1 = Quaternion.Euler(90, 0, 0);
    Quaternion Q_Dice_2 = Quaternion.Euler(0, 0, -90);
    Quaternion Q_Dice_3 = Quaternion.Euler(180, 0, 0);
    Quaternion Q_Dice_4 = Quaternion.Euler(0, 0, 0);
    Quaternion Q_Dice_5 = Quaternion.Euler(0, 0, 90);
    Quaternion Q_Dice_6 = Quaternion.Euler(-90, 0, 0);

    //public int _RandomY;
    //public int _RandomX;
    //public int _RandomZ;

    [Header("Random_Number")]
    private int[] _Dice_Number = new int[3];
    private int _Dice_Sum;
    public TMP_Text _Txt_Dice_Sum;

    [Header("Buttons")]
    public Button _B_Even;
    public Button _B_Odd;
    public Button _B_NanamiTurn_Even;
    public Button _B_NanamiTurn_Odd;
    [Space]
    public Button _B_NextRound;

    [Header("ScoreCounters")]
    //Red = DANGER | Blue = SAFETY
    private int _Player_RedToken = 0;
    private int _Player_BlueToken = 10;
    private int _Nanami_RedToken = 10;
    private int _Nanami_BlueToken = 0;
    //ScoreCount Text
    public TMP_Text _Txt_Player_RedToken;
    public TMP_Text _Txt_Player_BlueToken;
    public TMP_Text _Txt_Nanami_RedToken;
    public TMP_Text _Txt_Nanami_BlueToken;

    [Header("EndScreens")]
    public GameObject _WinningScreen;
    public GameObject _LostScreen;

    [Header("Bet Results")]
    public TMP_Text _Txt_Nanmi_Bet;
    public TMP_Text _Txt_Player_Bet;

    //Buttons Player clicks
    private bool _Even;
    private bool _Odd;
    //Random Even/Odd bool for nanamis bet
    private bool _NanamiHasBet;
    private bool _NanamiEvenBool;
    private int _NanamiEvenInt;
    //bools for whose turn it is to bet first (player always starts in new round, nanami not)
    private bool _PlayerTurn = true;
    private bool _NanamiTurn = false;


//START
    void Start()
    {
        //_BetSet = false;
        _Even = false;
        _Odd = false;
       

    }

//UPDATES
    void Update()
    {

    }


//FUNCTIONS

    //Set Even and Odd bool to true when the respective button is pressed
    public void _Bet_Even()
    {
        _Even = true;
        _Odd = false;
        _Txt_Player_Bet.text = "Player bet Even";

    } 
    public void _Bet_Odd()
    {
        _Odd = true;
        _Even = false;
        _Txt_Player_Bet.text = "Player bet Odd";

    }


    //PLAYER 
    //Roll Dice if Even or Odd are true (Button has been pressed)
    public void DiceRoll_Player()
    {
        if (_PlayerTurn == true && _NanamiTurn == false)
        {
            

            Debug.Log("Playerturn");            
            //DiceRolling Mechanic
            //Roll when even/odd button clicked
            if (_Even == true || _Odd == true)
            {
                //DiceRollAnimation
                //Disable Even and Odd Buttons
                _B_Odd.gameObject.SetActive(false);
                _B_Even.gameObject.SetActive(false);

                // When _BetSet(Button clicked) true, then generate numbers (and roll dice)
                // generate random number and place it on _Dice_Number
                // as many dice there are, as many dice numbers shall be generated
                for (int i = 0; i < _Dice.Length; i++)
                {
                    _Dice_Number[i] = Random.Range(1, 6);
                    //Debug.Log(_Dice_Number[i]);

                }


                //Dice Rotation
                for (int k = 0; k < _Dice_Number.Length; k++)
                {
                    if (_Dice_Number[k] == 1)
                    {
                        _Dice[k].transform.rotation = Q_Dice_1;
                    }
                    else if (_Dice_Number[k] == 2)
                    {
                        _Dice[k].transform.rotation = Q_Dice_2;
                    }
                    else if (_Dice_Number[k] == 3)
                    {
                        _Dice[k].transform.rotation = Q_Dice_3;
                    }
                    else if (_Dice_Number[k] == 4)
                    {
                        _Dice[k].transform.rotation = Q_Dice_4;
                    }
                    else if (_Dice_Number[k] == 5)
                    {
                        _Dice[k].transform.rotation = Q_Dice_5;
                    }
                    else if (_Dice_Number[k] == 6)
                    {
                        _Dice[k].transform.rotation = Q_Dice_6;
                    }
                }


                //Random for Nanamis Bet
                //Random number 1 or 2
                //1 == odd | 2 == even
                _NanamiEvenInt = Random.Range(1, 3);
                if (_NanamiEvenInt == 1)
                {
                    _NanamiEvenBool = false;
                    //Debug.Log(_NanamiEvenBool);
                    _Txt_Nanmi_Bet.text = "Nanami bet Odd";

                }
                else if (_NanamiEvenInt == 2)
                {
                    _NanamiEvenBool = true;
                    //Debug.Log(_NanamiEvenBool);
                    _Txt_Nanmi_Bet.text = "Nanami bet Even";

                }




                //Adding Numbers on Dice together and displaying in UI
                foreach (int item in _Dice_Number)
                {
                    _Dice_Sum += item;

                }


                //Exchanging Tokens/Points between Player + Nanami depending on what they bet


                _Txt_Dice_Sum.text = _Dice_Sum.ToString();

                //Token Calculations + Endgame
                TokenCalculation();
                FinishMinigame();

                //Next starter is nanami
                _PlayerTurn = false;
                _NanamiTurn = true;

                _B_NextRound.gameObject.SetActive(true);

                _Dice_Sum = 0;
            }

        }
    }

    //NANAMI
    //Roll Dice if Even or Odd are true (Button has been pressed)
    public void DiceRoll_Nanami()
    {
       
       if (_PlayerTurn == false && _NanamiTurn == true && _NanamiHasBet == true)
       {

            

            //DiceRolling Mechanic
            //Roll when even/odd button clicked
            if (_Even == true || _Odd == true)
            {
                //DiceRollAnimation
                //Disable Even and Odd Buttons
                _B_NanamiTurn_Odd.gameObject.SetActive(false);
                _B_NanamiTurn_Even.gameObject.SetActive(false);

                // When _BetSet(Button clicked) true, then generate numbers (and roll dice)
                // generate random number and place it on _Dice_Number
                // as many dice there are, as many dice numbers shall be generated
                for (int i = 0; i < _Dice.Length; i++)
                {
                    _Dice_Number[i] = Random.Range(1, 6);
                    //Debug.Log(_Dice_Number[i]);

                }


                //Dice Rotation
                for (int k = 0; k < _Dice_Number.Length; k++)
                {
                    if (_Dice_Number[k] == 1)
                    {
                        _Dice[k].transform.rotation = Q_Dice_1;
                    }
                    else if (_Dice_Number[k] == 2)
                    {
                        _Dice[k].transform.rotation = Q_Dice_2;
                    }
                    else if (_Dice_Number[k] == 3)
                    {
                        _Dice[k].transform.rotation = Q_Dice_3;
                    }
                    else if (_Dice_Number[k] == 4)
                    {
                        _Dice[k].transform.rotation = Q_Dice_4;
                    }
                    else if (_Dice_Number[k] == 5)
                    {
                        _Dice[k].transform.rotation = Q_Dice_5;
                    }
                    else if (_Dice_Number[k] == 6)
                    {
                        _Dice[k].transform.rotation = Q_Dice_6;
                    }
                }


                //Adding Numbers on Dice together and displaying in UI
                foreach (int item in _Dice_Number)
                {
                    _Dice_Sum += item;

                }

                _Txt_Dice_Sum.text = _Dice_Sum.ToString();

                //Token Calculations + Endgame
                TokenCalculation();
                FinishMinigame();

                //Next starter is player
                _PlayerTurn = true;
                _NanamiTurn = false;

                _NanamiHasBet = false;

                //Switch out the Buttons
                

                _B_Odd.gameObject.SetActive(true);
                _B_Even.gameObject.SetActive(true);

                _Dice_Sum = 0;
            }

        }

    }


    //Nanami Bet when shes first
    public void _NextRoundNanamiBet()
    {
        Debug.Log("Nanamiturn");
        //Random for Nanamis Bet
        //Random number 1 or 2
        //1 == odd | 2 == even
        _NanamiEvenInt = Random.Range(1, 3);
        if (_NanamiEvenInt == 1)
        {
            _NanamiEvenBool = false;
            //Debug.Log(_NanamiEvenBool);
            _Txt_Nanmi_Bet.text = "Nanami bet Odd";

        }
        else if (_NanamiEvenInt == 2)
        {
            _NanamiEvenBool = true;
            //Debug.Log(_NanamiEvenBool);
            _Txt_Nanmi_Bet.text = "Nanami bet Even";
        }

        _B_NanamiTurn_Odd.gameObject.SetActive(true);
        _B_NanamiTurn_Even.gameObject.SetActive(true);

        _B_NextRound.gameObject.SetActive(false);

        _NanamiHasBet = true;

    }



    //Calculate when which person gets how many tokens depending on their bets
    public void TokenCalculation()
    {
        //If DiceSum EVEN
        if (_Dice_Sum % 2 == 0)
        {
            //Both bet ODD
            if (_Even == false && _NanamiEvenBool == false)
            {
                //no trade
                PrintTokenScores();
            }
            //Both bet EVEN
            else if (_Even == true && _NanamiEvenBool == true)
            {
                //Player takes two Danger Tokens from Nanami
                //Player gives two Safety Tokens to Nanami
                _Player_RedToken += 2;
                _Player_BlueToken -= 2;
                //Nanami takes two Safety Tokens from Player
                //Nanami gives two Danger Tokens to Player
                _Nanami_RedToken -= 2;
                _Nanami_BlueToken += 2;
                PrintTokenScores();
            }
            //Player bet Odd, Nanami bet Even -> nanami Won
            else if (_Even == false && _NanamiEvenBool == true)
            {
                //Player gives two Safety Tokens to Nanami                
                _Player_BlueToken -= 2;

                //Nanami takes two Safety Tokens from Player
                _Nanami_BlueToken += 2;
                PrintTokenScores();
            }
            //Player bet Even, Nanami bet Odd -> player won
            else if (_Even == true && _NanamiEvenBool == false)
            {
                //Player takes two Danger Tokens from Nanami
                _Player_RedToken += 2;
                //Nanami gives two Danger Tokens to Player
                _Nanami_RedToken -= 2;
                PrintTokenScores();
            }
        }

        //If DiceSum ODD
        else if (_Dice_Sum % 2 == 1)
        {
            //Both bet EVEN
            if (_Even == true && _NanamiEvenBool == true)
            {
                //no trade
                PrintTokenScores();
            }
            //Both bet ODD
            else if (_Even == false && _NanamiEvenBool == false)
            {
                //Player takes two Danger Tokens from Nanami
                //Player gives two Safety Tokens to Nanami
                _Player_RedToken += 2;
                _Player_BlueToken -= 2;
                //Nanami takes two Safety Tokens from Player
                //Nanami gives two Danger Tokens to Player
                _Nanami_RedToken -= 2;
                _Nanami_BlueToken += 2;
                PrintTokenScores();
            }
            //Player bet even, Nanami bet ODD -> nanami won
            else if (_Even == true && _NanamiEvenBool == false)
            {
                //Player gives two Safety Tokens to Nanami                
                _Player_BlueToken -= 2;

                //Nanami takes two Safety Tokens from Player
                _Nanami_BlueToken += 2;
                PrintTokenScores();
            }
            //Player bet odd, Nanami bet even -> player won
            else if (_Even == false && _NanamiEvenBool == true)
            {
                //Player takes two Danger Tokens from Nanami
                _Player_RedToken += 2;
                //Nanami gives two Danger Tokens to Player
                _Nanami_RedToken -= 2;
                PrintTokenScores();
            }
        }

    }

    //Write the Token scores into the UI
    public void PrintTokenScores()
    {
        _Txt_Player_RedToken.text = _Player_RedToken.ToString();
        _Txt_Player_BlueToken.text = _Player_BlueToken.ToString();
        _Txt_Nanami_RedToken.text = _Nanami_RedToken.ToString();
        _Txt_Nanami_BlueToken.text = _Nanami_BlueToken.ToString();

    }

    //Check if the Game has been Won or Lost
    public void FinishMinigame()
    {
        if (_Player_RedToken == 10 && _Nanami_BlueToken == 10)
        {
            Debug.Log("Game finished successfully: All Tokens traded");
            _WinningScreen.SetActive(true);
        }
        else if (_Player_RedToken == 10 && _Player_BlueToken == 10)
        {
            Debug.Log("Game failed: Player has all Tokens");
            _LostScreen.SetActive(true);
        }
        else if (_Player_RedToken < 0 || _Player_BlueToken < 0 || _Nanami_BlueToken < 0 || _Nanami_RedToken < 0)
        {
            Debug.Log("Game failed: No Tokens can be exchanged anymore");
            _LostScreen.SetActive(true);

        }
        else if (_Nanami_RedToken == 10 && _Nanami_BlueToken == 10)
        {
            Debug.Log("Game failed: Nanami has all Tokens");
            _LostScreen.SetActive(true);
        }
        
    }
}
