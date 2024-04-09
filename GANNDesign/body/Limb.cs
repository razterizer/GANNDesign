using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathLib.utils;

namespace GANNDesign.body
{
    class Limb
    {
        #region DATA
        static int m_global_id_ctr;
        int m_id;
        Joint[] m_joints;
        #endregion

        #region CONSTRUCTORS
        static Limb()
        {
            m_global_id_ctr = 0;
        }

        public Limb(Joint joint_A, Joint joint_B)
        {
            m_id = m_global_id_ctr++;
            m_joints = new Joint[2];
            m_joints[0] = joint_A;
            m_joints[1] = joint_B;
            joint_A.AddLimb(this);
            joint_B.AddLimb(this);

            this.Stiffness = 100.0f;
            this.Damping = 1.0f;
            this.IsMuscle = false;
            this.MuscleGain = 1.0f;
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

        public Joint JointA
        {
            get { return m_joints[0]; }
        }

        public Joint JointB
        {
            get { return m_joints[1]; }
        }

        public float Stiffness
        {
            get;
            set;
        }

        public float Damping
        {
            get;
            set;
        }

        public float RestLength
        {
            get { return UtilsPointF.Distance(m_joints[0].Position, m_joints[1].Position); }
        }

        public bool IsMuscle
        {
            get;
            set;
        }

        public float MuscleGain
        {
            get;
            set;
        }
        #endregion

        #region METHODS
        public static void set_global_id_ctr(int ctr)
        {
            m_global_id_ctr = ctr;
        }
        #endregion
    }
}
