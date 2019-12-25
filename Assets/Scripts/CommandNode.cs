using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandNode
{
    public string name;

    public CommandNode(string initname){
        name=initname;
    }

    public string GetName(){
        return name;
    }

}
