namespace AudioCompressionProject.Core.Models
{
    public class CompressionSettings
    {
        public string Algorithm { get; set; }

        public int SampleRate { get; set; }

        public int BitDepth { get; set; }

        public int QuantizationLevels { get; set; }

        public static CompressionSettings Default()
        {
            return new CompressionSettings
            {
                Algorithm = "DPCM",
                SampleRate = 44100,
                BitDepth = 16,
                QuantizationLevels = 256
            };
        }
    }
}