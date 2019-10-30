using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    private Singleton singleton;
    protected Singleton Instance
    {
        get 
        {
            if(singleton == null)
            {
                singleton = new Singleton();
            }

            return singleton;
        }
    }

    protected virtual void Initialize()
    {

    }
}
