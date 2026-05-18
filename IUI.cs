using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Garage_Task
{
    internal interface IUI
    {
        static abstract void StartDisplay();
        static abstract bool LoopDisplay(bool looping, GarageHandler handler);
    }
}
