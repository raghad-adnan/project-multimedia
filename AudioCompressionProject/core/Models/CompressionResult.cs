namespace AudioCompressionProject.Core.Models
{
    public class CompressionResult
    {
        public long OriginalSize { get; set; }

        public long CompressedSize { get; set; }

        public double CompressionRatio { get; set; }

        public double SavingPercentage { get; set; }

        public double ExecutionTimeMs { get; set; }

        public string AlgorithmName { get; set; }

        public CompressionSettings Settings { get; set; }
    }
}