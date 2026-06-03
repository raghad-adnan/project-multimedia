namespace AudioCompressionProject.Core.Algorithms
{
    public interface ICompressionAlgorithm
    {
        byte[] Compress(short[] samples);

        short[] Decompress(byte[] compressedData);
    }
}