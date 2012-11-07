using System;

namespace cocos2d
{
    public class CCWaves3D : CCGrid3DAction
    {
        protected float m_fAmplitude;
        protected float m_fAmplitudeRate;
        protected int m_nWaves;

        public float Amplitude
        {
            get { return m_fAmplitude; }
            set { m_fAmplitude = value; }
        }

        public override float AmplitudeRate
        {
            get { return m_fAmplitudeRate; }
            set { m_fAmplitudeRate = value; }
        }

        public bool InitWithWaves(int wav, float amp, ccGridSize gridSize, float duration)
        {
            if (InitWithSize(gridSize, duration))
            {
                m_nWaves = wav;
                m_fAmplitude = amp;
                m_fAmplitudeRate = 1.0f;
                return true;
            }
            return false;
        }

        public override CCObject CopyWithZone(CCZone pZone)
        {
            CCWaves3D pCopy;
            if (pZone != null && pZone.m_pCopyObject != null)
            {
                //in case of being called at sub class
                pCopy = (CCWaves3D) (pZone.m_pCopyObject);
            }
            else
            {
                pCopy = new CCWaves3D();
                pZone = new CCZone(pCopy);
            }

            base.CopyWithZone(pZone);

            pCopy.InitWithWaves(m_nWaves, m_fAmplitude, m_sGridSize, m_fDuration);

            return pCopy;
        }

        public override void Update(float time)
        {
            int i, j;
            for (i = 0; i < m_sGridSize.x + 1; ++i)
            {
                for (j = 0; j < m_sGridSize.y + 1; ++j)
                {
                    ccVertex3F v = OriginalVertex(new ccGridSize(i, j));
                    v.z += ((float) Math.Sin((float) Math.PI * time * m_nWaves * 2 + (v.y + v.x) * .01f) * m_fAmplitude * m_fAmplitudeRate);
                    SetVertex(new ccGridSize(i, j), ref v);
                }
            }
        }

        public static CCWaves3D Create(int wav, float amp, ccGridSize gridSize, float duration)
        {
            var pAction = new CCWaves3D();
            pAction.InitWithWaves(wav, amp, gridSize, duration);
            return pAction;
        }
    }
}