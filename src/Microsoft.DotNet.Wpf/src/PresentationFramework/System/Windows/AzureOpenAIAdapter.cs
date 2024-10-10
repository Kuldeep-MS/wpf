using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;
using Azure.AI.OpenAI.Images;
using OpenAI.Images;
using System.ClientModel;

namespace System.Windows
{
    public class AzureOpenAIAdapter : IAIAdapter
    {
        string _endpoint;
        string _key;
        AdapterType _adapterType;
        public AzureOpenAIAdapter(string endpoint, string key, AdapterType adapterType)
        {
            _endpoint = endpoint;
            _key = key;
            _adapterType = adapterType;
        }

        void IAIAdapter.sendRequest(string prompt, FrameworkElement fe)
        {
            switch(_adapterType)
            {
                case AdapterType.Text:
                    sendRequestImplText(prompt, fe);
                break;
                case AdapterType.Image:
                    sendRequestImplImage(prompt, fe);
                break;
            }
        }

        void sendRequestImplText(string prompt, FrameworkElement fe)
        {
            AzureOpenAIClient client = new(
                new Uri(_endpoint),
                new AzureKeyCredential(_key));
        
            ChatClient chatClient = client.GetChatClient("CopilotDataGrid");
        
            ChatCompletion completion = chatClient.CompleteChat([ new UserChatMessage(prompt) ]);

            fe.AIResponse = completion.Content[0].Text;

            UIATaskWorker.ExecuteTask(fe.UIAActions, fe);
        }

        async void sendRequestImplImage(string prompt, FrameworkElement fe)
        {
            AzureOpenAIClient client = new(
                new Uri(_endpoint),
                new AzureKeyCredential(_key));
        
            ImageClient imgclient = client.GetImageClient("Dalle3");
        
            ClientResult<GeneratedImage> imageResult = await imgclient.GenerateImageAsync(prompt, new()
            {
                Quality = GeneratedImageQuality.Standard,
                Size = GeneratedImageSize.W1024xH1024,
                ResponseFormat = GeneratedImageFormat.Uri
            });

            fe.AIResponse = imageResult.Value;

            UIATaskWorker.ExecuteTask(fe.UIAActions, fe);
        }

        AdapterType IAIAdapter.GetAdapterType()
        {
            return _adapterType;
        }
    }
}