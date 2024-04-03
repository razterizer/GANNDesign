using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using GANNDesign.brain;

namespace GANNDesign.ui.components
{
    class UIInputLayer : UILayer
    {
        UIInputLayerDialog m_dialog;
        List<InputSignal> m_signals;

        public UIInputLayer(Rectangle layer_box_bounds)
            : base(layer_box_bounds, "Input Layer", Brushes.AliceBlue)
        {
            m_dialog = new UIInputLayerDialog();
            m_signals = new List<InputSignal>();
        }

        public void BeginUpdate()
        {
            m_dialog.BeginUpdate();

            foreach (InputSignal s in m_signals)
                s.live = false;
        }

        public void EndUpdate()
        {
            // Clean up.
            bool dead_found;
            do
            {
                dead_found = false;
                foreach (InputSignal s in m_signals)
                    if (!s.live)
                    {
                        m_signals.Remove(s);
                        dead_found = true;
                        break;
                    }
            } while (dead_found);

            update_num_neurons();


            foreach (InputSignal signal in m_signals)
                m_dialog.AddSignal(signal);

            m_dialog.EndUpdate();
        }

        public void AddSignal(string name, int id)
        {
            foreach (InputSignal s in m_signals)
                if (s.name == name && s.id == id)
                {
                    s.live = true;
                    return;
                }
            
            InputSignal signal = new InputSignal(name, id, true);
            m_signals.Add(signal);
        }

        public void AddSignal(InputSignal signal)
        {
            m_signals.Add(signal);
        }

        public void ClearSignals()
        {
            m_signals.Clear();
            m_dialog.ClearSignals();
        }

        public override bool MouseUp(Point p, IWin32Window owner)
        {
            bool point_in_layer_box = false;
            if (base.MouseUp(p, owner))
            {
                point_in_layer_box = true;

                if (DialogResult.OK == m_dialog.ShowDialog(owner))
                {
                    m_dialog.SetSignals();
                    update_num_neurons();
                }
            }
            return point_in_layer_box;
        }

        public IEnumerable<InputSignal> InputSignals
        {
            get { return m_signals; }
        }

        private void update_num_neurons()
        {
            m_num_neurons = 0;
            foreach (InputSignal signal in m_signals)
                if (signal.active)
                    m_num_neurons++;
        }
    }
}
