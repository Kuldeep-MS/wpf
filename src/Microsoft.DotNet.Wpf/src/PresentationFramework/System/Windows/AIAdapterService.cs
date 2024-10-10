namespace System.Windows
{
    public static class AIAdapterService
    {
        public static IAIAdapter s_TextAdapter;
        public static IAIAdapter s_ImageAdapter;
        public static bool s_isTextAdapterAvailable = false;
        public static bool s_isImageAdapterAvailable = false;
        public static IAIAdapter GetCurrentAdapter(AdapterType adapterType)
        {
            switch(adapterType)
            {
                case AdapterType.Text:
                    if(s_TextAdapter != null)
                    {
                        return s_TextAdapter;
                    }
                    throw new Exception("Text AI Adapter not registered");
                case AdapterType.Image:
                    if(s_ImageAdapter != null)
                    {
                        return s_ImageAdapter;
                    }
                    throw new Exception("Image AI Adapter not registered");
            }

            return null;
        }

        public static void SetCurrentAdapter(IAIAdapter adapter)
        {
            if(adapter != null)
            {

                switch(adapter.GetAdapterType())
                {
                    case AdapterType.Text:
                        s_TextAdapter = adapter;
                        s_isTextAdapterAvailable = true;
                    break;
                    case AdapterType.Image:
                        s_ImageAdapter = adapter;
                        s_isImageAdapterAvailable = true;
                    break;
                }
            } else
            {
                throw new Exception("Cannot AI adapter to null");
            }
        }

        public static bool IsAITextAdapterAvailable()
        {
            return s_isTextAdapterAvailable;
        }

        public static bool IsAIImageAdapterAvailable()
        {
            return s_isImageAdapterAvailable;
        }
    }
}