using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GANNDesign.body
{
    class AngularSpring
    {
        #region DATA
        static int m_global_id_ctr;
        int m_id;

        Joint m_joint;
        Limb[] m_limbs;
        #endregion

        #region CONSTRUCTORS
        static AngularSpring()
        {
            m_global_id_ctr = 0;
        }

        public AngularSpring(Joint joint, Limb limb_A, Limb limb_B)
        {
            m_id = m_global_id_ctr++;
            m_joint = joint;
            m_limbs = new Limb[2];
            m_limbs[0] = limb_A;
            m_limbs[1] = limb_B;
            this.DrawRadius = float.NaN;

            this.Stiffness = 1000.0f;
            this.Damping = 1.0f;
            this.RestAngle = 0.0f;
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

        public float DrawRadius
        {
            get;
            set;
        }

        public Joint Joint
        {
            get { return m_joint; }
        }

        public Limb LimbA
        {
            get { return m_limbs[0]; }
        }

        public Limb LimbB
        {
            get { return m_limbs[1]; }
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

        public float RestAngle
        {
            get;
            set;
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
    }
}
