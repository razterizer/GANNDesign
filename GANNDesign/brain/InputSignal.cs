using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GANNDesign.brain
{
    public class InputSignal
    {
        public InputSignal(string _name, int _id, bool _active)
        {
            name = _name;
            id = _id;
            active = _active;
            live = true;
        }
        public string name;
        public int id;
        public bool active;
        public bool live;
    }
}
