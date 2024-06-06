using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void Attach(IObserver observer);

    void Detack(IObserver observer);
    void Notify();
}
