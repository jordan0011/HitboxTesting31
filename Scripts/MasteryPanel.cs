using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasteryPanel : MonoBehaviour
{
    public bool action1 = false;
    public int number = 0;
    public Text[] masteryCheck;
    public Text availablepoints;

    public int points;
    public int playermaxpoints;
    public int treemaxpoints = 25;

    public int startmastery = 0;
    public int lastmastery = 11;

    public int startpipe = 0;
    public int lastpipe = 8;

    public int required = 5;
    public int reqpos = 5;

    public MasteryBlock[] masterydatabase = new MasteryBlock[12];

    public MasteryBlock[] masteryTree1 = new MasteryBlock[12];

    public MasteryIconBlock[] masteryIcons = new MasteryIconBlock[12];

    public Image[] pipes = new Image[12];

    public GameObject[] masterybuttons = new GameObject[12];

    Color pipegrey;

    public MasteryInfoPanel masteryinfopanel;

    // Start is called before the first frame update
    void Start()
    {
        pipegrey = new Color(0.24f, 0.24f, 0.24f);
        int[] start1 = { };
        masterydatabase[0] = new MasteryBlock(0, start1, 1);
        masterydatabase[1] = new MasteryBlock(1, start1, 3);
        masterydatabase[2] = new MasteryBlock(2, start1, 4);
        int[] start = { 1 };
        masterydatabase[3] = new MasteryBlock(3, start, 5);
        int[] start3 = { 2 };
        masterydatabase[4] = new MasteryBlock(4, start3, 5);
        int[] start4 = { 0 };
        masterydatabase[5] = new MasteryBlock(5, start4, 5);
        int[] start5 = { 3 };
        masterydatabase[6] = new MasteryBlock(6, start5, 5);
        int[] start6 = { 5 };
        masterydatabase[7] = new MasteryBlock(7, start6, 1);
        int[] start7 = { 6 };
        masterydatabase[8] = new MasteryBlock(8, start7, 1);
        int[] start8 = { 4 };
        masterydatabase[9] = new MasteryBlock(9, start8, 2);
        int[] start9 = { 7 };
        masterydatabase[10] = new MasteryBlock(10, start9, 1);
        int[] start10 = { 8 };
        int[] start11 = { 9 };
        masterydatabase[11] = new MasteryBlock(11, start11, 1);

        for(int i=0; i< masteryIcons.Length; i++)
        {
            masteryIcons[i].SetState(0);
        }

        for(int i=0; i< masterybuttons.Length; i++)
        {
            if( masterybuttons[i] != null)
            {
                int a =i;
                a = MasteryDatabase.Instance.masterybuttons[i];

                //Debug.Log(a);
                masterybuttons[i].GetComponent<MasteryButton>().Set(this, a);
            }
        }

        //RefreshView();
        //RegisterActiveMasteries();

        StartCoroutine(DelayReset(0.02f));
    }
    public void LoadMasteries(bool[] array)
    { 
        ResetPoints();

        for(int i=0; i< array.Length; i++)
        {
            if (array[i])
            {
                int a = 0;
                if (i < MasteryDatabase.Instance.VirtualMasteries[MasteryDatabase.Instance.VirtualMasteries.Length - 1])
                {
                    while (i >= MasteryDatabase.Instance.VirtualMasteries[a + 1])
                    {
                        a++;
                    }
                    AddMasteryVolume(a);
                }
                else
                {
                    AddMasteryVolume(masteryTree1.Length - 1);
                }
            }
        }

        RegisterActiveMasteries();
        RefreshView();
    }
    public bool[] SaveMasteries() //server only please
    {
        bool[] array = new bool[34];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = false;

            int b = 0;

            while(i > MasteryDatabase.Instance.VirtualMasteries[b] + masteryTree1[b].GetMaxVolume() -1)
            {
                b++;
            }
            
            if(b < masteryTree1.Length)
            {
                int k = MasteryDatabase.Instance.VirtualMasteries[b];
                if (i >= k && i < k + masteryTree1[b].GetMaxVolume())
                {
                    if (i - k < masteryTree1[b].GetVolume())
                    {
                        array[i] = true;
                    }
                    else
                    {
                        array[i] = false;
                    }
                }
            }
        }

        return array;
    }
    public void EnterMastery(int n)
    {
        // Debug.Log("Mastery entered: " + MasteryDatabase.Instance.AllMasteries[masteryTree1[n].getId()].getName());
        int b = MasteryDatabase.Instance.masterybuttons[n];
        int a = MasteryDatabase.Instance.VirtualMasteries[b];
        a += masteryTree1[n].GetVolume();
        if (masteryTree1[n].GetVolume() > 0)
            a--;
        //Debug.Log(MasteryDatabase.Instance.VirtualMasteries[n] + ", " + a);
        masteryinfopanel.SetStart(MasteryDatabase.Instance.AllMasteries[a].getName(), MasteryDatabase.Instance.AllMasteries[a].getDescription());
        masteryinfopanel.gameObject.SetActive(true);
    }

    public void ExitMastery(int n)
    {
        masteryinfopanel.gameObject.SetActive(false);
       // Debug.Log("Mastery exited: " + n);
    }

    public void ClientCommand(int n)
    {
        AddMasteryVolume(n);
    }
    public void AddMasteryVolume(int n) // in server
    {
        MasteryBlock instance = masteryTree1[n];
        bool ready = false;

        for(int i=0; i< instance.GetPointers().Length; i++)
        {
            int pointer = instance.GetPointers()[i];
            MasteryBlock instance1 = masteryTree1[pointer];
           // if (instance1.GetVolume() == instance1.GetMaxVolume())  // instance = change
            if (instance1.GetVolume() >0)  // instance = change
                ready = true;

        }
        if(instance.GetPointers().Length == 0)
        {
            ready = true;
        }
        if(n >= 5)
        {
            if((playermaxpoints - points) < 5)
            {
                ready = false;
            }
        }

        if(instance.GetVolume() < instance.GetMaxVolume() && points >0&&ready)
        {
            masteryTree1[n].AddVolume();
            points--;
            
            RegisterActiveMasteries();
            RefreshView();
        }
        int b = MasteryDatabase.Instance.masterybuttons[n];
        int a = MasteryDatabase.Instance.VirtualMasteries[b];
        a += masteryTree1[n].GetVolume();
        if (masteryTree1[n].GetVolume() > 0)
            a--;
        masteryinfopanel.SetStart(MasteryDatabase.Instance.AllMasteries[a].getName(), MasteryDatabase.Instance.AllMasteries[a].getDescription());

    }
    public void ClientUpdate(int a, int b)
    {
        //client Add Mastery Volume
    }
    public void ResetPoints()
    {
        for(int i=0; i< masteryTree1.Length; i++)
        {
            masteryTree1[i].ResetVolume();
        }
        points = treemaxpoints;
        RegisterActiveMasteries();
        RefreshView();
    }
    public void RefreshView()
    {
        for (int i = 0; i < masteryCheck.Length; i++)
        {
            MasteryBlock instance = masterydatabase[i];

            masteryTree1[i] = instance;
            if(instance != null)
            masteryCheck[i].text = masteryTree1[i].GetVolume().ToString() + "/" + masteryTree1[i].GetMaxVolume().ToString();
        }
        RefreshMasteryIconBlocks();
        availablepoints.text = "Available Points: "+points.ToString();
    }
    public void RegisterActiveMasteries()
    {
       /* for(int i=0; i<bookMasteries.Length; i++)
        {
            bookMasteries[i] = null;
        }*/

        for(int i=0; i< masteryTree1.Length; i++)
        {
            if(masteryTree1[i] != null)
            {
                //Debug.Log(masteryTree1[i].getId());
                MasteryBlock mastery = masteryTree1[i];
               /* if (mastery.GetActive())
                {
                    //Debug.Log(i+  " "+ masteryTree1[i].getId());
                    int a = MasteryDatabase.Instance.VirtualMasteries[i];
                    Mastery temp = MasteryDatabase.Instance.AllMasteries[a + mastery.GetVolume()];
                    bookMasteries[i] = new Mastery(temp.getId(), temp.getIcon(), temp.getName(), temp.getDescription());
                }*/
            }
        }

       /* for(int i=0; i< bookMasteries.Length; i++)
        {
            if (bookMasteries[i] != null)
            {
               // Debug.Log(bookMasteries[i].getName()+ ", "+ bookMasteries[i].getDescription());

            }
        }*/

       

    }
    public void RefreshMasteryIconBlocks()
    {
        for (int i = 0; i < masteryIcons.Length; i++)
        {
            //masteryIcons[i].SetState(0);
            //Debug.Log(masteryTree1[i].GetVolume());
            if (masteryTree1[i] != null)
            {

                if (masteryTree1[i].GetVolume() > 0)
                {
                    masteryIcons[i].SetState(2);
                }
                else
                {
                    MasteryBlock instance = masteryTree1[i];
                    bool ready = false;

                    for (int h = 0; h < instance.GetPointers().Length; h++)
                    {
                        int pointer = instance.GetPointers()[h];
                        MasteryBlock instance1 = masteryTree1[pointer];
                        if (instance1.GetVolume() > 0)  // instance = change
                            ready = true;

                    }
                    if (instance.GetPointers().Length == 0)
                    {
                        ready = true;
                    }
                    if (i >= 5)
                    {
                        if ((playermaxpoints - points) < 5)
                        {
                            ready = false;
                        }
                    }
                    if (ready)
                    {
                        masteryIcons[i].SetState(1);
                    }
                    else
                    {
                        masteryIcons[i].SetState(0);
                    }
                }
            }
        }
        RefreshPipes();
    }
    public void RefreshPipes()
    {
        for(int i=0; i< lastpipe+1; i++)
        {

            bool lit = false;

            if(MasteryDatabase.Instance.pipes[i] != null )
            {

            int root = MasteryDatabase.Instance.pipes[i].getRoot();

                if (masteryTree1[root] != null)
                {
                    MasteryBlock instance = masteryTree1[root];

                    if (instance.GetVolume() > 0)
                    {
                        lit = true;
                    }
                    else
                    {
                        lit = false;
                    }

                    if (MasteryDatabase.Instance.allbookbarriers[number].getArray()[root] == 1)
                    {
                        if ((playermaxpoints - points) < required)
                        {
                            lit = false;
                        }
                    }


                    if (lit)
                    {
                        pipes[i].color = Color.white;
                    }
                    else
                    {
                        pipes[i].color = pipegrey;
                    }
                }
            }
        }
    }
    public void OpenClose()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    IEnumerator DelayReset(float t)
    {
        yield return new WaitForSeconds(t);
        RegisterActiveMasteries();
        RefreshView();
    }
}
public class MasteryBlock
{
    private int id;
    private int[] pointers;
    private int volume;
    private int maxVolume;
    private bool active;
    public MasteryBlock(int myid, int[] mypointers, int mymaxvolume)
    {
        id = myid;
        pointers = mypointers;
        volume = 0;
        maxVolume = mymaxvolume;
    }
    public int getId()
    {
        return id;
    }
    public int[] GetPointers()
    {
        return pointers;
    }
    public bool isActivatable()
    {
        return false;
    }
    public void isActive(bool myactive)
    {
        active = myactive;
    }
    public bool GetActive()
    {
        return active;
    }
    public int GetVolume()
    {
        return volume;
    }
    public int GetMaxVolume()
    {
        return maxVolume;
    }
    public void AddVolume()
    {
        if (volume < maxVolume)
        {
            volume++;
            if (volume > 0)
                active = true;
        }
    }
    public void ResetVolume()
    {
        volume = 0;
        active = false;
    }
}