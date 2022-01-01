using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CheckBoxObserver
{
    public static void OnClick(GameObject checkbox)
    {
        CheckBoxController controller = checkbox.GetComponent<CheckBoxController>();
        controller.OnClick();
        if (!controller.Checked)
        {
            if (checkbox.name == "ckbTutorial") Options.Tutorial = true;
            else if (checkbox.name == "ckbDontShow") Options.Tutorial = false;
            else if (checkbox.name == "ckbSound")
            {
                Options.Sound = true;
                Configuration.SetSounds();
            }
        }
        else
        {
            if (checkbox.name == "ckbTutorial") Options.Tutorial = false;
            else if (checkbox.name == "ckbDontShow") Options.Tutorial = true;
            else if (checkbox.name == "ckbSound")
            {
                Options.Sound = false;
                Configuration.SetSounds();
            }
        }
    }
}
