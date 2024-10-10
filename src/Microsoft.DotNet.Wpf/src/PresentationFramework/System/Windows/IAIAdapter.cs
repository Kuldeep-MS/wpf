namespace System.Windows
{
    public enum AdapterType
    {
        Text,
        Image
    };

    public interface IAIAdapter {
        void sendRequest(string prompt, FrameworkElement fe);
        AdapterType GetAdapterType();
    }
}