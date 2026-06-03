using System;
using System.Collections.Generic;

namespace AudioCompressionProject.Core.Algorithms
{
    public class DPCMAlgorithm : ICompressionAlgorithm
    {
        public byte[] Compress(short[] samples)
        {
            if (samples == null || samples.Length == 0)
                return new byte[0];

            List<short> differences = new List<short>();

            differences.Add(samples[0]);

            for (int i = 1; i < samples.Length; i++)
            {
                short diff = (short)(samples[i] - samples[i - 1]);
                differences.Add(diff);
            }

            byte[] result = new byte[differences.Count * sizeof(short)];

            Buffer.BlockCopy(
                differences.ToArray(),
                0,
                result,
                0,
                result.Length);

            return result;
        }

        public short[] Decompress(byte[] compressedData)
        {
            if (compressedData == null || compressedData.Length == 0)
                return new short[0];

            short[] differences =
                new short[compressedData.Length / sizeof(short)];

            Buffer.BlockCopy(
                compressedData,
                0,
                differences,
                0,
                compressedData.Length);

            short[] reconstructed =
                new short[differences.Length];

            reconstructed[0] = differences[0];

            for (int i = 1; i < differences.Length; i++)
            {
                reconstructed[i] =
                    (short)(reconstructed[i - 1] + differences[i]);
            }

            return reconstructed;
        }
    }
}