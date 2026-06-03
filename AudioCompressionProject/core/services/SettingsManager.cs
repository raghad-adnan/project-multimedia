using AudioCompressionProject.Core.Models;

namespace AudioCompressionProject.Core.Services
{
    public class SettingsManager
    {
        private OriginalAudioState originalState;

        public void SaveOriginalState(
            OriginalAudioState state)
        {
            originalState = state;
        }

        public OriginalAudioState Reset()
        {
            return originalState;
        }
    }
}