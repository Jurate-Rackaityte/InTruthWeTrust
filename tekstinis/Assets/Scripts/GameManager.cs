using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Scripts.Class;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera UI;
    public Camera HerosRoom;
    public Camera PrisonOutOfCapsule;
    public Camera PrisonTalkToRobot;
    public Camera PrisonTV;
    public Camera Corridor01GotOut;
    public Camera Corridor02TurnOrRoom;
    public Camera Corridor03;
    public Camera Corridor04;
    public Camera Corridor05;
    public Camera Corridor06BrokenElevator;
    public Camera Corridor07SecondElevator;
    public Camera Corridor08Mystery;
    public Camera Corridor09Dirty;
    public Camera Corridor10FinalBattle;
    public Camera BurningRoom;
    public Camera LeakingGas;
    public Camera DirtyRoom2;

    public ParticleSystem Fire;
    public ParticleSystem Water;
    public ParticleSystem LeakingGasWall;
    public ParticleSystem LeakingGasGround;

    public Text StoryText;
    public Text manaText;
    public Text healthText;
    public Text Explanation;
    public Button choice1;
    //Text button1Text;
    public Button choice2;
    //Text button2Text;
    public Button choice3;
    //Text button3Text;
    public Button choice4;
    //Text button4Text;
    public Button choice5;
    //Text button5Text;
    public Button choice6;
    //Text button6Text;

    private Story storyArray;
    private StoryAndPointer pointer;
    private bool hasOptions;
    private StoryAndPointer[] choices;
    public string PlayersName;

    public int PH, PM, JoaH, JoaM, JosH, JosM, aggresiveness;
    public int choiceOPtion;
    //PACIOJE PRADZIOJE PAKLAUSK HEROJAUS VARDO

    // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(this);

        disableAllCameras();

        UI.enabled = true;

        Fire.enableEmission = false;
        Water.enableEmission = false;
        LeakingGasWall.enableEmission = false;
        LeakingGasGround.enableEmission = false;

        disableAllButtons();
        //button1Text = transform.FindChild("Text").GetComponent<Text>();
        //button2Text = transform.FindChild("Text").GetComponent<Text>();
        //button3Text = transform.FindChild("Text").GetComponent<Text>();
        //button4Text = transform.FindChild("Text").GetComponent<Text>();
        //button5Text = transform.FindChild("Text").GetComponent<Text>();
        //button6Text = transform.FindChild("Text").GetComponent<Text>();

        //StoryText.text = "Write player\'s name and press ENTER";
        storyArray = new Assets.Scripts.Class.Story(PlayersName);
        manaText.text = "100";  healthText.text = "100";
        pointer = new StoryAndPointer();
        hasOptions = false; choices = new StoryAndPointer[10];
        Explanation.enabled = false;
        //Explanation.text = "To read the story, press SPACE\nTo choose an option, press its button";
    }
    //palaukti keleta sec
    //private IEnumerator wait(int sec)
    //{
    //    Debug.Log("ESU");
    //    yield return new WaitForSeconds(sec);
    //}
    // Update is called once per frame
    private void wait(int milisec)
    {
        System.Threading.Thread.Sleep(milisec);
    }
    //Update is called once per frame
    void Update()
    {
        if(!storyArray.thisIsTheEnd())
        {
            if(Input.GetKey("space") && hasOptions 
                && choiceOPtion == 0
                )
            {
                Debug.Log(hasOptions + "\n" + pointer.getPointer());
                wait(500);
            }
            else if (Input.GetKey("space") && !storyArray.nextIsTheEndOfOption(pointer) && !hasOptions)          //jei tereikia toliau eiti per istorija
            {
                pointer = storyArray.nextStory(pointer);
                Debug.Log(pointer.getPointer() + "\n" + pointer.getStory());
                StoryText.text = pointer.getStory();
                hasOptions = false;
                enableCamera(pointer.getPointer());
                wait(500);
                //StoryText.text = stage0StoryText.Length.ToString();
                //if (nextStage == true && stage0 == true)
                //{
                //    stage0 = false;
                //    stage1 = true;
                //    nextStage = false;
                //    ChangeCamera(HerosRoom);
                //}
                //else if (nextStage == true && stage1 == true)
                //{
                //    stage1 = false;
                //    stage2 = true;
                //    nextStage = false;
                //    HerosRoom.enabled = false;
                //    ChangeCamera(PrisonOutOfCapsule);
                //}
                //else if (nextStage == true && stage2 == true)
                //{
                //    stage2 = false;
                //    nextStage = false;
                //    PrisonOutOfCapsule.enabled = false;     //veliau pakeisti kitu
                //    //ijungti koridoriaus (?) kamera
                //    ChangeCamera(Corridor01GotOut);
                //}
                //else        //jei baigesi ciklas (testavimo tikslais)
                //{
                //    stage1 = false;
                //    stage2 = false;
                //    stage0 = true;
                //    PrisonOutOfCapsule.enabled = false;
                //    HerosRoom.enabled = false;
                //}
            }
            else if(Input.GetKey("space") && storyArray.nextIsTheEndOfOption(pointer))           //jei reikia parodyti options
            {
                enableCamera(pointer.getPointer());
                if(storyArray.nextHasOptions(pointer))
                {
                    choices = storyArray.lastTextAndOptions(pointer);
                    hasOptions = true;
                }
                else
                {
                    pointer = storyArray.lastTextWithoutOptions(pointer);
                    hasOptions = false;
                }

                //pakeisti mana ir health reiksmes
                manaText.text = storyArray.myMana.ToString();
                healthText.text = storyArray.myHealth.ToString();
                updateVariables();
                //jei nereikia daryti pasirinkimu
                if (!hasOptions)
                {
                    StoryText.text = pointer.getStory();
                    wait(500);
                }
                //jei reikia daryti pasirinkimus
                else
                {
                    StoryText.text = choices[0].getStory();
                    int x = howManyButtonsNeeded();
                    enableButtons(x);
                    wait(500);
                }
            }
            //else if (EventSystem.current.currentSelectedGameObject.name.Equals("choice1"))
            //{
            //    pointer.setPointer(storyArray.getStoryIndex(choices[1].getNextStage()));
            //    disableAllButtons();
            //    hasOptions = false;
            //}
            //else if (EventSystem.current.currentSelectedGameObject.name.Equals("choice2"))
            //{
            //    pointer.setPointer(storyArray.getStoryIndex(choices[2].getNextStage()));
            //    disableAllButtons();
            //    hasOptions = false;
            //}
            //else if (EventSystem.current.currentSelectedGameObject.name.Equals("choice3"))
            //{
            //    pointer.setPointer(storyArray.getStoryIndex(choices[3].getNextStage()));
            //    disableAllButtons();
            //    hasOptions = false;
            //}
            //else if (EventSystem.current.currentSelectedGameObject.name.Equals("choice4"))
            //{
            //    pointer.setPointer(storyArray.getStoryIndex(choices[4].getNextStage()));
            //    disableAllButtons();
            //    hasOptions = false;
            //}
            //else if (EventSystem.current.currentSelectedGameObject.name.Equals("choice5"))
            //{
            //    pointer.setPointer(storyArray.getStoryIndex(choices[5].getNextStage()));
            //    disableAllButtons();
            //    hasOptions = false;
            //}
            //else if (EventSystem.current.currentSelectedGameObject.name.Equals("choice6"))
            //{
            //    pointer.setPointer(storyArray.getStoryIndex(choices[6].getNextStage()));
            //    disableAllButtons();
            //    hasOptions = false;
            //}
            if (hasOptions && choiceOPtion != 0)
            {
                hasOptions = false;
                disableAllButtons();
                pointer.setPointer(storyArray.getStoryIndex(choices[choiceOPtion].getNextStage()));
                Debug.Log("Gavau ats is buttons:  einam i " + pointer.getPointer());
                choiceOPtion = 0;
            }
        }               
        else                //jei jau baigesi zaidimas
        {
            SceneManager.LoadScene(3);          //einama i score langa
        }
    }
    //options pasirinkimas
    //public void onClickChoice1()
    //{
    //    pointer.setPointer(storyArray.getStoryIndex(choices[1].getNextStage()));
    //    disableAllButtons();
    //    hasOptions = false;
    //}
    //public void onClickChoice2()
    //{
    //    pointer.setPointer(storyArray.getStoryIndex(choices[2].getNextStage()));
    //    disableAllButtons();
    //    hasOptions = false;
    //}
    //public void onClickChoice3()
    //{
    //    pointer.setPointer(storyArray.getStoryIndex(choices[3].getNextStage()));
    //    disableAllButtons();
    //    hasOptions = false;
    //}
    //public void onClickChoice4()
    //{
    //    pointer.setPointer(storyArray.getStoryIndex(choices[4].getNextStage()));
    //    disableAllButtons();
    //    hasOptions = false;
    //}
    //public void onClickChoice5()
    //{
    //    pointer.setPointer(storyArray.getStoryIndex(choices[5].getNextStage()));
    //    disableAllButtons();
    //    hasOptions = false;
    //}
    //public void onClickChoice6()
    //{
    //    pointer.setPointer(storyArray.getStoryIndex(choices[6].getNextStage()));
    //    disableAllButtons();
    //    hasOptions = false;
    //}
    //mygtuku inicializacija
    private int howManyButtonsNeeded()
    {
        int count = 0;
        for (int i = 1; i < choices.Length; i++)
            if (!choices[i].getStory().Equals(""))
                count++;
        return count;
    }
    private void enableButtons(int x)
    {
        if(x >= 1)
        {
            choice1.enabled = true;
            choice1.GetComponentInChildren<Text>().text = choices[1].getStory();
        }
        if (x >= 2)
        {
            choice2.enabled = true;
            choice2.GetComponentInChildren<Text>().text = choices[2].getStory();
        }
        if (x >= 3)
        {
            choice3.enabled = true;
            choice3.GetComponentInChildren<Text>().text = choices[3].getStory();
        }
        if (x >= 4)
        {
            choice4.enabled = true;
            choice4.GetComponentInChildren<Text>().text = choices[4].getStory();
        }
        if (x >= 5)
        {
            choice5.enabled = true;
            choice5.GetComponentInChildren<Text>().text = choices[5].getStory();
        }
        if (x >= 6)
        {
            choice6.enabled = true;
            choice6.GetComponentInChildren<Text>().text = choices[6].getStory();
        }
    }
    //isjungiame mygtukus (kai yra tik istorija)
    private void disableAllButtons()
    {
        choice1.enabled = false;
        choice1.GetComponentInChildren<Text>().text = "";
        choice2.enabled = false;
        choice2.GetComponentInChildren<Text>().text = "";
        choice3.enabled = false;
        choice3.GetComponentInChildren<Text>().text = "";
        choice4.enabled = false;
        choice4.GetComponentInChildren<Text>().text = "";
        choice5.enabled = false;
        choice5.GetComponentInChildren<Text>().text = "";
        choice6.enabled = false;
        choice6.GetComponentInChildren<Text>().text = "";
    }
    //isjungiame kameras
    private void disableAllCameras()
    {
        HerosRoom.enabled = false;
        PrisonOutOfCapsule.enabled = false;
        PrisonTalkToRobot.enabled = false;
        PrisonTV.enabled = false;
        Corridor01GotOut.enabled = false;
        Corridor02TurnOrRoom.enabled = false;
        Corridor03.enabled = false;
        Corridor04.enabled = false;
        Corridor05.enabled = false;
        Corridor06BrokenElevator.enabled = false;
        Corridor07SecondElevator.enabled = false;
        Corridor08Mystery.enabled = false;
        Corridor09Dirty.enabled = false;
        Corridor10FinalBattle.enabled = false;
        BurningRoom.enabled = false;
        LeakingGas.enabled = false;
        DirtyRoom2.enabled = false;
    }
    //ijungiame reikiama kamera
    private void enableCamera(int i)
    {
        Debug.Log("Keiciam kamera! Turiu: " + i);
        switch (i)
        {
            case 3:
                ChangeCamera(HerosRoom);
                Debug.Log("Esame herojaus kambaryje");
                Water.enableEmission = true;
                break;
            case 8:
                ChangeCamera(PrisonOutOfCapsule);
                Water.enableEmission = false;
                break;
            case 9:
                ChangeCamera(PrisonTV);
                break;
            case 15:
                ChangeCamera(PrisonTV);
                break;
            case 10:
                ChangeCamera(PrisonTV);
                break;
            case 12:
                ChangeCamera(PrisonTV);
                break;
            case 13:
                ChangeCamera(PrisonTV);
                break;
            case 14:
                ChangeCamera(PrisonTV);
                break;
            case 11:
                ChangeCamera(PrisonTalkToRobot);
                break;
            case 31:
                ChangeCamera(BurningRoom);
                Fire.enableEmission = true;
                break;
            case 32:
                ChangeCamera(BurningRoom);
                Fire.enableEmission = true;
                break;
            case 33:
                ChangeCamera(BurningRoom);
                Fire.enableEmission = true;
                break;
            case 34:
                ChangeCamera(BurningRoom);
                Fire.enableEmission = true;
                break;
            case 35:
                ChangeCamera(BurningRoom);
                Fire.enableEmission = true;
                break;
            case 50:
                ChangeCamera(BurningRoom);
                Fire.enableEmission = true;
                break;
            case 51:
                ChangeCamera(BurningRoom);
                Fire.enableEmission = true;
                break;
            //case 37:
            //    ChangeCamera()
            case 53:
                ChangeCamera(Corridor07SecondElevator);
                Fire.enableEmission = false;
                break;
            case 99:
                ChangeCamera(Corridor09Dirty);
                break;
            case 89:
                ChangeCamera(LeakingGas);
                LeakingGasWall.enableEmission = true;
                LeakingGasGround.enableEmission = true;
                break;
            case 90:
                ChangeCamera(LeakingGas);
                LeakingGasWall.enableEmission = true;
                LeakingGasGround.enableEmission = true;
                break;
            case 54:
                ChangeCamera(Corridor08Mystery);
                break;
            case 92:
                ChangeCamera(Corridor08Mystery);
                break;
            case 98:
                ChangeCamera(DirtyRoom2);
                break;
            default:
                disableAllCameras();
                break;
        }
    }
    //ijungiame ir i canvas idedame reikiama kamera
    private void ChangeCamera(Camera cam)
    {
        //cam = Camera.main;
        cam.enabled = true;
        cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
        //Camera.main.rect = new Rect(0.8f, 0.81f, 0.5f, 0.5f);

    }

    private void updateVariables()
    {
        PH = storyArray.myHealth;
        PM = storyArray.myMana;
        JoaH = storyArray.JoannaHealth;
        JoaM = storyArray.JoannaMana;
        JosH = storyArray.JoshHealth;
        JosM = storyArray.JoshMana;
        aggresiveness = storyArray.aggresiveness;
    }
    //private string askInput()
    //{
    //    GameObject go = GameObject.Find("GameManager");
    //    InputFieldScript input = go.GetComponent<InputFieldScript>();
    //    return input.answer;
    //}
}
