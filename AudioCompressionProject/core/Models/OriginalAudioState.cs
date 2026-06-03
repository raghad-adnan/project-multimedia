namespace AudioCompressionProject.Core.Models
{
    public class OriginalAudioState
    {
        public int SampleRate { get; set; }

        public int BitDepth { get; set; }

        public int Channels { get; set; }

        public string Algorithm { get; set; }
    }
}