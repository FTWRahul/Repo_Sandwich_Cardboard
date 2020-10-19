using UnityEngine;

namespace ExtensionMethods
{
    public static class FloatExtensionMethods
    {
        /// Returns a value mapped from the current min max to the provided min max
        public static float Remap( this float value, float iMin, float iMax, float oMin, float oMax )
        {
            var t = Mathf.InverseLerp( iMin, iMax, value );
            return Mathf.Lerp( oMin, oMax, t );
        }
    }
}