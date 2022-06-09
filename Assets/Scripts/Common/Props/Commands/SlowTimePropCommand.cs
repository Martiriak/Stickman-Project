using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stickman.Managers;

namespace Stickman.Props.Commands
{
    public class SlowTimePropCommand : PropCommand
    {
        // Start is called before the first frame update
        public override void Execute()
        {
            GameManager.Instance.SpeedManager.SlowDown(30);
        }
    }
}
