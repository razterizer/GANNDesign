using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using MathLib.utils;

namespace GANNDesign.body
{
    class Joint
    {
        #region DATA
        static int m_global_id_ctr;
        int m_id;
        List<Limb> m_limbs;
        List<AngularSpring> m_angular_springs;
        
        float m_angular_spring_r0;
        float m_angular_spring_dr;
        #endregion

        #region CONSTRUCTORS
        static Joint()
        {
            m_global_id_ctr = 0;
        }

        public Joint(Point pos)
        {
            m_id = m_global_id_ctr++;
            this.Position = pos;
            m_limbs = new List<Limb>();
            this.Mass = 0.5f;
            this.HasContactSensor = true;
            this.IsHead = false;
            m_angular_springs = new List<AngularSpring>();
            m_angular_spring_r0 = 10.0f;
            m_angular_spring_dr = 3.0f;
        }
        #endregion

        #region PROPERTIES
        public int ID
        {
            get { return m_id; }
            set
            {
                m_id = value;
                m_global_id_ctr = m_id + 1;
            }
        }

        public Point Position
        {
            get;
            set;
        }

        public float Mass
        {
            get;
            set;
        }

        public bool HasContactSensor
        {
            get;
            set;
        }

        public bool IsHead
        {
            get;
            set;
        }

        public int NumLimbs
        {
            get { return m_limbs.Count; }
        }

        public IEnumerable<AngularSpring> AngularSprings
        {
            get { return m_angular_springs; }
        }
        #endregion

        #region LIMB METHODS
        public void AddLimb(Limb limb)
        {
            m_limbs.Add(limb);
        }

        public void RemoveLimb(Limb limb)
        {
            m_limbs.Remove(limb);

            bool found_matching_spring;
            do
            {
                found_matching_spring = false;
                foreach (AngularSpring spring in m_angular_springs)
                    if (spring.LimbA == limb || spring.LimbB == limb)
                    {
                        m_angular_springs.Remove(spring);
                        found_matching_spring = true;
                        break;
                    }
            } while (found_matching_spring);
        }
        #endregion

        #region ANGULAR SPRING METHODS
        public void AddAngularSpring(AngularSpring spring)
        {
            if (FindAngularSpring(spring.LimbA, spring.LimbB) == null)
                m_angular_springs.Add(spring);

            PointF pC;
            float ang_start;
            float ang_end;
            calc_spring_angles(spring, out pC, out ang_start, out ang_end);
            spring.RestAngle = ang_end - ang_start;
            if (spring.RestAngle < 0.0f)
                spring.RestAngle = -spring.RestAngle;
                //spring.RestAngle += (float)(2.0 * Math.PI);
            //if (ang_end - ang_start > (float)Math.PI)
            //{
            //    spring.RestAngle = (float)(2.0 * Math.PI) - (ang_end - ang_start);
            //    spring.SwapLimbs();
            //}
        }

        public void RemoveAngularSpring(AngularSpring spring)
        {
            m_angular_springs.Remove(spring);
        }

        public AngularSpring FindAngularSpring(Limb limbA, Limb limbB)
        {
            foreach (AngularSpring spring in m_angular_springs)
                if ((spring.LimbA == limbA && spring.LimbB == limbB) ||
                    (spring.LimbA == limbB && spring.LimbB == limbA))
                {
                    return spring;
                }
            return null;
        }

        public AngularSpring FindClosestAngularSpring(Point p)
        {
            for (int spring_idx = 0; spring_idx < m_angular_springs.Count; spring_idx++)
            {
                AngularSpring spring = m_angular_springs[spring_idx];
                PointF pC;
                float ang_start;
                float ang_end;
                calc_spring_angles(spring, out pC, out ang_start, out ang_end);

                float radius = m_angular_spring_r0 + spring_idx * m_angular_spring_dr;
                PointF v = UtilsPointF.Minus(p, pC);
                float distance = UtilsPointF.Length(v);
                float tol = m_angular_spring_dr * 0.5f;
                if (radius - tol < distance && distance < radius + tol)
                {
                    float point_angle = (float)Math.Atan2(v.Y, v.X);
                    if (point_angle < 0.0f)
                        point_angle += (float)(2.0 * Math.PI);
                    if ((ang_start <= point_angle && point_angle <= ang_end) ||
                        (ang_end <= point_angle && point_angle <= ang_start))
                    {
                        return spring;
                    }
                }
            }
            return null;
        }

        public void DrawAngularSprings(Graphics g, AngularSpring hilited_spring)
        {
            const float rad2deg = (float)(180.0 / Math.PI);
            Pen normal_pen = (Pen)Pens.Blue.Clone();
            Pen hilite_pen = new Pen(Color.Blue, 2.0f);
            for (int spring_idx = 0; spring_idx < m_angular_springs.Count; spring_idx++)
            {
                AngularSpring spring = m_angular_springs[spring_idx];
                normal_pen.Color = spring.IsMuscle ? Color.DeepPink : Color.Blue;
                hilite_pen.Color = spring.IsMuscle ? Color.DeepPink : Color.Blue;
                PointF pC;
                float ang_start;
                float ang_end;
                calc_spring_angles(spring, out pC, out ang_start, out ang_end);
                float ang_sweep = ang_end - ang_start;

                float radius = m_angular_spring_r0 + spring_idx * m_angular_spring_dr;
                PointF bb_pos = UtilsPointF.Minus(pC, new PointF(radius, radius));
                SizeF bb_size = new SizeF(2.0f * radius, 2.0f * radius);

                g.DrawArc(hilited_spring == m_angular_springs[spring_idx] ? hilite_pen : normal_pen,
                    new RectangleF(bb_pos, bb_size), ang_start * rad2deg, ang_sweep * rad2deg);
            }
        }

        private void calc_spring_angles(AngularSpring spring, out PointF pC,
            out float ang_start_rad, out float ang_end_rad)
        {
            PointF pL;
            PointF pR;
            pC = this.Position;

            if (spring.LimbA.JointA == this)
                pL = spring.LimbA.JointB.Position;
            else
                pL = spring.LimbA.JointA.Position;

            if (spring.LimbB.JointA == this)
                pR = spring.LimbB.JointB.Position;
            else
                pR = spring.LimbB.JointA.Position;

            PointF vL = UtilsPointF.Minus(pL, pC);
            PointF vR = UtilsPointF.Minus(pR, pC);

            ang_start_rad = (float)Math.Atan2(vL.Y, vL.X);
            ang_end_rad = (float)Math.Atan2(vR.Y, vR.X);
            if (ang_start_rad < 0.0f)
                ang_start_rad += (float)(2.0 * Math.PI);
            if (ang_end_rad < 0.0f)
                ang_end_rad += (float)(2.0 * Math.PI);
            //Console.WriteLine("a0 = {0}, a1 = {1}", ang_start_rad, ang_end_rad);
        }
        #endregion
    }
}
