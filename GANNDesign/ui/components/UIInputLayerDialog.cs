using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GANNDesign.brain;

namespace GANNDesign.ui.components
{
    public partial class UIInputLayerDialog : UIDialog
    {
        bool[] m_orig_checked_states;

        public UIInputLayerDialog()
        {
            InitializeComponent();
        }

        private void UIInputLayerDialog_Load(object sender, EventArgs e)
        {
            m_orig_checked_states = new bool[listViewSignals.Items.Count];
            for (int lvi_idx = 0; lvi_idx < listViewSignals.Items.Count; lvi_idx++)
                m_orig_checked_states[lvi_idx] = listViewSignals.Items[lvi_idx].Checked;
        }

        public void BeginUpdate()
        {
            listViewSignals.Items.Clear();
            listViewSignals.BeginUpdate();
        }
        public void EndUpdate()
        {
            listViewSignals.EndUpdate();
        }

        public void AddSignal(InputSignal signal)
        {
            ListViewItem lvi = listViewSignals.Items.Add(signal.name + (signal.id >= 0 ? " " + signal.id.ToString() : ""));
            lvi.Tag = signal;
            lvi.Checked = signal.active;
        }

        public void ClearSignals()
        {
            listViewSignals.Clear();
        }

        public void SetSignals()
        {
            foreach (ListViewItem lvi in listViewSignals.Items)
                ((InputSignal)lvi.Tag).active = lvi.Checked;
        }

        protected override void buttonCancel_Click(object sender, EventArgs e)
        {
            for (int lvi_idx = 0; lvi_idx < listViewSignals.Items.Count; lvi_idx++)
                listViewSignals.Items[lvi_idx].Checked = m_orig_checked_states[lvi_idx];

            base.buttonCancel_Click(sender, e);
        }
    }
}
