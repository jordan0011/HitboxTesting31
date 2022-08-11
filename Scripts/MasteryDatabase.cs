using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasteryDatabase : MonoBehaviour
{
    private static MasteryDatabase _instance;

    public static MasteryDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Mastery Database is null");
            }
            return _instance;
        }
    }
    public Mastery[] AllMasteries= new Mastery[34];

    public GameObject[] MasteryIcons = new GameObject[12];
    public GameObject EmptyIcon;

    public Pipe[] pipes = new Pipe[10];

    public BookBarriers[] allbookbarriers = new BookBarriers[10];

    public int[] VirtualMasteries = { 0, 1, 4, 8, 13, 18, 23, 28, 29, 30, 32, 33 };

    public int[] masterybuttons = new int[13];
    private void Awake()
    {
        _instance = this;

        pipes[0] = new Pipe(0, 0);
        pipes[1] = new Pipe(1, 1);
        pipes[2] = new Pipe(2, 2);
        pipes[3] = new Pipe(3, 3);
        pipes[4] = new Pipe(4, 4);
        pipes[5] = new Pipe(5, 5);
        pipes[6] = new Pipe(6, 6);
        pipes[7] = new Pipe(7, 7);
        pipes[8] = new Pipe(8, 9);

        int[] book0 = { 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 };
        allbookbarriers[0] = new BookBarriers(book0);

        masterybuttons[0] = 0;
        masterybuttons[1] = 1;
        masterybuttons[2] = 2;
        masterybuttons[3] = 4;
        masterybuttons[4] = 5;
        masterybuttons[5] = 3;
        masterybuttons[6] = 6;
        masterybuttons[7] = 8;
        masterybuttons[8] = 7;
        masterybuttons[9] = 9;
        masterybuttons[10] =10;
        masterybuttons[11] =11;
        masterybuttons[12] = 12;



    }

    // Start is called before the first frame update
    void Start()
    {
        

        string[] names = new string[34];
        names[0] = "Restore help";

        names[1] = "New Arrival Focus";
        names[2] = "New Arrival Focus 2/3";
        names[3] = "New Arrival Focus 3/3";

        names[4] = "Warrior Destiny";
        names[5] = "Warrior Destiny 2/4";
        names[6] = "Warrior Destiny 3/4";
        names[7] = "Warrior Destiny 4/4";

        names[8] = "Reckless Spirit";
        names[9] = "Reckless Spirit 2/5";
        names[10] = "Reckless Spirit 3/5";
        names[11] = "Reckless Spirit 4/5";
        names[12] = "Reckless Spirit 5/5";

        names[13] = "Weapon Sacrifice";
        names[14] = "Weapon Sacrifice 2/5";
        names[15] = "Weapon Sacrifice 3/5";
        names[16] = "Weapon Sacrifice 4/5";
        names[17] = "Weapon Sacrifice 5/5";

        names[18] = "Mana Technique";
        names[19] = "Mana Technique 2/5";
        names[20] = "Mana Technique 3/5";
        names[21] = "Mana Technique 4/5";
        names[22] = "Mana Technique 5/5";

        names[23] = "Warmed Up";
        names[24] = "Warmed Up 2/5";
        names[25] = "Warmed Up 3/5";
        names[26] = "Warmed Up 4/5";
        names[27] = "Warmed Up 5/5";

        names[28] = "Unexpected Storm";

        names[29] = "Lost Chapter";

        names[30] = "Work Better Alone";
        names[31] = "Work Better Alone 2/2";

        names[32] = "Adaptive Shield";

        names[33] = "Determination";

        string[] descriptions = new string[34];
        descriptions[0] = "Restores (+20) health after you kill a unit.";

        descriptions[1] = "Deal (8) damage when you hit a new target.  (Resets on target death, 20s cooldown)";
        descriptions[2] = "Deal (16) damage when you hit a new target.  (Resets on target death, 20s cooldown)";
        descriptions[3] = "Deal (24) damage when you hit a new target.  (Resets on target death, 20s cooldown)";

        descriptions[4] = "+1% damage of all sources.";
        descriptions[5] = "+2% damage of all sources.";
        descriptions[6] = "+3% damage of all sources.";
        descriptions[7] = "+5% damage of all sources.";

        descriptions[8] = "Recieve (+7)% more damage overall and deal (+5)%.";
        descriptions[9] = "Recieve (+8)% more damage overall and deal (+6)%.";
        descriptions[10] = "Recieve (+9)% more damage overall and deal (+7)%.";
        descriptions[11] = "Recieve (+10)% more damage overall and deal (+8)%.";
        descriptions[12] = "Recieve (+12)% more damage overall and deal (+10)%.";

        descriptions[13] = "If you get shot over (12)% of your current health ignore (10) damage.";
        descriptions[14] = "If you get shot over (24)% of your current health ignore (20) damage.";
        descriptions[15] = "If you get shot over (36)% of your current health ignore (30) damage.";
        descriptions[16] = "If you get shot over (48)% of your current health ignore (40) damage.";
        descriptions[17] = "If you get shot over (60)% of your current health ignore (50) damage.";

        descriptions[18] = "(+2)% base mana.";
        descriptions[19] = "(+4)% base mana.";
        descriptions[20] = "(+6)% base mana.";
        descriptions[21] = "(+8)% base mana.";
        descriptions[22] = "(+10)% base mana.";

        descriptions[23] = "Each other consecutive attack increases your critical strike chance by 1%.";
        descriptions[24] = "Each other consecutive attack increases your critical strike chance by 2%.";
        descriptions[25] = "Each other consecutive attack increases your critical strike chance by 3%.";
        descriptions[26] = "Each other consecutive attack increases your critical strike chance by 4%.";
        descriptions[27] = "Each other consecutive attack increases your critical strike chance by 5%.";

        descriptions[28] = "Spells with projectiles deal (+25)% more damage.";

        descriptions[29] = "Unlock -Arcane Burst- Spell.";

        descriptions[30] = "When you kill an enemy without any help restore health.  (10%) for player kills, (3%) for mobs";
        descriptions[31] = "When you kill an enemy without any help restore health.  (20%) for player kills, (5%) for mobs";

        descriptions[32] = "When you get hit by an enemy gain (+1)% armor, (stacks up to 6 times, max: (6)%";

        descriptions[33] = "When you damage a target gain +8% damage.   (10s cooldown)";


        for (int i = 0; i < 34; i++)
        {
            int x = 0;
            for(int h= 0; h< VirtualMasteries.Length; h++)
            {
                if (VirtualMasteries[h] > i)
                {
                    x = h - 1;
                    break;
                }
            }
            if (MasteryIcons[x] != null)
            {
                GameObject temp = MasteryIcons[x];
                AllMasteries[i] = new Mastery(i, temp, names[i], descriptions[i]);
                 //Debug.Log(AllMasteries[i].getName() + " Spawned");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class Mastery {
    private int id;
    private GameObject icon;
    private string name;
    private string description;
    public Mastery(int myid, GameObject myicon, string myname, string mydescription)
    {
        id = myid;
        icon = myicon;
        name = myname;
        description = mydescription;
    }
    public int getId()
    {
        return id;
    }
    public GameObject getIcon()
    {
        return icon;
    }
    public string getName()
    {
        return name;
    }
    public string getDescription()
    {
        return description;
    }
}
public class Pipe
{
    private int id;
    private int root;
    public Pipe(int myid, int myroot)
    {
        id = myid;
        root = myroot;
    }
    public int getID()
    {
        return id;
    }
    public int getRoot()
    {
        return root;
    }
}
public class BookBarriers
{
    private int[] array;
    public BookBarriers(int[] myarray)
    {
        array = myarray;
    }
    public int[] getArray()
    {
        return array;
    }
}
