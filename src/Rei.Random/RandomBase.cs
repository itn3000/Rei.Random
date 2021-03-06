/*
 * Copyright (C) Rei HOBARA 2007
 * 
 * Name:
 *     RandomBase.cs
 * Class:
 *     Rei.Random.RandomBase
 * Purpose:
 *     A base class for random number generator.
 * Remark:
 * History:
 *     2007/10/6 Initial release.
 *     2016/1/29 Modified NextDouble method and translated XML comments to English.
 */

using System;

namespace Rei.Random {

    /// <summary>
    /// Base class for a variety of pseudo-random number generators.
    /// </summary>
    public abstract class RandomBase
    {

        /// <summary>
        /// Returns a random unsigned integer.
        /// </summary>
        public abstract UInt32 NextUInt32();

        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        public virtual Int32 NextInt32()
        {
            return (Int32) NextUInt32();
        }

        /// <summary>
        /// Returns a random unsigned 64 bit integer.
        /// </summary>
        public virtual UInt64 NextUInt64()
        {
            return ((UInt64) NextUInt32() << 32) | NextUInt32();
        }

        /// <summary>
        /// Returns a non-negative random 64 bit integer.
        /// </summary>
        public virtual Int64 NextInt64()
        {
            return ((Int64) NextUInt32() << 32) | NextUInt32();
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public virtual void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "buffer is null.");
            }

            int i = 0;
            UInt32 r;
            while (i + 4 <= buffer.Length)
            {
                r = NextUInt32();
                buffer[i++] = (byte) r;
                buffer[i++] = (byte) (r >> 8);
                buffer[i++] = (byte) (r >> 16);
                buffer[i++] = (byte) (r >> 24);
            }
            if (i >= buffer.Length) return;
            r = NextUInt32();
            buffer[i++] = (byte) r;
            if (i >= buffer.Length) return;
            buffer[i++] = (byte) (r >> 8);
            if (i >= buffer.Length) return;
            buffer[i++] = (byte) (r >> 16);
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <returns></returns>
        public virtual double NextDouble()
        {
            return ((double) NextUInt32())/uint.MaxValue;

            // Can't figure out the intention behind the original method (so I changed it).
            // The method does not return a decimal [0,1)
            // Simplified result is: (([random uint] * 4096) + [random uint]) / 4194304
            // Maybe I'm missing something, but this will rarely generate a double [0,1).

            //UInt32 r1, r2;
            //r1 = NextUInt32();
            //r2 = NextUInt32();
            //return (r1 * (double)(2 << 11) + r2) / (double)(2 << 53);
        }
    }
}