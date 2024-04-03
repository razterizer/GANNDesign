using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GANNDesign.ui.components
{
    class UIPalette
    {
        List<UIButton> m_buttons;
        Point m_location;
        Point m_mouse_down;

        public UIPalette(Point location)
        {
            int button_width = 80;
            int button_height = 30;
            m_location = location;
            m_buttons = new List<UIButton>();
            
            UIButtonLinearSpring button_linear_spring = new UIButtonLinearSpring(new Rectangle(
                location.X, 
                location.Y, 
                button_width, 
                button_height));
            m_buttons.Add(button_linear_spring);
            
            UIButtonAngularSpring button_angular_spring = new UIButtonAngularSpring(new Rectangle(
                button_linear_spring.Bounds.Left, 
                button_linear_spring.Bounds.Bottom, 
                button_width, 
                button_height));
            m_buttons.Add(button_angular_spring);

            UIButtonMove button_move = new UIButtonMove(new Rectangle(
                button_angular_spring.Bounds.Left, 
                button_angular_spring.Bounds.Bottom, 
                button_width, 
                button_height));
            m_buttons.Add(button_move);

            UIButtonRemove button_remove = new UIButtonRemove(new Rectangle(
                button_move.Bounds.Left,
                button_move.Bounds.Bottom,
                button_width,
                button_height));
            m_buttons.Add(button_remove);

            UIButtonConfigure button_configure = new UIButtonConfigure(new Rectangle(
                button_remove.Bounds.Left,
                button_remove.Bounds.Bottom,
                button_width,
                button_height));
            m_buttons.Add(button_configure);
        }

        public void Draw(Graphics g)
        {
            foreach (UIButton button in m_buttons)
                button.Draw(g);
        }

        public void MouseDown(Point p)
        {
            m_mouse_down = p;
        }

        public void MouseUp(Point p)
        {
            foreach (UIButton button in m_buttons)
            {
                if (button.Bounds.Contains(m_mouse_down) &&
                    button.Bounds.Contains(p))
                {
                    button.Pressed = !button.Pressed;
                }
                else
                    button.Pressed = false;
            }
        }

        public UIButton ActiveButton
        {
            get
            {
                foreach (UIButton button in m_buttons)
                    if (button.Pressed)
                        return button;
                return null;
            }
        }

        public UIButton.ButtonType ActiveButtonType
        {
            get
            {
                UIButton active_button = this.ActiveButton;
                if (active_button == null)
                    return UIButton.ButtonType.None;
                else
                    return active_button.Type;
            }
        }

        public bool Contains(Point p)
        {
            foreach (UIButton button in m_buttons)
                if (button.Bounds.Contains(p))
                    return true;
            return false;
        }

        public void Clear()
        {
            foreach (UIButton button in m_buttons)
                button.Pressed = false;
        }
    }
}
