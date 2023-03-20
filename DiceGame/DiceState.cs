using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceState
{
    public int value;

    // [Up, Down, Left, Right]
    public int[] values;

    // Indexes for the next states. Eg. when rolling up, the next state is the one under index upStateIndex.  
    public int upStateIndex;
    public int downStateIndex;
    public int leftStateIndex;
    public int rightStateIndex;

    public DiceState(int value, int[] values, int upStateIndex, int downStateIndex, int leftStateIndex, int rightStateIndex)
    {
        this.value = value;
        this.values = values;
        this.upStateIndex = upStateIndex;
        this.downStateIndex = downStateIndex;
        this.leftStateIndex = leftStateIndex;
        this.rightStateIndex = rightStateIndex;
    }
}

public class Dice
{
    private static List<DiceState> states;
    public int currentStateIndex;

    public Dice()
    {
        states = new List<DiceState>() {
            //1
            new DiceState(1, new int[] {4,3,2,5}, 9, 12, 18, 21),
            new DiceState(1, new int[] {3,4,5,2}, 14, 11, 23, 16),
            new DiceState(1, new int[] {2,5,3,4}, 19, 22, 13, 10),
            new DiceState(1, new int[] {5,2,4,3}, 20, 17, 8, 15),
            //6
            new DiceState(6, new int[] {3,4,2,5}, 12, 9, 16, 23),
            new DiceState(6, new int[] {2,5,4,3}, 17, 20, 10, 13),
            new DiceState(6, new int[] {4,3,5,2}, 11, 14, 21, 18),
            new DiceState(6, new int[] {5,2,3,4}, 22, 19, 15, 8),
            //4
            new DiceState(4, new int[] {5,2,6,1}, 23, 18, 7, 3),
            new DiceState(4, new int[] {6,1,2,5}, 4, 0, 19, 20),
            new DiceState(4, new int[] {2,5,1,6}, 16, 21, 2, 5),
            new DiceState(4, new int[] {1,6,5,2}, 1, 6, 22, 17),
            //3
            new DiceState(3, new int[] {1,6,2,5}, 0, 4, 17, 22),
            new DiceState(3, new int[] {2,5,6,1}, 18, 23, 5, 2),
            new DiceState(3, new int[] {6,1,5,2}, 6, 1, 20, 19),
            new DiceState(3, new int[] {5,2,1,6}, 21, 16, 3, 7),
            //2
            new DiceState(2, new int[] {3,4,1,6}, 15, 10, 1, 4),
            new DiceState(2, new int[] {1,6,4,3}, 3, 5, 11, 12),
            new DiceState(2, new int[] {4,3,6,1}, 8, 13, 6, 0),
            new DiceState(2, new int[] {6,1,3,4}, 7, 2, 14, 9),
            //5
            new DiceState(5, new int[] {6,1,4,3}, 5, 3, 9, 14),
            new DiceState(5, new int[] {4,3,1,6}, 10, 15, 0, 6),
            new DiceState(5, new int[] {1,6,3,4}, 2, 7, 12, 11),
            new DiceState(5, new int[] {3,4,6,1}, 13, 8, 4, 1),



        };
    }

    public int getIndex()
    {
        return currentStateIndex;
    }

    public int getValue()
    {
        return states[currentStateIndex].value;
    }

    public void RollUp()
    {
        currentStateIndex = states[currentStateIndex].upStateIndex;
    }

    public void RollDown()
    {
        currentStateIndex = states[currentStateIndex].downStateIndex;
    }

    public void RollLeft()
    {
        currentStateIndex = states[currentStateIndex].leftStateIndex;
    }

    public void RollRight()
    {
        currentStateIndex = states[currentStateIndex].rightStateIndex;
    }
}
