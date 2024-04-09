using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GANNDesign.ui.components;
using GANNDesign.body;
using GANNDesign.brain;
using System.Xml;
using MathLib.utils;

namespace GANNDesign
{
    public partial class Form1 : Form
    {
        UIPalette m_palette;
        List<Limb> m_limbs;
        List<Joint> m_joints;

        bool m_palette_mode;
        Point m_start_pt, m_end_pt;
        bool m_is_drawing;
        Point m_motion_pt;
        Limb m_closest_limb;
        List<Limb> m_angular_spring_limbs;
        Joint m_closest_joint;
        Joint m_selected_joint;
        AngularSpring m_closest_angular_spring;

        UIInputLayer m_input_layer;
        UIOutputLayer m_output_layer;
        List<UIHiddenLayer> m_hidden_layers;

        public Form1()
        {
            InitializeComponent();

            m_palette = new UIPalette(Point.Empty);

            m_limbs = new List<Limb>();
            m_joints = new List<Joint>();
            m_palette_mode = true;
            m_is_drawing = false;
            m_closest_limb = null;
            m_angular_spring_limbs = new List<Limb>();
            m_closest_joint = null;
            m_selected_joint = null;
            m_closest_angular_spring = null;

            m_input_layer = new UIInputLayer(new Rectangle(200, 10, 200, 20));
            m_output_layer = new UIOutputLayer(new Rectangle(200, canvasBrainDesign.Height - 30, 200, 20));
            m_hidden_layers = new List<UIHiddenLayer>();
            for (int layer_idx = 0; layer_idx < 6; layer_idx++)
                m_hidden_layers.Add(new UIHiddenLayer(new Rectangle(200, 55 + 43 * layer_idx, 200, 20)));
        }

        private void canvasBodyDesign_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.Clear(Color.WhiteSmoke);

            draw_grid(e.Graphics);
            
            m_palette.Draw(e.Graphics);

            foreach (Joint joint in m_joints)
                joint.DrawAngularSprings(e.Graphics, m_closest_angular_spring);

            Color limb_color_normal = Color.Peru;
            Color limb_color_muscle = Color.DeepPink;
            Pen limb_pen = new Pen(limb_color_normal, 2.0f);
            Pen hovered_limb_pen = new Pen(limb_color_normal, 3.0f);
            Pen selected_limb_pen = new Pen(Color.DarkSlateGray, 2.0f);
            foreach (Limb limb in m_limbs)
            {
                limb_pen.Color = limb.IsMuscle ? limb_color_muscle : limb_color_normal;
                hovered_limb_pen.Color = limb.IsMuscle ? limb_color_muscle : limb_color_normal;
                Pen curr_pen = limb_pen;
                if (m_palette.ActiveButtonType == UIButton.ButtonType.AngularSpring)
                {
                    if (m_angular_spring_limbs.Count >= 1 && m_angular_spring_limbs[0] == limb)
                    {
                        curr_pen = selected_limb_pen;
                    }
                    else if (m_closest_limb == limb)
                    {
                        curr_pen = hovered_limb_pen;
                    }
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Remove)
                {
                    if (m_closest_limb == limb)
                    {
                        curr_pen = hovered_limb_pen;
                    }
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Configure)
                {
                    if (m_closest_limb == limb)
                    {
                        curr_pen = hovered_limb_pen;
                    }
                }
                e.Graphics.DrawLine(curr_pen, limb.JointA.Position, limb.JointB.Position);
            }

            Color joint_color_hub = limb_color_normal;
            Color joint_color_sensor = Color.DarkSlateBlue;
            Color joint_color_head = Color.DarkGreen;
            SolidBrush joint_brush = (SolidBrush)Brushes.SlateBlue.Clone();
            foreach (Joint joint in m_joints)
            {
                joint_brush.Color = joint.IsHead ? joint_color_head : 
                    (joint.HasContactSensor ? joint_color_sensor : joint_color_hub);
                if (m_palette.ActiveButtonType == UIButton.ButtonType.Move && 
                    (m_closest_joint == joint || m_selected_joint == joint))
                {
                    e.Graphics.FillEllipse(joint_brush, joint.Position.X - 5, joint.Position.Y - 5, 10, 10);
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Remove &&
                         m_closest_joint == joint)
                {
                    e.Graphics.FillEllipse(joint_brush, joint.Position.X - 5, joint.Position.Y - 5, 10, 10);
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Configure &&
                         m_closest_joint == joint)
                {
                    e.Graphics.FillEllipse(joint_brush, joint.Position.X - 5, joint.Position.Y - 5, 10, 10);
                }
                else
                    e.Graphics.FillEllipse(joint_brush, joint.Position.X - 3, joint.Position.Y - 3, 6, 6);
            }

            if (m_is_drawing)
            {
                Pen drawing_pen = new Pen(Color.Crimson, 1.0f);
                drawing_pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                drawing_pen.DashPattern = new float[] { 4.0f, 4.0f };
                e.Graphics.DrawLine(drawing_pen, m_start_pt, m_motion_pt);
            }
        }

        private static void draw_grid(Graphics g)
        {
            for (int x_idx = 0; x_idx < 80; x_idx++)
                for (int y_idx = 0; y_idx < 60; y_idx++)
                    g.FillEllipse(Brushes.Silver, (float)x_idx / 60 * 480 - 0.5f, (float)y_idx / 60 * 480 - 0.5f, 2, 2);
        }

        private void canvasBodyDesign_MouseDown(object sender, MouseEventArgs e)
        {
            if (!m_palette_mode)
            {
                if (m_palette.ActiveButtonType == UIButton.ButtonType.LinearSpring)
                {
                    m_start_pt = e.Location;
                    snap_point(ref m_start_pt);
                    
                    m_is_drawing = true;
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.AngularSpring)
                {
                    if (m_closest_limb != null &&
                        (m_angular_spring_limbs.Count == 0 || m_angular_spring_limbs[0] != m_closest_limb))
                    {
                        m_angular_spring_limbs.Add(m_closest_limb);
                    }
                    else
                    {
                        m_angular_spring_limbs.Clear();
                    }
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Move)
                {
                    //m_start_pt = e.Location;
                    //snap_point(ref m_start_pt);
                    m_selected_joint = m_closest_joint;
                }
            }

            m_palette.MouseDown(e.Location);
            toolTipData.Hide(this);
        }

        private void canvasBodyDesign_MouseUp(object sender, MouseEventArgs e)
        {
            bool command_attempt = false;

            if (!m_palette_mode)
            {
                if (m_palette.ActiveButtonType == UIButton.ButtonType.LinearSpring)
                {
                    m_end_pt = e.Location;
                    snap_point(ref m_end_pt);

                    m_is_drawing = false;

                    if (m_end_pt != m_start_pt)
                    {
                        command_attempt = true;

                        Joint joint_A = null;
                        foreach (Joint joint in m_joints)
                            if (joint.Position == m_start_pt)
                            {
                                joint_A = joint;
                                break;
                            }
                        if (joint_A == null)
                        {
                            joint_A = new Joint(m_start_pt);
                            m_joints.Add(joint_A);
                        }

                        Joint joint_B = null;
                        foreach (Joint joint in m_joints)
                            if (joint.Position == m_end_pt)
                            {
                                joint_B = joint;
                                break;
                            }
                        if (joint_B == null)
                        {
                            joint_B = new Joint(m_end_pt);
                            m_joints.Add(joint_B);
                        }

                        bool limb_exists = false;
                        foreach (Limb limb in m_limbs)
                            if ((limb.JointA == joint_A && limb.JointB == joint_B) ||
                                (limb.JointA == joint_B && limb.JointB == joint_A))
                            {
                                limb_exists = true;
                                break;
                            }
                        if (!limb_exists)
                        {
                            Limb limb_new = new Limb(joint_A, joint_B);
                            m_limbs.Add(limb_new);
                        }

                        Console.WriteLine("# limbs = " + m_limbs.Count);
                        Console.WriteLine("# joints = " + m_joints.Count);
                    }
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.AngularSpring)
                {
                    if (m_angular_spring_limbs.Count == 2)
                    {
                        command_attempt = true;

                        Limb limb1 = m_angular_spring_limbs[0];
                        Limb limb2 = m_angular_spring_limbs[1];
                        if (limb1.JointA == limb2.JointA ||
                            limb1.JointA == limb2.JointB)
                        {
                            limb1.JointA.AddAngularSpring(
                                new AngularSpring(limb1.JointA, limb1, limb2));
                            canvasBodyDesign.Refresh();
                        }
                        else if (limb1.JointB == limb2.JointA ||
                                 limb1.JointB == limb2.JointB)
                        {
                            limb1.JointB.AddAngularSpring(
                                new AngularSpring(limb1.JointB, limb1, limb2));
                            canvasBodyDesign.Refresh();
                        }
                        m_angular_spring_limbs.Clear();
                    }
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Move)
                {
                    if (m_selected_joint != null)
                    {
                        command_attempt = true;
                        m_selected_joint = null;
                    }
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Remove)
                {
                    if (m_closest_joint != null)
                    {
                        command_attempt = true;

                        // Remove connecting limbs.
                        bool found_connecting_limb;
                        do
                        {
                            found_connecting_limb = false;
                            foreach (Limb limb in m_limbs)
                                if (limb.JointA == m_closest_joint ||
                                    limb.JointB == m_closest_joint)
                                {
                                    // Remove references to these limbs 
                                    // from joints that connnect to them.
                                    if (limb.JointA == m_closest_joint)
                                        limb.JointB.RemoveLimb(limb);
                                    else if (limb.JointB == m_closest_joint)
                                        limb.JointA.RemoveLimb(limb);
                                    m_limbs.Remove(limb);
                                    found_connecting_limb = true;
                                    break;
                                }
                        } while (found_connecting_limb);
                        
                        // Remove the joint.
                        m_joints.Remove(m_closest_joint);
                        
                        // Clean up.
                        bool found_empty_joint;
                        do
                        {
                            found_empty_joint = false;
                            foreach (Joint joint in m_joints)
                                if (joint.NumLimbs == 0)
                                {
                                    m_joints.Remove(joint);
                                    found_empty_joint = true;
                                    break;
                                }
                        } while (found_empty_joint);
                    }
                    else if (m_closest_limb != null)
                    {
                        command_attempt = true;

                        // Remove references of this limb from the joints.
                        m_closest_limb.JointA.RemoveLimb(m_closest_limb);
                        m_closest_limb.JointB.RemoveLimb(m_closest_limb);

                        // Remove the limb itself.
                        m_limbs.Remove(m_closest_limb);

                        // Clean up.
                        bool found_empty_joint;
                        do
                        {
                            found_empty_joint = false;
                            foreach (Joint joint in m_joints)
                                if (joint.NumLimbs == 0)
                                {
                                    m_joints.Remove(joint);
                                    found_empty_joint = true;
                                    break;
                                }
                        } while (found_empty_joint);
                    }
                    else if (m_closest_angular_spring != null)
                    {
                        command_attempt = true;

                        m_closest_angular_spring.Joint.RemoveAngularSpring(m_closest_angular_spring);
                    }
                }
                else if (m_palette.ActiveButtonType == UIButton.ButtonType.Configure)
                {
                    if (m_closest_joint != null)
                    {
                        command_attempt = true;

                        UIBodyDialogJoint diag = new UIBodyDialogJoint();
                        diag.Mass = m_closest_joint.Mass;
                        diag.HasContactSensor = m_closest_joint.HasContactSensor;
                        diag.IsHead = m_closest_joint.IsHead;
                        if (DialogResult.OK == diag.ShowDialog(this))
                        {
                            m_closest_joint.Mass = diag.Mass;
                            m_closest_joint.HasContactSensor = diag.HasContactSensor;
                            m_closest_joint.IsHead = diag.IsHead;
                        }
                    }
                    else if (m_closest_limb != null)
                    {
                        command_attempt = true;

                        UIBodyDialogLinearSpring diag = new UIBodyDialogLinearSpring();
                        diag.Stiffness = m_closest_limb.Stiffness;
                        diag.Damping = m_closest_limb.Damping;
                        diag.IsMuscle = m_closest_limb.IsMuscle;
                        diag.MuscleGain = m_closest_limb.MuscleGain;
                        if (DialogResult.OK == diag.ShowDialog(this))
                        {
                            m_closest_limb.Stiffness = diag.Stiffness;
                            m_closest_limb.Damping = diag.Damping;
                            m_closest_limb.IsMuscle = diag.IsMuscle;
                            m_closest_limb.MuscleGain = diag.MuscleGain;
                        }
                    }
                    else if (m_closest_angular_spring != null)
                    {
                        command_attempt = true;

                        UIBodyDialogAngularSpring diag = new UIBodyDialogAngularSpring();
                        diag.Stiffness = m_closest_angular_spring.Stiffness;
                        diag.Damping = m_closest_angular_spring.Damping;
                        diag.IsMuscle = m_closest_angular_spring.IsMuscle;
                        diag.MuscleGain = m_closest_angular_spring.MuscleGain;
                        if (DialogResult.OK == diag.ShowDialog(this))
                        {
                            m_closest_angular_spring.Stiffness = diag.Stiffness;
                            m_closest_angular_spring.Damping = diag.Damping;
                            m_closest_angular_spring.IsMuscle = diag.IsMuscle;
                            m_closest_angular_spring.MuscleGain = diag.MuscleGain;
                        }
                    }
                }
            }

            if (command_attempt)
                canvasBodyDesign.Refresh();
            else if (m_angular_spring_limbs.Count == 0)
            {
                m_palette.MouseUp(e.Location);
                canvasBodyDesign.Refresh();
            }
        }

        private void canvasBodyDesign_MouseMove(object sender, MouseEventArgs e)
        {
            m_palette_mode = m_palette.Contains(e.Location);
            if (m_palette_mode || m_palette.ActiveButton == null)
                this.Cursor = Cursors.Default;
            else if (m_palette.ActiveButtonType == UIButton.ButtonType.LinearSpring)
                this.Cursor = Cursors.Cross;
            else if (m_palette.ActiveButtonType == UIButton.ButtonType.AngularSpring)
            {
                m_closest_limb = find_closest_limb(e.Location);
                if (m_closest_limb != null)
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;
                canvasBodyDesign.Refresh();
            }
            else if (m_palette.ActiveButtonType == UIButton.ButtonType.Move)
            {
                m_closest_joint = find_closest_joint(e.Location);
                if (m_closest_joint != null)
                    this.Cursor = Cursors.SizeAll;
                else
                    this.Cursor = Cursors.Default;

                if (m_selected_joint != null)
                {
                    m_end_pt = e.Location;
                    snap_point(ref m_end_pt);

                    if (m_selected_joint != null)
                        m_selected_joint.Position = m_end_pt;

                    this.Cursor = Cursors.SizeAll;
                }

                canvasBodyDesign.Refresh();
            }
            else if (m_palette.ActiveButtonType == UIButton.ButtonType.Remove)
            {
                m_closest_joint = null;
                m_closest_limb = null;
                m_closest_angular_spring = null;
                m_closest_joint = find_closest_joint(e.Location);
                if (m_closest_joint == null)
                    m_closest_limb = find_closest_limb(e.Location);
                if (m_closest_limb == null)
                    m_closest_angular_spring = find_closest_angular_spring(e.Location);
                if (m_closest_limb != null || m_closest_joint != null || m_closest_angular_spring != null)
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;

                canvasBodyDesign.Refresh();
            }
            else if (m_palette.ActiveButtonType == UIButton.ButtonType.Configure)
            {
                m_closest_joint = null;
                m_closest_limb = null;
                m_closest_angular_spring = null;
                m_closest_joint = find_closest_joint(e.Location);
                if (m_closest_joint == null)
                    m_closest_limb = find_closest_limb(e.Location);
                if (m_closest_limb == null)
                    m_closest_angular_spring = find_closest_angular_spring(e.Location);
                if (m_closest_limb != null || m_closest_joint != null || m_closest_angular_spring != null)
                    this.Cursor = Cursors.Hand;
                else
                    this.Cursor = Cursors.Default;

                if (m_closest_joint != null)
                    toolTipData.Show("ID = " + m_closest_joint.ID + "\nm = " + m_closest_joint.Mass + "\nSensor: " + m_closest_joint.HasContactSensor + "\nHead: " + m_closest_joint.IsHead, this, e.Location.X, e.Location.Y - 20);
                else if (m_closest_limb != null)
                    toolTipData.Show("ID = " + m_closest_limb.ID + "\nk = " + m_closest_limb.Stiffness + "\nc = " + m_closest_limb.Damping + "\nL_0 = " + m_closest_limb.RestLength + " [px]\nMuscle: " + m_closest_limb.IsMuscle.ToString(), this, e.Location.X, e.Location.Y - 40);
                else if (m_closest_angular_spring != null)
                    toolTipData.Show("ID = " + m_closest_angular_spring.ID + "\nk = " + m_closest_angular_spring.Stiffness + "\nc = " + m_closest_angular_spring.Damping + "\nalpha_0 = " + m_closest_angular_spring.RestAngle * 180.0f / (float)Math.PI + " [deg]\nMuscle: " + m_closest_angular_spring.IsMuscle, this, e.Location.X, e.Location.Y - 40);
                else
                    toolTipData.Hide(this);

                canvasBodyDesign.Refresh();
            }

            if (m_is_drawing)
            {
                m_motion_pt = e.Location;
                snap_point(ref m_motion_pt);
                canvasBodyDesign.Refresh();
            }
        }

        private void canvasBodyDesign_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            //m_closest_joint = null;
            //m_closest_limb = null;
            //m_closest_angular_spring = null;
            //m_is_drawing = false;
        }

        private void snap_point(ref Point p)
        {
            p.X = (int)Math.Round((float)p.X / 8.0f) * 8;
            p.Y = (int)Math.Round((float)p.Y / 8.0f) * 8;
        }

        private Limb find_closest_limb(Point p)
        {
            foreach (Limb limb in m_limbs)
                if (UtilsPointF.PointLineSegDistanceSquared(p, limb.JointA.Position, limb.JointB.Position) < 9.0f)
                    return limb;
            return null;
        }

        private Joint find_closest_joint(Point p)
        {
            foreach (Joint joint in m_joints)
                if (UtilsPointF.DistanceSquared(p, joint.Position) < 9.0f)
                    return joint;
            return null;
        }

        private AngularSpring find_closest_angular_spring(Point p)
        {
            foreach (Joint joint in m_joints)
            {
                AngularSpring spring = joint.FindClosestAngularSpring(p);
                if (spring != null)
                    return spring;
            }
            return null;
        }

        // ----------------- BRAIN --------------------

        private void canvasBrainDesign_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.Clear(Color.WhiteSmoke);

            //draw_grid(e.Graphics);

            m_input_layer.Draw(e.Graphics);
            foreach (UIHiddenLayer hidden_layer in m_hidden_layers)
                hidden_layer.Draw(e.Graphics);
            m_output_layer.Draw(e.Graphics);

            draw_layer_arrows(e.Graphics);

            e.Graphics.DrawString("Enable Hidden Layer:", SystemFonts.DefaultFont, Brushes.Black, 70, 30);
            e.Graphics.DrawString("Recurrent:", SystemFonts.DefaultFont, Brushes.Black, 430, 30);

            int num_neurons = 0;
            num_neurons += m_input_layer.NumNeurons;
            foreach (UIHiddenLayer hidden_layer in m_hidden_layers)
                num_neurons += hidden_layer.NumNeurons;
            num_neurons += m_output_layer.NumNeurons;
            e.Graphics.DrawString("# Neurons = " + num_neurons, SystemFonts.DefaultFont, Brushes.Black, 20, canvasBrainDesign.Height - 20);
        }

        private void draw_layer_arrows(Graphics g)
        {
            UIArrow arrow = new UIArrow(30, 10, 15);
            int last_active_hidden_layer = -1;
            for (int layer_idx = 0; layer_idx < m_hidden_layers.Count; layer_idx++)
                if (m_hidden_layers[layer_idx].Active)
                {
                    last_active_hidden_layer = layer_idx;
                    arrow.Draw(g,
                        m_input_layer.LayerBoxBounds,
                        m_hidden_layers[layer_idx].LayerBoxBounds);
                    break;
                }
            for (int layer1_idx = Math.Max(0, last_active_hidden_layer); layer1_idx < m_hidden_layers.Count; layer1_idx++)
            {
                if (m_hidden_layers[layer1_idx].Active)
                {
                    for (int layer2_idx = layer1_idx + 1; layer2_idx < m_hidden_layers.Count; layer2_idx++)
                    {
                        if (m_hidden_layers[layer2_idx].Active)
                        {
                            arrow.Draw(g,
                                m_hidden_layers[layer1_idx].LayerBoxBounds,
                                m_hidden_layers[layer2_idx].LayerBoxBounds);
                            last_active_hidden_layer = layer2_idx;
                            layer1_idx = last_active_hidden_layer - 1;
                            break;
                        }
                    }
                }
            }

            if (last_active_hidden_layer != -1)
                arrow.Draw(g,
                    m_hidden_layers[last_active_hidden_layer].LayerBoxBounds,
                    m_output_layer.LayerBoxBounds);
            else
                arrow.Draw(g,
                    m_input_layer.LayerBoxBounds,
                    m_output_layer.LayerBoxBounds);
        }

        private void canvasBrainDesign_MouseDown(object sender, MouseEventArgs e)
        {
            m_input_layer.MouseDown(e.Location);
            m_output_layer.MouseDown(e.Location);
            foreach (UIHiddenLayer hidden_layer in m_hidden_layers)
                hidden_layer.MouseDown(e.Location);
        }

        private void canvasBrainDesign_MouseUp(object sender, MouseEventArgs e)
        {
            m_input_layer.MouseUp(e.Location, this);
            m_output_layer.MouseUp(e.Location, this);
            foreach (UIHiddenLayer hidden_layer in m_hidden_layers)
                hidden_layer.MouseUp(e.Location, this);
            canvasBrainDesign.Refresh();
        }

        private void canvasBrainDesign_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            if (m_input_layer.MouseMove(e.Location))
                this.Cursor = Cursors.Hand;
            if (m_output_layer.MouseMove(e.Location))
                this.Cursor = Cursors.Hand;
            foreach (UIHiddenLayer hidden_layer in m_hidden_layers)
                if (hidden_layer.MouseMove(e.Location))
                    this.Cursor = Cursors.Hand;
        }

        private void canvasBrainDesign_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void tabPageBrain_Enter(object sender, EventArgs e)
        {
            update_brain();
        }

        private void update_brain()
        {
            // Input layer.

            m_input_layer.BeginUpdate();
            m_input_layer.AddSignal("centroid x position", -1);
            m_input_layer.AddSignal("centroid y position", -1);
            m_input_layer.AddSignal("centroid x velocity", -1);
            m_input_layer.AddSignal("centroid y velocity", -1);
            m_input_layer.AddSignal("mean angle", -1);
            m_input_layer.AddSignal("mean ang.vel", -1);
            foreach (Joint joint in m_joints)
                if (joint.HasContactSensor)
                    m_input_layer.AddSignal("joint contact", joint.ID);
            foreach (Limb limb in m_limbs)
                if (limb.IsMuscle)
                {
                    m_input_layer.AddSignal("linear muscle length", limb.ID);
                    m_input_layer.AddSignal("linear muscle velocity", limb.ID);
                }
            foreach (Joint joint in m_joints)
                foreach (AngularSpring spring in joint.AngularSprings)
                    if (spring.IsMuscle)
                    {
                        m_input_layer.AddSignal("angular muscle angle", spring.ID);
                        m_input_layer.AddSignal("angular muscle ang.vel.", spring.ID);
                    }
            m_input_layer.EndUpdate();

            // Output layer.

            m_output_layer.NumNeurons = 0;
            foreach (Limb limb in m_limbs)
                if (limb.IsMuscle)
                    m_output_layer.NumNeurons++;
            foreach (Joint joint in m_joints)
                foreach (AngularSpring spring in joint.AngularSprings)
                    if (spring.IsMuscle)
                        m_output_layer.NumNeurons++;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFileDialog1.ShowDialog(this))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root_node = doc.CreateElement("root");
                doc.AppendChild(root_node);

                XmlNode body_node = doc.CreateElement("body");
                root_node.AppendChild(body_node);
                XmlAttribute scaling_attr = doc.CreateAttribute("scaling");
                body_node.Attributes.Append(scaling_attr);
                scaling_attr.Value = textBoxScaling.Text;
                XmlAttribute outputgain_attr = doc.CreateAttribute("output_gain");
                body_node.Attributes.Append(outputgain_attr);
                outputgain_attr.Value = textBoxOutputGain.Text;

                XmlNode joints_node = doc.CreateElement("joints");
                body_node.AppendChild(joints_node);
                foreach (Joint joint in m_joints)
                {
                    XmlNode joint_node = doc.CreateElement("joint");
                    joints_node.AppendChild(joint_node);

                    XmlAttribute id_attr = doc.CreateAttribute("id");
                    joint_node.Attributes.Append(id_attr);
                    id_attr.Value = joint.ID.ToString();

                    XmlNode sensor_node = doc.CreateElement("has_sensor");
                    joint_node.AppendChild(sensor_node);
                    sensor_node.InnerText = joint.HasContactSensor.ToString().ToLower();

                    XmlNode head_node = doc.CreateElement("is_head");
                    joint_node.AppendChild(head_node);
                    head_node.InnerText = joint.IsHead.ToString().ToLower();
                    
                    XmlNode position_node = doc.CreateElement("position");
                    joint_node.AppendChild(position_node);
                    XmlAttribute position_x_attr = doc.CreateAttribute("x");
                    position_node.Attributes.Append(position_x_attr);
                    position_x_attr.Value = joint.Position.X.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                    XmlAttribute position_y_attr = doc.CreateAttribute("y");
                    position_node.Attributes.Append(position_y_attr);
                    position_y_attr.Value = joint.Position.Y.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);

                    XmlNode mass_node = doc.CreateElement("mass");
                    joint_node.AppendChild(mass_node);
                    mass_node.InnerText = joint.Mass.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);

                    //--- springs
                    
                    XmlNode angsprings_node = doc.CreateElement("angular_springs");
                    joint_node.AppendChild(angsprings_node);
                    foreach (AngularSpring spring in joint.AngularSprings)
                    {
                        XmlNode spring_node = doc.CreateElement("spring");
                        angsprings_node.AppendChild(spring_node);

                        XmlAttribute spring_id_attr = doc.CreateAttribute("id");
                        spring_node.Attributes.Append(spring_id_attr);
                        spring_id_attr.Value = spring.ID.ToString();

                        XmlNode muscle_node = doc.CreateElement("is_muscle");
                        spring_node.AppendChild(muscle_node);
                        muscle_node.InnerText = spring.IsMuscle.ToString().ToLower();

                        XmlNode gain_node = doc.CreateElement("muscle_gain");
                        spring_node.AppendChild(gain_node);
                        gain_node.InnerText = spring.MuscleGain.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);

                        XmlNode limb_A_node = doc.CreateElement("limbA");
                        spring_node.AppendChild(limb_A_node);
                        XmlAttribute limb_A_attr = doc.CreateAttribute("id");
                        limb_A_node.Attributes.Append(limb_A_attr);
                        limb_A_attr.Value = spring.LimbA.ID.ToString();

                        XmlNode limb_B_node = doc.CreateElement("limbB");
                        spring_node.AppendChild(limb_B_node);
                        XmlAttribute limb_B_attr = doc.CreateAttribute("id");
                        limb_B_node.Attributes.Append(limb_B_attr);
                        limb_B_attr.Value = spring.LimbB.ID.ToString();

                        XmlNode stiffness_node = doc.CreateElement("stiffness");
                        spring_node.AppendChild(stiffness_node);
                        stiffness_node.InnerText = spring.Stiffness.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);

                        XmlNode damping_node = doc.CreateElement("damping");
                        spring_node.AppendChild(damping_node);
                        damping_node.InnerText = spring.Damping.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                }

                XmlNode limbs_node = doc.CreateElement("limbs");
                body_node.AppendChild(limbs_node);
                foreach (Limb limb in m_limbs)
                {
                    XmlNode limb_node = doc.CreateElement("limb");
                    limbs_node.AppendChild(limb_node);

                    XmlAttribute id_attr = doc.CreateAttribute("id");
                    limb_node.Attributes.Append(id_attr);
                    id_attr.Value = limb.ID.ToString();

                    XmlNode muscle_node = doc.CreateElement("is_muscle");
                    limb_node.AppendChild(muscle_node);
                    muscle_node.InnerText = limb.IsMuscle.ToString().ToLower();

                    XmlNode gain_node = doc.CreateElement("muscle_gain");
                    limb_node.AppendChild(gain_node);
                    gain_node.InnerText = limb.MuscleGain.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);

                    XmlNode joint_A_node = doc.CreateElement("jointA");
                    limb_node.AppendChild(joint_A_node);
                    XmlAttribute joint_A_attr = doc.CreateAttribute("id");
                    joint_A_node.Attributes.Append(joint_A_attr);
                    joint_A_attr.Value = limb.JointA.ID.ToString();

                    XmlNode joint_B_node = doc.CreateElement("jointB");
                    limb_node.AppendChild(joint_B_node);
                    XmlAttribute joint_B_attr = doc.CreateAttribute("id");
                    joint_B_node.Attributes.Append(joint_B_attr);
                    joint_B_attr.Value = limb.JointB.ID.ToString();

                    XmlNode stiffness_node = doc.CreateElement("stiffness");
                    limb_node.AppendChild(stiffness_node);
                    stiffness_node.InnerText = limb.Stiffness.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);

                    XmlNode damping_node = doc.CreateElement("damping");
                    limb_node.AppendChild(damping_node);
                    damping_node.InnerText = limb.Damping.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                }

                // Brain ---

                update_brain();

                XmlNode brain_node = doc.CreateElement("brain");
                root_node.AppendChild(brain_node);

                XmlNode input_layer_node = doc.CreateElement("input_layer");
                brain_node.AppendChild(input_layer_node);

                XmlNode signals_node = doc.CreateElement("signals");
                input_layer_node.AppendChild(signals_node);

                foreach (InputSignal signal in m_input_layer.InputSignals)
                {
                    XmlNode signal_node = doc.CreateElement("signal");
                    signals_node.AppendChild(signal_node);

                    XmlAttribute name_attr = doc.CreateAttribute("name");
                    signal_node.Attributes.Append(name_attr);
                    name_attr.Value = signal.name;

                    XmlAttribute id_attr = doc.CreateAttribute("id");
                    signal_node.Attributes.Append(id_attr);
                    id_attr.Value = signal.id.ToString();

                    XmlAttribute active_attr = doc.CreateAttribute("active");
                    signal_node.Attributes.Append(active_attr);
                    active_attr.Value = signal.active.ToString().ToLower();
                }

                XmlNode hidden_layers_node = doc.CreateElement("hidden_layers");
                brain_node.AppendChild(hidden_layers_node);

                foreach (UIHiddenLayer hidden_layer in m_hidden_layers)
                {
                    if (hidden_layer.Active)
                    {
                        XmlNode layer_node = doc.CreateElement("layer");
                        hidden_layers_node.AppendChild(layer_node);

                        XmlNode recurrent_node = doc.CreateElement("is_recurrent");
                        layer_node.AppendChild(recurrent_node);
                        recurrent_node.InnerText = hidden_layer.Recurrent.ToString().ToLower();

                        XmlNode neurons_h_node = doc.CreateElement("neurons");
                        layer_node.AppendChild(neurons_h_node);
                        neurons_h_node.InnerText = hidden_layer.NumNeurons.ToString();
                    }
                }

                XmlNode output_layer_node = doc.CreateElement("output_layer");
                brain_node.AppendChild(output_layer_node);

                XmlNode neurons_o_node = doc.CreateElement("neurons");
                output_layer_node.AppendChild(neurons_o_node);
                neurons_o_node.InnerText = m_output_layer.NumNeurons.ToString();

                // Save the stuff ---

                doc.Save(saveFileDialog1.FileName);
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog(this))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(openFileDialog1.FileName);

                m_joints.Clear();
                m_limbs.Clear();
                m_angular_spring_limbs.Clear();

                XmlNode root_node = doc.FirstChild;

                XmlNode body_node = root_node["body"];
                XmlAttribute scaling_attr = body_node.Attributes["scaling"];
                textBoxScaling.Text = scaling_attr.Value;
                XmlAttribute outputgain_attr = body_node.Attributes["output_gain"];
                textBoxOutputGain.Text = outputgain_attr.Value;

                XmlNode joints_node = body_node["joints"];

                int max_joint_id = 0;
                int max_limb_id = 0;
                int max_ang_spring_id = 0;

                foreach (XmlNode joint_node in joints_node.ChildNodes)
                {
                    int id = int.Parse(joint_node.Attributes["id"].Value);
                    XmlNode sensor_node = joint_node["has_sensor"];
                    bool has_sensor = bool.Parse(sensor_node.InnerText);
                    XmlNode head_node = joint_node["is_head"];
                    bool is_head = bool.Parse(head_node.InnerText);
                    XmlNode position_node = joint_node["position"];
                    XmlAttribute x_attr = position_node.Attributes["x"];
                    XmlAttribute y_attr = position_node.Attributes["y"];
                    Point pos = new Point(
                        int.Parse(x_attr.Value),
                        int.Parse(y_attr.Value));
                    XmlNode mass_node = joint_node["mass"];
                    float mass = float.Parse(mass_node.InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                    if (id > max_joint_id)
                        max_joint_id = id;

                    Joint joint = new Joint(pos);
                    joint.ID = id;
                    joint.HasContactSensor = has_sensor;
                    joint.IsHead = is_head;
                    joint.Mass = mass;
                    m_joints.Add(joint);
                }
                Joint.set_global_id_ctr(max_joint_id + 1); // Fix global counter messed up in above loop.

                XmlNode limbs_node = body_node["limbs"];
                foreach (XmlNode limb_node in limbs_node)
                {
                    int id = int.Parse(limb_node.Attributes["id"].Value);
                    XmlNode muscle_node = limb_node["is_muscle"];
                    bool is_muscle = bool.Parse(muscle_node.InnerText);
                    XmlNode gain_node = limb_node["muscle_gain"];
                    float muscle_gain = float.Parse(gain_node.InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                    XmlNode joint_A_node = limb_node["jointA"];
                    XmlAttribute joint_A_attr = joint_A_node.Attributes["id"];
                    int joint_A_id = int.Parse(joint_A_attr.Value);
                    
                    XmlNode joint_B_node = limb_node["jointB"];
                    XmlAttribute joint_B_attr = joint_B_node.Attributes["id"];
                    int joint_B_id = int.Parse(joint_B_attr.Value);

                    XmlNode stiffness_node = limb_node["stiffness"];
                    float stiffness = float.Parse(stiffness_node.InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                    XmlNode damping_node = limb_node["damping"];
                    float damping = float.Parse(damping_node.InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                    Joint joint_A = null, joint_B = null;
                    foreach (Joint joint in m_joints)
                    {
                        if (joint.ID == joint_A_id)
                            joint_A = joint;
                        else if (joint.ID == joint_B_id)
                            joint_B = joint;
                    }
                    if (joint_A != null && joint_B != null)
                    {
                        if (id > max_limb_id)
                            max_limb_id = id;

                        Limb limb = new Limb(joint_A, joint_B);
                        limb.ID = id;
                        limb.IsMuscle = is_muscle;
                        limb.MuscleGain = muscle_gain;
                        limb.Stiffness = stiffness;
                        limb.Damping = damping;
                        m_limbs.Add(limb);
                    }
                }
                Limb.set_global_id_ctr(max_limb_id + 1); // Fix global counter messed up in above loop.

                foreach (XmlNode joint_node in joints_node)
                {
                    XmlNode angsprings_node = joint_node["angular_springs"];
                    foreach (XmlNode spring_node in angsprings_node)
                    {
                        int joint_id = int.Parse(joint_node.Attributes["id"].Value);

                        int id = int.Parse(spring_node.Attributes["id"].Value);
                        XmlNode muscle_node = spring_node["is_muscle"];
                        bool is_muscle = bool.Parse(muscle_node.InnerText);
                        XmlNode gain_node = spring_node["muscle_gain"];
                        float muscle_gain = float.Parse(gain_node.InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                        XmlNode limb_A_node = spring_node["limbA"];
                        XmlAttribute limb_A_attr = limb_A_node.Attributes["id"];
                        int limb_A_id = int.Parse(limb_A_attr.Value);

                        XmlNode limb_B_node = spring_node["limbB"];
                        XmlAttribute limb_B_attr = limb_B_node.Attributes["id"];
                        int limb_B_id = int.Parse(limb_B_attr.Value);

                        XmlNode stiffness_node = spring_node["stiffness"];
                        float stiffness = float.Parse(stiffness_node.InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);
                        XmlNode damping_node = spring_node["damping"];
                        float damping = float.Parse(damping_node.InnerText, System.Globalization.NumberFormatInfo.InvariantInfo);

                        Joint joint_C = null;
                        foreach (Joint joint in m_joints)
                            if (joint.ID == joint_id)
                            {
                                joint_C = joint;
                                break;
                            }

                        Limb limb_A = null, limb_B = null;
                        foreach (Limb limb in m_limbs)
                        {
                            if (limb.ID == limb_A_id)
                                limb_A = limb;
                            else if (limb.ID == limb_B_id)
                                limb_B = limb;
                        }
                        if (joint_C != null && limb_A != null && limb_B != null)
                        {
                            if (id > max_ang_spring_id)
                                max_ang_spring_id = id;

                            AngularSpring spring = new AngularSpring(joint_C, limb_A, limb_B);
                            spring.ID = id;
                            spring.IsMuscle = is_muscle;
                            spring.Stiffness = stiffness;
                            spring.Damping = damping;
                            spring.MuscleGain = muscle_gain;
                            joint_C.AddAngularSpring(spring);
                        }
                    }
                }
                AngularSpring.set_global_id_ctr(max_ang_spring_id + 1); // Fix global counter messed up in above loop.

                XmlNode brain_node = root_node["brain"];

                XmlNode input_layer_node = brain_node["input_layer"];

                XmlNode signals_node = input_layer_node["signals"];
                m_input_layer.ClearSignals();
                m_input_layer.BeginUpdate();
                foreach (XmlNode signal_node in signals_node.ChildNodes)
                {
                    string name = signal_node.Attributes["name"].Value;
                    int id = int.Parse(signal_node.Attributes["id"].Value);
                    bool active = bool.Parse(signal_node.Attributes["active"].Value);

                    InputSignal signal = new InputSignal(name, id, active);
                    m_input_layer.AddSignal(signal);
                }
                m_input_layer.EndUpdate();


                XmlNode hidden_layers_node = brain_node["hidden_layers"];

                int hidden_layer_idx = 0;
                foreach (UIHiddenLayer layer in m_hidden_layers)
                    layer.Active = false;
                foreach (XmlNode layer_node in hidden_layers_node.ChildNodes)
                {
                    XmlNode recurrent_node = layer_node["is_recurrent"];
                    bool is_recurrent = bool.Parse(recurrent_node.InnerText);
                    XmlNode neurons_h_node = layer_node["neurons"];
                    int num_neurons = int.Parse(neurons_h_node.InnerText);
                    UIHiddenLayer layer = m_hidden_layers[hidden_layer_idx++];
                    layer.Active = true;
                    layer.Recurrent = is_recurrent;
                    layer.NumNeurons = num_neurons;
                }

                XmlNode output_layer_node = brain_node["output_layer"];
                XmlNode neurons_o_node = output_layer_node["neurons"];
                m_output_layer.NumNeurons = int.Parse(neurons_o_node.InnerText);

                canvasBodyDesign.Refresh();
                canvasBrainDesign.Refresh();
            }
        }

    }
}
